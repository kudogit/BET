#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserProfileEntity.cs </Name>
//         <Created> 16/04/2018 8:51:43 PM </Created>
//         <Key> d9abb173-af85-4bf2-857f-7aa85cab3b12 </Key>
//     </File>
//     <Summary>
//         UserProfileEntity.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.ComponentModel.DataAnnotations;

namespace BETSTS.Contract.Repository.Models.User
{
    public class UserProfileEntity : Entity
    {
        public string FullName { get; set; }

        public string Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        // User

        public virtual UserEntity User { get; set; }
    }
}