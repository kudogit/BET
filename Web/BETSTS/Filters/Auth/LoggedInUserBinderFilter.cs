using System.Linq;
using BETSTS.Core.Models.Authentication;
using BETSTS.Core.Models.Constants;
using BETSTS.Core.Utils;
using Elect.Mapper.AutoMapper.ObjUtils;
using Elect.Web.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BETSTS.Filters.Auth
{
    public class LoggedInUserBinderFilter : IAuthorizationFilter
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            BindLoggedInUser(context.HttpContext);
        }

        public LoggedInUserBinderFilter(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public void BindLoggedInUser(HttpContext httpContext)
        {
            try
            {
                if (httpContext == null)
                {
                    return;
                }

                var loggedInUserBasicInfo = GetLoggedInUserBasicInfo(httpContext);

                if (loggedInUserBasicInfo == null)
                {
                    return;
                }

                LoggedInUser.Current = loggedInUserBasicInfo.MapTo<LoggedInUserModel>();
                
                // TODO get Permission for LoggedInUserModel
            }
            catch (System.Exception)
            {
                // Ignore
            }
        }

        public UserBasicInfoModel GetLoggedInUserBasicInfo(HttpContext httpContext)
        {
            UserBasicInfoModel basicUserInfoModel = null;

            // Check User Basic Info in Token of Header
            var token = httpContext.Request.Headers[HeaderKey.Authorization].ToString();

            if (!string.IsNullOrWhiteSpace(token))
            {
                token = token.Replace(TokenType.AuthTokenType, string.Empty)?.Trim();

                var isValid = JwtHelper.IsValid(token);

                var isExpire = JwtHelper.IsExpire(token);

                if (isValid && !isExpire)
                {
                    basicUserInfoModel = JwtHelper.Get<UserBasicInfoModel>(token);
                }
            }
            else
            {
                // Check Logged User in Cookie
                AuthTrackingModel authTracking = CookieHelper.GetAuthTracking(httpContext, _dataProtectionProvider);

                var currentUserSignInTrackingModel =
                    authTracking.Users.FirstOrDefault(x => x.IsLoggedIn && authTracking.CurrentUserId == x.Id);

                basicUserInfoModel = currentUserSignInTrackingModel?.MapTo<UserBasicInfoModel>();
            }

            return basicUserInfoModel;
        }
    }
}