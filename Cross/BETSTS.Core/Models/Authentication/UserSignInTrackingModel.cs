#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> UserSignInTrackingModel.cs </Name>
//         <Created> 01/05/2018 9:25:06 AM </Created>
//         <Key> 8889358c-c07c-4863-a54f-e1ee1934c4d6 </Key>
//     </File>
//     <Summary>
//         UserSignInTrackingModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using BETSTS.Core.Utils;
using System;

namespace BETSTS.Core.Models.Authentication
{
    public class UserSignInTrackingModel : UserBasicInfoModel
    {
        public DateTimeOffset LastUpdatedTime { get; set; } = SystemHelper.SystemTimeNow;

        public bool IsLoggedIn { get; set; } = true;
    }
}