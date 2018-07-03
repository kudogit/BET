using BETSTS.Contract.Service;
using BETSTS.Core.Models.User;
using BETSTS.Core.Utils;
using BETSTS.Utils;
using Elect.Web.IUrlHelperUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Threading.Tasks;

namespace BETSTS.Areas.Api.Controllers
{
    [Route(Endpoint)]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public const string Endpoint = AreaName + "/users";

        public const string GetPagedEndpoint = "";

        public const string GetEndpoint = "{id}";

        public const string ProfileEndpoint = "profile";

        public const string CreateEndpoint = "";

        public const string UpdateEndpoint = "";

        public const string DeleteEndpoint = "{id}";

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
     

        /// <summary>
        ///     Get Detail User 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(GetEndpoint)]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(UserModel), "User detail information.")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userService.GetAsync(id, this.GetRequestCancellationToken()).ConfigureAwait(true);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        ///     Get Logged In User Profile: Basic info and permissions.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(ProfileEndpoint)]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status200OK, typeof(UserModel), "User profile information.")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userService.GetAsync(LoggedInUser.Current.Id, this.GetRequestCancellationToken())
                .ConfigureAwait(true);

            return Ok(user);
        }

        /// <summary>
        ///     Create User 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(CreateEndpoint)]
        [SwaggerResponse(StatusCodes.Status201Created, typeof(Guid), "Id of new User.")]
        public async Task<IActionResult> Create([FromBody] CreateUserModel model)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                var userId = await _userService.CreateAsync(user.Id,model, this.GetRequestCancellationToken()).ConfigureAwait(true);

                Uri getDetailUri = new Uri(Url.AbsoluteAction("Get", "User", new { id = userId }));

                return Created(getDetailUri, new
                {
                    id = userId.ToString("N")
                });
            }
            return BadRequest();
        }

      

        /// <summary>
        ///     Delete User 
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route(DeleteEndpoint)]
        [SwaggerResponse(StatusCodes.Status204NoContent, null, "User deleted.")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                 await _userService.DeleteAsync(user.Id, id, this.GetRequestCancellationToken()).ConfigureAwait(true);

                return NoContent();
            }
            return BadRequest();
            
        }
        /// <summary>
        /// Get All User with Amout
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var listUser = _userService.GetAllUser();
            return Ok(listUser);
        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("ChangePass")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                await _userService.ChangePassword(user.Id, model, this.GetRequestCancellationToken()).ConfigureAwait(true);

                return Ok();
            }
            return BadRequest();
        }
    }
}