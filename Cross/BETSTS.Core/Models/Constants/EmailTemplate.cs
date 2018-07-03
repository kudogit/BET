#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> EmailTemplate.cs </Name>
//         <Created> 21/04/2018 9:30:59 PM </Created>
//         <Key> c3a28efc-23d1-40cf-9e10-2df25e1b4403 </Key>
//     </File>
//     <Summary>
//         EmailTemplate.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace BETSTS.Core.Models.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EmailTemplate
    {
        /// <summary>
        ///     This email will be sent whenever a user signs up or admin invite a user. 
        /// </summary>
        [Display(Name = "Verify Email")]
        VerifyEmail,

        /// <summary>
        ///     This email will be sent once the user make a first success login. 
        /// </summary>
        [Display(Name = "Welcome")]
        Welcome,

        /// <summary>
        ///     This email will be sent whenever a user requests a password change. The password will
        ///     not be changed until the user follows the verification link in the email.
        /// </summary>
        [Display(Name = "Change Password")]
        ChangePassword,
    }
}