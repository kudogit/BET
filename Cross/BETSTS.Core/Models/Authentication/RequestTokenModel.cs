#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> RequestTokenModel.cs </Name>
//         <Created> 22/04/2018 10:57:34 PM </Created>
//         <Key> 7a0fe33e-7123-45ed-a9bb-095849c774e0 </Key>
//     </File>
//     <Summary>
//         RequestTokenModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Constants;
using System;

namespace BETSTS.Core.Models.Authentication
{
    public class RequestTokenModel
    {
        /// <summary>
        ///     GrantType must be ResourceOwner, Authorization Code (PKCE), RefreshToken 
        /// </summary>
        public GrantType GrantType { get; set; }

        public string Role { get; set; }

        //public Guid ClientId { get; set; }

        //public string State { get; set; }

        ///// <summary>
        /////     Authorization Code 
        ///// </summary>
        //public string ClientSecret { get; set; }

        /// <summary>
        ///     Resource Owner Password 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Resource Owner Password 
        /// </summary>
        public string Password { get; set; }

        ///// <summary>
        /////     AuthorizationCode 
        ///// </summary>
        //public string Code { get; set; }

        ///// <summary>
        /////     AuthorizationCode PKCE 
        ///// </summary>
        //public string CodeVerifier { get; set; }

        ///// <summary>
        /////     AuthorizationCode + Implicit 
        ///// </summary>
        //public string RedirectUri { get; set; }

        /// <summary>
        ///     RefreshToken 
        /// </summary>
        public string RefreshToken { get; set; }
    }
}