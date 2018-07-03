#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> GrantType.cs </Name>
//         <Created> 16/04/2018 8:36:59 PM </Created>
//         <Key> a7adf62c-d5a8-4561-9e82-7f3fb33e87cb </Key>
//     </File>
//     <Summary>
//         GrantType.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BETSTS.Core.Models.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GrantType
    {

        [Display(Name = "Resouce Owner Password")]
        ResourceOwner,
        [Display(Name = "Client Credential")]
        ClientCredential,

        [Display(Name = "Authorization Code")]
        AuthorizationCode,

        [Display(Name = "Authorization Code with PKCE")]
        AuthorizationCodePKCE,

        [Display(Name = "Implicit")]
        Implicit,


        [Display(Name = "Refresh Token")]
        RefreshToken
    }
    public enum Role
    {

        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "Manager")]
        Manager,

        [Display(Name = "Member")]
        Member,

        [Display(Name = "MemberVip")]
        MemberVip,
    }
}