#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserService.cs </Name>
//         <Created> 20/04/2018 6:50:54 PM </Created>
//         <Key> c9211419-02f0-4cd3-87ac-064059a194e6 </Key>
//     </File>
//     <Summary>
//         UserService.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Contract.Repository.Interfaces;
using BETSTS.Contract.Repository.Models.User;
using BETSTS.Contract.Service;
using BETSTS.Core.Exceptions;
using BETSTS.Core.Models.User;
using BETSTS.Core.Utils;
using Elect.DI.Attributes;
using Elect.Mapper.AutoMapper.IQueryableUtils;
using Elect.Web.Api.Models;
using Elect.Web.DataTable.Models.Request;
using Elect.Web.DataTable.Models.Response;
using Elect.Web.DataTable.Processing.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PagedRequestModel = BETSTS.Core.Models.PagedRequestModel;

namespace BETSTS.Service
{
    [ScopedDependency(ServiceType = typeof(IUserService))]
    public class UserService : Base.Service, IUserService
    {
        //private readonly IServiceProvider _serviceProvider;

        private readonly IRepository<UserEntity> _userRepo;

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRepo = unitOfWork.GetRepository<UserEntity>();
        }

        // Get

        public Task<PagedResponseModel<UserModel>> GetPagedAsync(PagedRequestModel model, CancellationToken cancellationToken = default)
        {
            var query = _userRepo.Get();

            var total = query.Count();

            var items = query.QueryTo<UserModel>().OrderByDescending(x => x.CreatedTime).Skip(model.Skip).Take(model.Take).ToList();

            var pagedResponse = new PagedResponseModel<UserModel> { Total = total, Items = items };

            return Task.FromResult(pagedResponse);
        }

        public Task<DataTableResponseModel<UserModel>> GetDataTableAsync(DataTableRequestModel model, CancellationToken cancellationToken = default)
        {
            var query = _userRepo.Get();

            var listData = query.QueryTo<UserModel>();

            var result = listData.GetDataTableResponse(model);

            return Task.FromResult(result);
        }
        public GetAllUserModel GetAllUser()
        {
            var listUser = _userRepo.Include(x => x.Amout).Where(x => x.DeletedTime == null).ToList();
            var listModel = new GetAllUserModel();
            listModel.AllTotal = 0;
            listModel.AllSent = 0;
            var listUserModel = new List<GetUserModel>();
            foreach (var user in listUser)
            {
                listUserModel.Add(new GetUserModel(){Id = user.Id,Username = user.UserName,Sent = user.Amout.Sent,Total = user.Amout.Total});
                listModel.AllTotal += user.Amout.Total;
                listModel.AllSent += user.Amout.Sent;
            }
            listModel.Users = listUserModel;
            return listModel;
        }
        public Task<UserModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = _userRepo.Get(x => x.Id == id).QueryTo<UserModel>().SingleOrDefault();

            return Task.FromResult(user);
        }

        public Task<UserModel> GetAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = _userRepo.Get(x => x.Email == email).QueryTo<UserModel>().SingleOrDefault();

            return Task.FromResult(user);
        }

        // Create

        public Task<Guid> CreateAsync(Guid userId,CreateUserModel model, CancellationToken cancellationToken = default)
        {
            CheckIsAdmin(userId);

            CheckUniqueUserName(model.Username);

            var systemNow = SystemHelper.SystemTimeNow;

            var userEntity = new UserEntity()
            {
                UserName = model.Username,
                PasswordHash = AuthenticationService.HashPassword(model.Password, systemNow),
                PasswordLastUpdatedTime = systemNow,
                CreatedTime = SystemHelper.GetNetworkTime(),
                IsAdmin = 0,
                Amout = new AmoutEntity() { Sent = 0,Total = 0}
            };

            _userRepo.Add(userEntity);

            cancellationToken.ThrowIfCancellationRequested();

            UnitOfWork.SaveChanges();

            return Task.FromResult(userEntity.Id);
        }

        // Change password
        public Task ChangePassword(Guid userId,ChangePasswordModel model, CancellationToken cancellationToken = default)
        {
            CheckExist(userId);
            var user = _userRepo.Get(x => x.Id == userId).Single();
            if (user.PasswordLastUpdatedTime == null)
            {
                throw new CoreException(ErrorCode.UserPasswordWrong);
            }

            var password = AuthenticationService.HashPassword(model.OldPassword, user.PasswordLastUpdatedTime.Value);

            if (password != user.PasswordHash)
            {
                throw new CoreException(ErrorCode.UserPasswordWrong);
            }
            var systemNow = SystemHelper.SystemTimeNow;
            user.PasswordHash = AuthenticationService.HashPassword(model.NewPassword, systemNow);
            user.PasswordLastUpdatedTime = systemNow;

            _userRepo.Update(user,x=>x.PasswordHash,x=>x.PasswordLastUpdatedTime);

            UnitOfWork.SaveChanges();
            return Task.CompletedTask;
        }

     

        // Delete

        public Task DeleteAsync(Guid userId,Guid id, CancellationToken cancellationToken = default)
        {
            CheckIsAdmin(userId);

            CheckExist(id);

            _userRepo.Delete(new UserEntity { Id = id });

            cancellationToken.ThrowIfCancellationRequested();

            UnitOfWork.SaveChanges();

            return Task.CompletedTask;
        }

        #region Helper

        public void CheckIsAdmin(Guid id)
        {
            var isAdmin = _userRepo.Get(x => x.Id == id).SingleOrDefault(x=>x.IsAdmin == 1);
            if (isAdmin == null)
            {
                throw new CoreException(ErrorCode.UnAuthenticated,"Không phải việc của mài !!!");
            }
        }
        public void CheckExist(Guid id)
        {
            var isExist = _userRepo.Get(x => x.Id == id).Any();

            if (!isExist)
            {
                throw new CoreException(ErrorCode.NotFound, "The User is not found.");
            }
        }

        public void CheckUniqueUserName(string userName, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return;
            }

            var isExist = _userRepo.Get(x=>x.UserName == userName).Any();

            if (isExist)
            {
                throw new CoreException(ErrorCode.NotUnique, $"User {userName} is already exist, please try another.");
            }
        }

        public void CheckUniquePhone(string phone, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return;
            }

            var query = _userRepo.Get(x => x.Phone == phone);

            if (excludeId != null && excludeId.Value != Guid.Empty)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }

            var isExist = query.Any();

            if (isExist)
            {
                throw new CoreException(ErrorCode.NotUnique, $"Phone {phone} is already exist, please try another.");
            }
        }

        public void CheckUniqueEmail(string email, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            var query = _userRepo.Get(x => x.Email == email);

            if (excludeId != null && excludeId.Value != Guid.Empty)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }

            var isExist = query.Any();

            if (isExist)
            {
                throw new CoreException(ErrorCode.NotUnique, $"Email {email} is already exist, please try another.");
            }
        }
        public void CheckUniqueUsername(string email, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            var query = _userRepo.Get(x => x.Email == email);

            if (excludeId != null && excludeId.Value != Guid.Empty)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }

            var isExist = query.Any();

            if (isExist)
            {
                throw new CoreException(ErrorCode.NotUnique, $"Email {email} is already exist, please try another.");
            }
        }
        public Task ChangePasswordAsync(UserPasswordModel model)
        {
            var user = _userRepo.Get(x => x.Id == model.UserId).FirstOrDefault();
            if (user!=null)
            {
                var systemNow = SystemHelper.SystemTimeNow;
                var newPasswordHash = AuthenticationService.HashPassword(model.NewPassword, systemNow);
                //  user.PasswordHash = newPasswordSalt;
                user.PasswordHash = newPasswordHash;

                _userRepo.Update(user, x => x.PasswordHash);
                UnitOfWork.SaveChanges();
            }
            return Task.CompletedTask;
        }

        #endregion
    }
}