using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BETSTS.Attributes.Auth;
using BETSTS.Core.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BETSTS.Filters.Auth
{
    public static class FilterContextExtensions
    {
        /// <summary>
        ///     Check user is authentication, also check AllowAnonymous attribute. 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsAuthenticated(this FilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)) return true;

            // If allow anonymous => the user/request is authenticated
            if (IsActionAllowAnonymous(controllerActionDescriptor))
            {
                return true;
            }

            // Check is user pass authentication
            return IsUserAuthenticated(context.HttpContext);
        }

        /// <summary>
        ///     Check user is authorization. Please call <see cref="IsAuthenticated" /> to check
        ///     authenticated before call this method.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsAuthorized(this FilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)) return true;

            // If allow anonymous => the user/request is authorized
            if (IsActionAllowAnonymous(controllerActionDescriptor))
            {
                return true;
            }

            List<AuthAttribute> listAuthorizeAttribute = new List<AuthAttribute>();

            var actionAttributes =
                controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthAttribute>(true).ToList();

            if (actionAttributes.Any())
            {
                listAuthorizeAttribute.AddRange(actionAttributes);
            }

            // If Action combine Controller authorize or Action not have any Authorize Attribute,
            // then add allow role of controller to the list allow role.
            bool isCombineAuthorize = controllerActionDescriptor.MethodInfo
                .GetCustomAttributes<CombineAuthAttribute>(true).Any();

            if (isCombineAuthorize || !listAuthorizeAttribute.Any())
            {
                var controllerAttributes =
                    controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthAttribute>(true);
                listAuthorizeAttribute.AddRange(controllerAttributes);
            }

            // If list attribute or list allow role don't have any thing => User is authorized
            if (!listAuthorizeAttribute.Any() ||
                listAuthorizeAttribute.SelectMany(x => x.Permissions).Any() != true) return true;

            // Apply rule AND conditional for list attribute, OR conditional for role into an attribute

            // Only check attribute have role
            listAuthorizeAttribute = listAuthorizeAttribute.Where(x => x.Permissions?.Any() == true).ToList();

            bool isAuthorized =
                listAuthorizeAttribute.All(x => x.Permissions.Any(p => LoggedInUser.Current.Permissions.Contains(p)));

            return isAuthorized;
        }

        private static bool IsActionAllowAnonymous(ControllerActionDescriptor controllerActionDescriptor)
        {
            // If action have any AllowAnonymousAttribute => Allow Anonymous
            bool isActionAllowAnonymous =
                controllerActionDescriptor.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>(true).Any();

            if (isActionAllowAnonymous) return true;

            var isActionHaveAnyPermission =
                controllerActionDescriptor.MethodInfo.GetCustomAttributes<AuthAttribute>(true).Any();

            bool isCombineAuthorize =
                controllerActionDescriptor.MethodInfo.GetCustomAttributes<CombineAuthAttribute>(true).Any();

            if (!isCombineAuthorize && isActionHaveAnyPermission)
            {
                return false;
            }

            bool isControllerAllowAnonymous =
                controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AllowAnonymousAttribute>(true).Any();

            if (isControllerAllowAnonymous)
            {
                return true;
            }

            if (!isActionHaveAnyPermission)
            {
                var isControllerHaveAnyRole =
                    controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthAttribute>(true).Any();

                return !isControllerHaveAnyRole;
            }

            return false;
        }

        private static bool IsUserAuthenticated(HttpContext context)
        {
            return LoggedInUser.Current != null;
        }
    }
}