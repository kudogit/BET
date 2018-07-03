#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> RequestCodeModel.cs </Name>
//         <Created> 14/05/2018 2:17:52 PM </Created>
//         <Key> b9add98d-1b51-4a37-9ef0-e3d774add465 </Key>
//     </File>
//     <Summary>
//         RequestCodeModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Models.Constants;
using System;

namespace BETSTS.Core.Models.Authentication
{
    public class RequestCodeModel
    {
        public Guid ClientId { get; set; }

        public GrantType GrantType { get; set; } = GrantType.AuthorizationCode;

        /// <summary>
        ///     AuthorizationCode PKCE 
        /// </summary>
        public string CodeChallenge { get; set; }

        /// <summary>
        ///     AuthorizationCode PKCE 
        /// </summary>
        public CodeChallengeMethod CodeChallengeMethod { get; set; } = CodeChallengeMethod.S256;

        public string RedirectUri { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}