#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> AuthorizeModel.cs </Name>
//         <Created> 22/04/2018 11:09:50 PM </Created>
//         <Key> 873dcce2-1aa8-4d41-9f96-639fb39925e6 </Key>
//     </File>
//     <Summary>
//         AuthorizeModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace BETSTS.Core.Models.Authentication
{
    public class AuthorizeModel
    {
        public Guid? ClientId { get; set; }

        /// <summary>
        ///     Redirect URI 
        /// </summary>
        public string Continue { get; set; }

        public GrantType GrantType { get; set; } = GrantType.ResourceOwner;

        public string State { get; set; }

        /// <summary>
        ///     AuthorizationCode PKCE 
        /// </summary>
        public string CodeChallenge { get; set; }

        /// <summary>
        ///     AuthorizationCode PKCE 
        /// </summary>
        public CodeChallengeMethod CodeChallengeMethod { get; set; } = CodeChallengeMethod.Plain;

        /// <summary>
        ///     Hint - pre-enter User Name 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Hint - pre-enter Password 
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}