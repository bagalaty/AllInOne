using AllInOne.Contract.V2;
using Microsoft.AspNetCore.Mvc;

namespace AllInOne.Controllers.OptOut.V2
{

    /// <summary>
    /// 
    /// </summary>
    [ApiVersion(ApiRoutes.VersionNumber)]
    [Produces("application/json")]
    [Route("api/[controller]")]
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
