using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllInOne.Controllers.V1
{

    [ApiVersionNeutral]
    [Route("api/optout")]
    public class OptOutControler : ControllerBase
    {
        [HttpGet]
        public string Get() => HttpContext.GetRequestedApiVersion().ToString();
    }

    namespace AllInOne.Controllers.OptOut.V1
    {
        [ApiVersion("1.0")]
        [Produces("application/json")]
        [Route("api/Product")]
        [ApiController]
        public class OptOutController : ControllerBase
        {
        }
    }

    namespace AllInOne.Controllers.OptOut.V2
    {
        [ApiVersion("2.0")]
        [Produces("application/json")]
        [Route("api/Product")]
        [ApiController]
        public class OptOutController : ControllerBase
        {
        }
    }
}