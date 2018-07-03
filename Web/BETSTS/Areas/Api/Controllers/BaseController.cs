using BETSTS.Attributes.Auth;
using BETSTS.Filters.Auth;
using BETSTS.Filters.Exception;
using BETSTS.Filters.Validation;
using Elect.Web.Models;
using Elect.Web.Swagger.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace BETSTS.Areas.Api.Controllers
{
    [Auth]
    [ShowInApiDoc]
    [Area(AreaName)]
    [Produces(ContentType.Json, ContentType.Xml)]
    [ServiceFilter(typeof(LoggedInUserBinderFilter))]
    [ServiceFilter(typeof(ApiAuthActionFilter))]
    [ServiceFilter(typeof(ApiValidationActionFilterAttribute))]
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    public class BaseController : Controller
    {
        public const string AreaName = "api";
    }
}