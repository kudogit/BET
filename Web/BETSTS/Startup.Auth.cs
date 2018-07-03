using BETSTS.Filters.Auth;
using Elect.DI;
using Microsoft.Extensions.DependencyInjection;

namespace BETSTS
{
    public static class StartupAuth
    {
        /// <summary>
        ///     Binding Logged In User, MVC and API Authentication Filter
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddScopedIfNotExist<ApiAuthActionFilter>();
           // services.AddScopedIfNotExist<MvcAuthActionFilter>();
            services.AddScopedIfNotExist<LoggedInUserBinderFilter>();

            return services;
        }
    }
}