#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> SetPasswordModel.cs </Name>
//         <Created> 21/04/2018 5:57:36 PM </Created>
//         <Key> 5d13de7d-0d80-4d23-b7d1-5ce9f670f90d </Key>
//     </File>
//     <Summary>
//         SetPasswordModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System.ComponentModel.DataAnnotations;

namespace BETSTS.Core.Models.Authentication
{
    public class SetPasswordModel
    {
        public string Token { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}