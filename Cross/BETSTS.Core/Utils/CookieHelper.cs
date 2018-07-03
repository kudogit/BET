#region	License

//--------------------------------------------------
// <License>
//     <Copyright> 2018 © Top Nguyen </Copyright>
//     <Url> http://topnguyen.net/ </Url>
//     <Author> Top </Author>
//     <Project> Elate </Project>
//     <File>
//         <Name> CookieHelper.cs </Name>
//         <Created> 30/04/2018 11:00:01 AM </Created>
//         <Key> 78ca020e-3747-4d79-a9f8-e040ed91f162 </Key>
//     </File>
//     <Summary>
//         CookieHelper.cs is a part of Elate
//     </Summary>
// <License>
//--------------------------------------------------

#endregion License

using BETSTS.Core.Models.Authentication;
using Elect.Core.LinqUtils;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;

namespace BETSTS.Core.Utils
{
    public class CookieHelper
    {
        private const string AuthKey = "Auth";

        public static void Set<T>(string key, T data, HttpContext httpContext,
            IDataProtectionProvider protectionProvider) where T : class, new()
        {
            var protector = GetDataProtector(protectionProvider);

            var dataJson = JsonConvert.SerializeObject(data, Models.Constants.Formatting.JsonSerializerSettings);

            var protectedDataJson = protector.Protect(dataJson);

            httpContext.Response.Cookies.Append(key, protectedDataJson, new CookieOptions
            {
                Expires = null,
                HttpOnly = true,
                Secure = false,
            });
        }

        public static T Get<T>(string key, HttpContext httpContext, IDataProtectionProvider protectionProvider)
            where T : class, new()
        {
            var protector = GetDataProtector(protectionProvider);

            if (!httpContext.Request.Cookies.TryGetValue(key, out var protectedDataJson))
            {
                return default;
            }

            string dataJson;

            try
            {
                dataJson = protector.Unprotect(protectedDataJson);
            }
            catch
            {
                return default;
            }

            var data = JsonConvert.DeserializeObject<T>(dataJson, Models.Constants.Formatting.JsonSerializerSettings);

            return data;
        }

        public static AuthTrackingModel SetAuthTracking(UserSignInTrackingModel userSignInTracking,
            HttpContext httpContext, IDataProtectionProvider protectionProvider)
        {
            // Get Cookie

            var authTracking = Get<AuthTrackingModel>(AuthKey, httpContext, protectionProvider) ??
                               new AuthTrackingModel();

            authTracking.Users = authTracking.Users.RemoveWhere(x => x.Id == userSignInTracking.Id).ToList();

            authTracking.CurrentUserId = userSignInTracking.Id;

            authTracking.Users.Add(userSignInTracking);

            // Set Cookie

            Set(AuthKey, authTracking, httpContext, protectionProvider);

            return authTracking;
        }

        public static AuthTrackingModel GetAuthTracking(HttpContext httpContext,
            IDataProtectionProvider protectionProvider)
        {
            var authTracking = Get<AuthTrackingModel>(AuthKey, httpContext, protectionProvider) ??
                               new AuthTrackingModel();

            return authTracking;
        }

        private static IDataProtector GetDataProtector(IDataProtectionProvider protectionProvider)
        {
            return protectionProvider.CreateProtector(SystemHelper.Setting.EncryptKey.ToString("N"));
        }
    }
}