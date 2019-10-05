using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllInOne.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class ProductController : ControllerBase
    //{
    //}

    [ApiVersionNeutral]
    [Route("api/optout")]
    public class OptOutControler : ControllerBase
    {
        [HttpGet]
        public string Get() => HttpContext.GetRequestedApiVersion().ToString();
    }

    namespace AllInOne.Controllers.Product.V1
    {
        [ApiVersion("1.0")]
        [Produces("application/json")]
        [Route("api/Product")]
        [ApiController]
        public class ProductController : ControllerBase
        {
        }
    }

    namespace AllInOne.Controllers.Product.V2
    {
        [ApiVersion("2.0")]
        [Produces("application/json")]
        [Route("api/Product")]
        [ApiController]
        public class ProductController : ControllerBase
        {
        }
    }
}