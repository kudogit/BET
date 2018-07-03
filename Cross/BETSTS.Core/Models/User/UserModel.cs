#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserModel.cs </Name>
//         <Created> 20/04/2018 5:32:41 PM </Created>
//         <Key> 9270b16c-da24-4442-a03f-bd12b3653bc2 </Key>
//     </File>
//     <Summary>
//         UserModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using Elect.Web.DataTable.Attributes;
using Elect.Web.DataTable.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BETSTS.Core.Models.User
{
    public class CreateUserModel
    {
        [DataTable(DisplayName = "Username", Order = 3, IsVisible = false)]
        public string Username { get; set; }

        [DataTable(DisplayName = "Password", Order = 4)]
        public string Password { get; set; }
    }

    public class UpdateUserModel : CreateUserModel
    {
        [DataTable(IsVisible = false, Order = 1)]
        public Guid Id { get; set; }

        [Display(Name = "User Name")]
        [DataTable(DisplayName = "User Name", Order = 2)]
        public string UserName { get; set; }

        [Display(Name = "Banned")]
        [DataTableIgnore]
        public bool IsBanned { get; set; }

        [Display(Name = "Banned At")]
        [DataTable(DisplayName = "Banned At", Order = 5)]
        public DateTimeOffset? BannedTime { get; set; }

        [Display(Name = "Banned Remark")]
        [DataTableIgnore]
        public string BannedRemark { get; set; }
    }

    public class UserModel : UpdateUserModel
    {
        [Display(Name = "Phone Confirmed At")]
        [DataTableIgnore]
        public DateTimeOffset? PhoneConfirmedTime { get; set; }

        [Display(Name = "Email Confirmed At")]
        [DataTableIgnore]
        public DateTimeOffset? EmailConfirmedTime { get; set; }

        [Display(Name = "Created By")]
        [DataTableIgnore]
        public Guid? CreatedBy { get; set; }

        [Display(Name = "Updated By")]
        [DataTableIgnore]
        public Guid? LastUpdatedBy { get; set; }

        [Display(Name = "Created At")]
        [DataTable(DisplayName = "Created At", Order = 999, IsVisible = false)]
        public DateTimeOffset CreatedTime { get; set; }

        [Display(Name = "Updated At")]
        [DataTable(DisplayName = "Updated At", Order = 1000, SortDirection = SortDirection.Descending)]
        public DateTimeOffset LastUpdatedTime { get; set; }

        [DataTable(DisplayName = "App", Order = 1001, IsSearchable = false)]
        public bool IsApp { get; set; }

        [DataTableIgnore] 
        public string ApplicationName { get; set; }
    }

    public class UserPasswordModel
    {
        public Guid UserId { get; set; }

        public string NewPassword { get; set; }
        public string ConFirmNewPassWord { get; set; }
    }

    public class GetAllUserModel
    {
        public decimal AllTotal { get; set; }
        public decimal AllSent { get; set; }
        public IEnumerable<GetUserModel> Users { get; set; }
    }
    public class GetUserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public decimal Total { get; set; }
        public decimal Sent { get; set; }

    }

    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConFirmNewPassWord { get; set; }
    }
}