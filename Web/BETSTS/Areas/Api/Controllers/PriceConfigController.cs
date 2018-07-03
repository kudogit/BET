using BETSTS.Contract.Service;
using BETSTS.Core.Models.Match;
using Elect.Web.IUrlHelperUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BETSTS.Areas.Api.Controllers
{
    [Route(Endpoint)]
    [Authorize]
    public class PriceConfigController : BaseController
    {
        public const string Endpoint = AreaName + "/priceConfig";
        private readonly IPriceConfigService _priceConfigService;

        public PriceConfigController(IPriceConfigService priceConfigService)
        {
            _priceConfigService = priceConfigService;
        }
        /// <summary>
        /// Create Price Configuration Match
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PriceConfigurationModel model)
        {
            var configId = await _priceConfigService.Create(model);
            Uri getDetailUri = new Uri(Url.AbsoluteAction("Get", "Team", new { id = configId }));
            return Created(getDetailUri, new
            {
                id = configId.ToString("N")
            });
        }
        /// <summary>
        /// Get All Price Config
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var listPriceConfigs = _priceConfigService.GetAll();
            return Ok(listPriceConfigs);
        }
    }
}