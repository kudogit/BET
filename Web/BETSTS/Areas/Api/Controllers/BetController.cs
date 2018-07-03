using BETSTS.Contract.Service;
using BETSTS.Core.Models.User;
using BETSTS.Core.Utils;
using BETSTS.Utils;
using Elect.Web.IUrlHelperUtils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BETSTS.Areas.Api.Controllers
{
    [Route(Endpoint)]
    public class BetController : BaseController
    {
        public const string Endpoint = AreaName + "/bet";
        private readonly IUserBetService _userBetService;
        private readonly IUserService _userService;

        public BetController(IUserBetService userBetService, IUserService userService)
        {
            _userBetService = userBetService;
            _userService = userService;
        }
        /// <summary>
        /// Create Bet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]UserBetModel model)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                var betId = _userBetService.Create(model, user.Id, this.GetRequestCancellationToken()).ConfigureAwait(true); 
                Uri getDetailUri = new Uri(Url.AbsoluteAction("Get", "Bet", new { id = betId }));

                return Created(getDetailUri, new
                {
                    id = betId
                });
            }
            return BadRequest();
        }
        /// <summary>
        /// Update User Bet
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateBetModel model)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                await _userBetService.Update(model, user.Id, this.GetRequestCancellationToken()).ConfigureAwait(true); ;
                return NoContent();
            }
            return BadRequest();
        }
        /// <summary>
        /// Get Bet User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userBetService.Get(id).ConfigureAwait(true); ;
            return Ok(result);
        }
        /// <summary>
        /// Get All User Bet
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllUserBet")]
        public async Task<IActionResult> GetAllUserBet()
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                var listBet = _userBetService.GetAllBet(user.Id);
                return Ok(listBet);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                var listBet = _userBetService.GetAll(user.Id);
                return Ok(listBet);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get History Bet
        /// </summary>
        /// <returns></returns>
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                var listBet = _userBetService.GetHistory(user.Id);
                return Ok(listBet);
            }
            return BadRequest();
        }
        /// <summary>
        /// Get History Paid
        /// </summary>
        /// <returns></returns>
        [HttpPost("historyPaid")]
        public async Task<IActionResult> HistoryPaid()
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                var listBet = _userBetService.GetHistoryExchange(user.Id);
                return Ok(listBet);
            }
            return BadRequest();
        }

    }
}