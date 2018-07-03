#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserBasicInfoModel.cs </Name>
//         <Created> 23/04/2018 5:32:58 PM </Created>
//         <Key> 3155fa29-379d-4ba8-9868-1fd221942f98 </Key>
//     </File>
//     <Summary>
//         UserBasicInfoModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;

namespace BETSTS.Core.Models.Authentication
{
    public class UserBasicInfoModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public int IsAdmin { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public DateTime? Birthday { get; set; }
    }
}