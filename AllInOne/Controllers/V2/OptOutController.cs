using AllInOne.Contract.V2;
using Microsoft.AspNetCore.Mvc;

namespace AllInOne.Controllers.OptOut.V2
{

    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    [ApiVersion(ApiRoutes.VersionNumber)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OptOutController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiRoutes.OptOut.GetAll)]
        public string Get() => HttpContext.GetRequestedApiVersion().ToString();
    }
}
