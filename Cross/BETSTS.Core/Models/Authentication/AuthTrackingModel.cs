#region	License
//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> AuthTrackingModel.cs </Name>
//         <Created> 01/05/2018 9:24:17 AM </Created>
//         <Key> 5d5022c5-5527-4d66-8d36-8cfecc5fbf5b </Key>
//     </File>
//     <Summary>
//         AuthTrackingModel.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------
#endregion License

using System;
using System.Collections.Generic;

namespace BETSTS.Core.Models.Authentication
{
    public class AuthTrackingModel
    {
        public Guid CurrentUserId { get; set; }

        public List<UserSignInTrackingModel> Users { get; set; } = new List<UserSignInTrackingModel>();
    }
}