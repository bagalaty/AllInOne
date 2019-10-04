//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace AllInOne.Controllers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    [ApiVersion("2.0")]
//    [ApiVersion("3.0")]
//    [ApiVersion("1.0", Deprecated = true)]
//    [Route("api/helloworld/v{version:apiVersion}")]
//    public class HelloWorld2Controller : Controller
//    {
//        /// <summary>
//        /// Get Hello world v2
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public string Get() => "Hello world v2!";

//        /// <summary>
//        /// Get Hello world v3
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet, MapToApiVersion("3.0")]
//        public string GetV3() => "Hello world v3!";
//    }
//}
