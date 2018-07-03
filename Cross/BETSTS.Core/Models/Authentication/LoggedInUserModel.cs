using System.Collections.Generic;
using BETSTS.Core.Models.Constants;

namespace BETSTS.Core.Models.Authentication
{
    public class LoggedInUserModel : UserBasicInfoModel
    {
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}