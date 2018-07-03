using BETSTS.Contract.Service;
using BETSTS.Core.Models;
using BETSTS.Core.Utils;
using Elect.Web.IUrlHelperUtils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BETSTS.Areas.Api.Controllers
{
    [Route(Endpoint)]
    public class TeamController : BaseController
    {
        public const string Endpoint = AreaName + "/team";
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        /// <summary>
        /// Get All Football Team
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_teamService.GetAll());
            
        }
        
        /// <summary>
        /// Create Football Team
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FootballTeamModel model)
        {
            var teamId = await _teamService.Create(model);
            Uri getDetailUri = new Uri(Url.AbsoluteAction("Get", "Team", new { id = teamId }));
            return Created(getDetailUri, new
            {
                id = teamId.ToString("N")
            });
        }
        /// <summary>
        /// Get Team By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("byId")]
   
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _teamService.Get(id).ConfigureAwait(true);
            return Ok(result);
        }

        /// <summary>
        /// Delete Football Team
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _teamService.Delete(id);
            return NoContent();
        }
        /// <summary>
        /// Update Football Team
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] FootballTeamModel model)
        {
            await _teamService.Update(id, model);
            return NoContent();
        }
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        public IActionResult Test()
        {
            var a = new List<string>();
            var b = DateTime.UtcNow;
            var c = DateTime.Now;
            var d = SystemHelper.UtcToSystemTime(DateTime.UtcNow);
            var e = SystemHelper.GetNetworkTime();
            a.Add("UTC : " + b.ToString() + "Now : " + c + "Test : " + d + "Time Intểnt " + e);
            return Ok(a);
        }
    }
}