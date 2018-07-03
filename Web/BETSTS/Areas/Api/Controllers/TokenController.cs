using BETSTS.Contract.Service;
using BETSTS.Core.Exceptions;
using BETSTS.Core.Models.Authentication;
using BETSTS.Core.Utils;
using BETSTS.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BETSTS.Areas.Api.Controllers
{
    [Route(Endpoint)]
    [AllowAnonymous]
    public class TokenController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public const string Endpoint = AreaName + "/token";

        public TokenController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        /// <summary>
        /// Get Token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] RequestTokenModel model)
        {
            try
            {
                GrantTypeHelper.CheckAllowGenerateToken(model.GrantType);

                var tokenModel = await _authenticationService.GetTokenAsync(model, this.GetRequestCancellationToken())
                    .ConfigureAwait(true);

                return Ok(tokenModel);
            }
            catch (CoreException e)
            {
                ErrorModel errorModel = new ErrorModel(e.Code, e.Message, e.AdditionalData);

                return BadRequest(errorModel);
            }
        }
        /// <summary>
        /// Set Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/setPassword")]
        public async Task<IActionResult> SetPassword(SetPasswordModel model)
        {
            await _authenticationService.SetPasswordAsync(model, this.GetRequestCancellationToken())
                .ConfigureAwait(true);
            return Ok();
        }
    }
}