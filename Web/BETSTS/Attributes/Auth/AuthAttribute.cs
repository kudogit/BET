using System;
using BETSTS.Core.Models.Constants;

namespace BETSTS.Attributes.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthAttribute : Attribute
    {
        public Permission[] Permissions { get; }

        public AuthAttribute(params Permission[] permission)
        {
            Permissions = permission;
        }
    }
}