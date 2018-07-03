using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using BETSTS.Core.Models.Authentication;
using Elect.Core.DictionaryUtils;
using Elect.Core.ObjUtils;
using Elect.Web.Middlewares.HttpContextMiddleware;

namespace BETSTS.Core.Utils
{
    public static class LoggedInUser
    {
        private static string HttpContextItemKey => typeof(LoggedInUser).GetTypeInfo().Assembly.FullName;

        public static LoggedInUserModel Current
        {
            get
            {
                if (HttpContext.Current?.Items != null)
                {
                    return
                        HttpContext.Current.Items.TryGetValue(HttpContextItemKey, out var value)
                            ? value?.ConvertTo<LoggedInUserModel>()
                            : null;
                }

                return null;
            }
            set
            {
                if (HttpContext.Current.Items?.Any() != null)
                {
                    HttpContext.Current.Items = new Dictionary<object, object>();
                }

                if (value == null)
                {
                    if (HttpContext.Current.Items?.ContainsKey(HttpContextItemKey) == true)
                    {
                        HttpContext.Current.Items.Remove(HttpContextItemKey);
                    }

                    return;
                }

                // Add to Current of LoggerInUser
                HttpContext.Current.Items.AddOrUpdate(HttpContextItemKey, value);

                // Add to User in Http Context Too
                var claims = SystemHelper.GetClaimsIdentity(value);
                
                HttpContext.Current.User = new ClaimsPrincipal(claims);
            }
        }
    }
}