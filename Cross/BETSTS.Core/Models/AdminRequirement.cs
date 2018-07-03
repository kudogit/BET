using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Threading.Tasks;
using BETSTS.Core.Exceptions;

namespace BETSTS.Core
{
    public class AdminRequirement : AuthorizationHandler<AdminRequirement>, IAuthorizationRequirement
    {
        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            var otp = context.User.Claims.FirstOrDefault(c => c.Type == "otp")?.Value;
            if (otp == "Admin")
            {
                context.Succeed(this);
                return Task.CompletedTask;
            }
            context.Fail();

            throw new CoreException(ErrorCode.UnAuthenticated,"May khong duoc vao");
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            throw new NotImplementedException();
        }
    }
}



