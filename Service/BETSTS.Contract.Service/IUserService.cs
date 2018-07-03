#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> IUserService.cs </Name>
//         <Created> 20/04/2018 5:26:05 PM </Created>
//         <Key> 2aabd68f-6cdc-4e1f-9e59-5cea5093571a </Key>
//     </File>
//     <Summary>
//         IUserService.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using BETSTS.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BETSTS.Contract.Service.Base;

namespace BETSTS.Contract.Service
{
    public interface IUserService
    {
        Task<Guid> CreateAsync(Guid userId, CreateUserModel model, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid userId, Guid id, CancellationToken cancellationToken = default);
        Task<UserModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
        GetAllUserModel GetAllUser();
        Task ChangePassword(Guid userId, ChangePasswordModel model, CancellationToken cancellationToken = default);
    }
        
}