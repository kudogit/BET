using BETSTS.Contract.Service;
using BETSTS.Core.Models.Match;
using Elect.Web.IUrlHelperUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BETSTS.Core.Models.User;
using BETSTS.Core.Utils;
using BETSTS.Utils;

namespace BETSTS.Areas.Api.Controllers
{
    [Route(Endpoint)]
    public class MatchController : BaseController
    {
        public const string Endpoint = AreaName + "/match";
        private readonly IMatchService _matchService;
        private readonly IUserService _userService;

        public MatchController(IMatchService matchService, IUserService userService)
        {
            _matchService = matchService;
            _userService = userService;
        }
        
        /// <summary>
        /// Create Match
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("CreateMatch")]
        public async Task<IActionResult> Create([FromBody]MatchModel model)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                var matchId = await _matchService.Create(user.Id,model);
                Uri getDetailUri = new Uri(Url.AbsoluteAction("Get", "Team", new { id = matchId }));
                return Created(getDetailUri, new
                {
                    id = matchId.ToString("N")
                });
            }
            return BadRequest();

        }
        /// <summary>
        /// Get Match By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _matchService.Get(id).ConfigureAwait(true);
            return Ok(result);
        }
        /// <summary>
        /// Get All Match Updated Socre
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAll")]
        public IActionResult GetAll(string filter)
        {
            var listMatch = _matchService.GetAll(filter);
            return Ok(listMatch);
        }
        /// <summary>
        /// Update score
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateScore(Guid matchId,[FromBody] UpdateTeamMatch model)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                _matchService.UpdateScoreTeamMatch(user.Id,matchId, model);
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// Delete match
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                await _matchService.Delete(user.Id,id);
                return Ok();
            }
            return BadRequest();
          
        }
        /// <summary>
        /// Update toal amout All
        /// </summary>
        /// <returns></returns>
        [HttpPut("updateTotal")]
        public async Task<IActionResult> UpdateTotalAll()
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                 await _matchService.UpdateTotalAmoutAll(user.Id);
                return Ok();
            }
            return BadRequest();
 
        }
        /// <summary>
        /// Sent money for user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("sentMoney")]
        public async Task<IActionResult> SentMoney(Guid id,[FromBody] ExchargeModel model)
        {
            var loggedUser = LoggedInUser.Current?.Id;
            if (loggedUser.HasValue)
            {
                var user = await _userService.GetAsync(LoggedInUser.Current.Id)
                    .ConfigureAwait(true);
                await _matchService.SentMoney(user.Id,id,model);
                return Ok();
            }
            return BadRequest();

        }
      
        
    }
}