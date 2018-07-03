#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> AuthenticationService.cs </Name>
//         <Created> 21/04/2018 5:45:54 PM </Created>
//         <Key> abe00b25-6581-48fa-bbfc-b4ca602b9b9a </Key>
//     </File>
//     <Summary>
//         AuthenticationService.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using BETSTS.Contract.Repository.Interfaces;
using BETSTS.Contract.Repository.Models;
using BETSTS.Contract.Repository.Models.User;
using BETSTS.Contract.Service;
using BETSTS.Core.Exceptions;
using BETSTS.Core.Models.Authentication;
using BETSTS.Core.Models.Constants;
using BETSTS.Core.Utils;
using Elect.Core.DateTimeUtils;
using Elect.Core.SecurityUtils;
using Elect.DI.Attributes;
using Elect.Mapper.AutoMapper.IQueryableUtils;
using Elect.Mapper.AutoMapper.ObjUtils;
using Elect.Web.HttpDetection;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Service
{
    [ScopedDependency(ServiceType = typeof(IAuthenticationService))]
    public class AuthenticationService : Base.Service, IAuthenticationService
    {
        private readonly IServiceProvider _serviceProvider;

        //private readonly IMemoryCache _memoryCache;

        private readonly IRepository<UserEntity> _userRepo;

        private readonly IRepository<RefreshTokenEntity> _refreshTokenRepo;

        private readonly IDataProtector _protector;

        public AuthenticationService(IUnitOfWork unitOfWork,
            IDataProtectionProvider protectionProvider,
            IServiceProvider serviceProvider) : base(unitOfWork)
        {
            _serviceProvider = serviceProvider;
            //_memoryCache = memoryCache;

            _userRepo = unitOfWork.GetRepository<UserEntity>();

            _refreshTokenRepo = unitOfWork.GetRepository<RefreshTokenEntity>();

            _protector = protectionProvider.CreateProtector(SystemHelper.Setting.EncryptKey.ToString("N"));
        }

        // Set Password

        public Task<string> SendSetPasswordAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var expireIn = TimeSpan.FromDays(1);

            var setPasswordTokenExpireTime = SystemHelper.SystemTimeNow.Add(expireIn);

            string setPasswordToken = GenerateToken();

            _userRepo.Update(new UserEntity
            {
                Id = id,
                SetPasswordToken = setPasswordToken,
                SetPasswordTokenExpireTime = setPasswordTokenExpireTime
            }, x => x.SetPasswordToken, x => x.SetPasswordTokenExpireTime);

            UnitOfWork.SaveChanges();

            return Task.FromResult(setPasswordToken);
        }

        public void CheckSetPasswordToken(string token, CancellationToken cancellationToken = default)
        {
            bool isValidToken = IsValidToken(token);

            if (!isValidToken)
            {
                throw new CoreException(ErrorCode.TokenInvalid);
            }

            var checkTime = SystemHelper.SystemTimeNow;

            var setPasswordTokenExpireTime = _userRepo.Get(x => x.SetPasswordToken == token)
                .Select(x => x.SetPasswordTokenExpireTime).FirstOrDefault();

            if (setPasswordTokenExpireTime == null)
            {
                throw new CoreException(ErrorCode.NotFound, "Token is not found.");
            }

            if (setPasswordTokenExpireTime < checkTime)
            {
                throw new CoreException(ErrorCode.TokenExpired);
            }
        }

        public Task SetPasswordAsync(SetPasswordModel model, CancellationToken cancellationToken = default)
        {
            var systemTimeNow = SystemHelper.SystemTimeNow;

            var userEntity = _userRepo.Get(x => x.SetPasswordToken == model.Token).Single();

            userEntity.SetPasswordToken = null;

            userEntity.SetPasswordTokenExpireTime = null;

            userEntity.PasswordLastUpdatedTime = systemTimeNow;

            userEntity.PasswordHash = HashPassword(model.Password, userEntity.PasswordLastUpdatedTime.Value);

            _userRepo.Update(userEntity,
                x => x.SetPasswordToken,
                x => x.SetPasswordTokenExpireTime,
                x => x.PasswordHash,
                x => x.PasswordLastUpdatedTime);

            cancellationToken.ThrowIfCancellationRequested();

            UnitOfWork.SaveChanges();

            return Task.CompletedTask;
        }

        // Confirm Email

        //public async Task<string> SendConfirmEmailAsync(Guid id, CancellationToken cancellationToken = default)
        //{
        //    var expireIn = TimeSpan.FromDays(1);

        //    var confirmEmailTokenExpireTime = SystemHelper.SystemTimeNow.Add(expireIn);

        //    string confirmEmailToken = GenerateToken();

        //    _userRepo.Update(new UserEntity
        //    {
        //        Id = id,
        //        ConfirmEmailToken = confirmEmailToken,
        //        ConfirmEmailTokenExpireTime = confirmEmailTokenExpireTime
        //    }, x => x.ConfirmEmailToken, x => x.ConfirmEmailTokenExpireTime);

        //    UnitOfWork.SaveChanges();

        //    return confirmEmailToken;
        //}

        //public void CheckConfirmEmailToken(string token, CancellationToken cancellationToken = default)
        //{
        //    bool isValidToken = IsValidToken(token);

        //    if (!isValidToken)
        //    {
        //        throw new CoreException(ErrorCode.TokenInvalid);
        //    }

        //    var checkTime = SystemHelper.SystemTimeNow;

        //    var confirmEmailTokenExpireTime = _userRepo.Get(x => x.ConfirmEmailToken == token)
        //        .Select(x => x.ConfirmEmailTokenExpireTime).FirstOrDefault();

        //    if (confirmEmailTokenExpireTime == null)
        //    {
        //        throw new CoreException(ErrorCode.NotFound, "Token is not found.");
        //    }

        //    if (confirmEmailTokenExpireTime < checkTime)
        //    {
        //        throw new CoreException(ErrorCode.TokenExpired);
        //    }
        //}

        //public Task ConfirmEmailAsync(ConfirmEmailModel model, CancellationToken cancellationToken = default)
        //{
        //    var systemTimeNow = SystemHelper.SystemTimeNow;

        //    var userEntity = _userRepo.Get(x => x.ConfirmEmailToken == model.Token).Single();

        //    userEntity.ConfirmEmailToken = null;

        //    userEntity.ConfirmEmailTokenExpireTime = null;

        //    userEntity.UserName = model.UserName;

        //    userEntity.EmailConfirmedTime = userEntity.PasswordLastUpdatedTime = systemTimeNow;

        //    userEntity.PasswordHash = HashPassword(model.Password, userEntity.PasswordLastUpdatedTime.Value);

        //    _userRepo.Update(userEntity,
        //        x => x.ConfirmEmailToken,
        //        x => x.ConfirmEmailTokenExpireTime,
        //        x => x.UserName,
        //        x => x.EmailConfirmedTime,
        //        x => x.PasswordHash,
        //        x => x.PasswordLastUpdatedTime);

        //    cancellationToken.ThrowIfCancellationRequested();

        //    UnitOfWork.SaveChanges();

        //    return Task.CompletedTask;
        //}

        public Task<UserBasicInfoModel> SignInAsync(string userName, string password,
            CancellationToken cancellationToken = default)
        {
            CheckValidSignIn(userName, password);

            var userBasicInfo = GetUserBasicInfoByUserName(userName);

            return Task.FromResult(userBasicInfo);
        }

        // Authentication

        public Task<TokenModel> 
            GetTokenAsync(RequestTokenModel model,
            CancellationToken cancellationToken = default)
        {
            var systemNow = SystemHelper.SystemTimeNow;

            //var appService = _serviceProvider.GetService<IApplicationService>();

            //appService.CheckValid(model.ClientId, model.GrantType);
            var userService = _serviceProvider.GetService<IUserService>();

          
            var tokenModel = new TokenModel
            {
                ExpireIn = systemNow.AddSeconds(3600).ToTimestamp(),
                //State = model.State,
                TokenType = TokenType.AuthTokenType
            };

            switch (model.GrantType)
            {
                case GrantType.AuthorizationCode:
                //case GrantType.AuthorizationCodePKCE:
                //{
                //    AuthorizeCodeStorageModel authorizeCodeStorageModel = GetValidCode(model);

                //    var userBasicInfo = GetUserBasicInfoByUserName(authorizeCodeStorageModel.UserName);

                //    tokenModel.AccessToken = JwtHelper.Generate(userBasicInfo, 3600);

                //    tokenModel.RefreshToken = GenerateRefreshToken(userBasicInfo.Id);

                //    break;
                //}
                //case GrantType.Implicit:
                //{
                //    var userBasicInfo = GetUserBasicInfoByUserName(model.UserName);

                //    tokenModel.AccessToken = JwtHelper.Generate(userBasicInfo, 3600);

                //    break;
                //}
                case GrantType.ResourceOwner:
                {
                    CheckValidSignIn(model.UserName, model.Password);

                    var userBasicInfo = GetUserBasicInfoByUserName(model.UserName);

                    tokenModel.AccessToken = JwtHelper.Generate(userBasicInfo, 3600);

                    tokenModel.RefreshToken = GenerateRefreshToken(userBasicInfo.Id);

                    break;
                }
                case GrantType.RefreshToken:
                {
                    CheckValidRefreshToken(model.RefreshToken);

                    tokenModel.RefreshToken = model.RefreshToken;

                    var userBasicInfo = GetUserBasicInfoByRefreshToken(model.RefreshToken);

                    tokenModel.AccessToken = JwtHelper.Generate(userBasicInfo, 3600);

                    break;
                }
              
            }

            return Task.FromResult(tokenModel);
        }

        public Task ExpireAllRefreshTokenAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var listRefreshToken =
                _refreshTokenRepo.Get(x => x.UserId == id).Select(x =>
                        new RefreshTokenEntity
                        {
                            Id = x.Id
                        })
                    .ToList();

            var systemTimePast = SystemHelper.SystemTimeNow.AddSeconds(-1);

            foreach (var refreshTokenEntity in listRefreshToken)
            {
                refreshTokenEntity.ExpireOn = systemTimePast;

                _refreshTokenRepo.Update(refreshTokenEntity,
                    x => x.RefreshToken,
                    x => x.ExpireOn);
            }

            cancellationToken.ThrowIfCancellationRequested();

            UnitOfWork.SaveChanges();

            return Task.CompletedTask;
        }

        // Code

        //public Task<string> GetCodeAsync(RequestCodeModel model, CancellationToken cancellationToken = default)
        //{
        //    var appService = _serviceProvider.GetService<IApplicationService>();

        //    appService.CheckValid(model.ClientId, model.GrantType);

        //    AuthorizeCodeStorageModel authorizeCodeStorageModel = model.MapTo<AuthorizeCodeStorageModel>();

        //    // Generate Code
        //    authorizeCodeStorageModel.Code = SecurityHelper.GenerateSalt();

        //    // Save to cache
        //    var expireIn = SystemHelper.SystemTimeNow.AddMinutes(SystemHelper.Setting.AuthorizeCodeStorageSeconds);

        //    _memoryCache.Set(authorizeCodeStorageModel.Code, authorizeCodeStorageModel, expireIn);

        //    // Encode to Response
        //    authorizeCodeStorageModel.Code = StringHelper.EncodeBase64Url(authorizeCodeStorageModel.Code);

        //    return Task.FromResult(authorizeCodeStorageModel.Code);
        //}

        #region Helper

        public static string HashPassword(string password, DateTimeOffset hashTime)
        {
            var passwordSalt = hashTime.ToString("O") + SystemHelper.Setting.EncryptKey;

            var passwordHash = SecurityHelper.HashPassword(password, passwordSalt);

            return passwordHash;
        }

        private string GenerateToken()
        {
            var token = _protector.Protect(SecurityHelper.GenerateSalt());

            return token;
        }

        private bool IsValidToken(string token)
        {
            try
            {
                var salt = _protector.Protect(token);

                return !string.IsNullOrWhiteSpace(salt);
            }
            catch
            {
                return false;
            }
        }

        private string GenerateRefreshToken(Guid id)
        {
            var refreshToken = Guid.NewGuid().ToString();

            var refreshTokenEntity = new RefreshTokenEntity
            {
                RefreshToken = refreshToken,
                UserId = id,
                TotalUsage = 1,
                ExpireOn = null
            };

            var deviceInfo = SystemHelper.CurrentHttpContext?.Request.GetDeviceInformation();

            //deviceInfo?.MapTo(refreshTokenEntity);

            _refreshTokenRepo.Add(refreshTokenEntity);

            UnitOfWork.SaveChanges();

            return refreshToken;
        }

        private void CheckValidSignIn(string userName, string password, bool isApp = false)
        {
            var query = _userRepo
                .Get(x =>
                    string.Equals(x.UserName, userName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.OTP, userName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.Phone, userName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.Email, userName, StringComparison.OrdinalIgnoreCase)
                );

            var user = query.Select(x => new
                {
                    x.PasswordHash,
                    x.PasswordLastUpdatedTime,
                    x.BannedTime,
                    x.BannedRemark
                })
                .FirstOrDefault();

            // Check Exist
            if (user == null)
            {
                throw new CoreException(ErrorCode.UserNotFound);
            }

            // Check Password
            if (user.PasswordLastUpdatedTime == null)
            {
                throw new CoreException(ErrorCode.UserPasswordWrong);
            }

            password = HashPassword(password, user.PasswordLastUpdatedTime.Value);

            if (password != user.PasswordHash)
            {
                throw new CoreException(ErrorCode.UserPasswordWrong);
            }

            // Check Banned

            if (user.BannedTime != null)
            {
                throw new CoreException(ErrorCode.UserBanned, user.BannedRemark);
            }
        }

        private void CheckValidRefreshToken(string refreshToken)
        {
            var refreshTokenInfo = _refreshTokenRepo.Get(x => x.RefreshToken == refreshToken).FirstOrDefault();

            if (refreshTokenInfo == null)
            {
                throw new CoreException(ErrorCode.NotFound, "The refresh token is not found.");
            }

            if (refreshTokenInfo.ExpireOn.HasValue && refreshTokenInfo.ExpireOn.Value < SystemHelper.SystemTimeNow)
            {
                throw new CoreException(ErrorCode.TokenExpired, "The refresh token is expired.");
            }
        }

        private UserBasicInfoModel GetUserBasicInfoByUserName(string userName)
        {
            return _userRepo.Get(x =>
                    string.Equals(x.UserName, userName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.Phone, userName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.IsAdmin.ToString(), userName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.Email, userName, StringComparison.OrdinalIgnoreCase))
                .QueryTo<UserBasicInfoModel>()
                .FirstOrDefault();
        }

        private UserBasicInfoModel GetUserBasicInfoByRefreshToken(string refreshToken)
        {
            return _refreshTokenRepo.Get(x => x.RefreshToken == refreshToken).Select(x => x.User)
                .QueryTo<UserBasicInfoModel>().Single();
        }

        //private AuthorizeCodeStorageModel GetValidCode(RequestTokenModel model)
        //{
        //    // Decode to get Data in Cache
        //    model.Code = StringHelper.DecodeBase64Url(model.Code);

        //    _memoryCache.TryGetValue(model.Code, out var data);

        //    var authorizeCodeStorageModel = data as AuthorizeCodeStorageModel;

        //    if (authorizeCodeStorageModel == null)
        //    {
        //        throw new CoreException(ErrorCode.NotFound);
        //    }

        //    if (model.ClientId != authorizeCodeStorageModel.ClientId ||
        //        model.RedirectUri != authorizeCodeStorageModel.RedirectUri)
        //    {
        //        throw new CoreException(ErrorCode.AuthCodeInValid);
        //    }

        //    //if (model.GrantType == GrantType.AuthorizationCode)
        //    //{
        //    //    var appService = _serviceProvider.GetService<IApplicationService>();

        //    //    appService.CheckValid(model.ClientId, model.ClientSecret);
        //    //}
        //    else
        //    {
        //        var codeHash = authorizeCodeStorageModel.CodeChallenge;

        //        if (authorizeCodeStorageModel.CodeChallengeMethod == CodeChallengeMethod.S256)
        //        {
        //            codeHash = SecurityHelper.EncryptSha256(codeHash);
        //        }

        //        if (model.CodeVerifier != codeHash)
        //        {
        //            throw new CoreException(ErrorCode.AuthCodeInValid);
        //        }
        //    }

        //    _memoryCache.Remove(model.Code);

        //    return authorizeCodeStorageModel;
        //}

        public Task<string> GetCodeAsync(RequestCodeModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}