#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> IAuthenticationService.cs </Name>
//         <Created> 21/04/2018 5:45:41 PM </Created>
//         <Key> cbbc3cda-ab65-4ef0-a2ee-7dae4d933a38 </Key>
//     </File>
//     <Summary>
//         IAuthenticationService.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BETSTS.Contract.Service
{
    public interface IAuthenticationService
    {
        // Set Password

        Task<string> SendSetPasswordAsync(Guid id, CancellationToken cancellationToken = default);

        void CheckSetPasswordToken(string token, CancellationToken cancellationToken = default);

        Task SetPasswordAsync(SetPasswordModel model, CancellationToken cancellationToken = default);

        // Confirm Email

        //Task<string> SendConfirmEmailAsync(Guid id, CancellationToken cancellationToken = default);

        //void CheckConfirmEmailToken(string token, CancellationToken cancellationToken = default);

        //Task ConfirmEmailAsync(ConfirmEmailModel model, CancellationToken cancellationToken = default);

        // Authentication

        Task<UserBasicInfoModel> SignInAsync(string userName, string password, CancellationToken cancellationToken = default);

        Task<TokenModel> GetTokenAsync(RequestTokenModel model, CancellationToken cancellationToken = default);

        Task ExpireAllRefreshTokenAsync(Guid id, CancellationToken cancellationToken = default);

        // Code

        /// <summary>
        ///     Generate Code for Grantype Authorization (with PKCE) 
        /// </summary>
        /// <param name="model">            </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> GetCodeAsync(RequestCodeModel model, CancellationToken cancellationToken = default);
    }
}