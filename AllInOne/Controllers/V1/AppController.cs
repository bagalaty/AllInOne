using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AllInOne.Controllers.V1
{
    //[Route("api/[controller]")]
    //[ApiController]
    /// <summary>
    /// 
    /// </summary>
    public class AppController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public static string Version => "2.1.102.AllInOneBranch";

        /// <summary>
        /// 
        /// </summary>
        public static string ProductName => Environment.GetEnvironmentVariable("productName");

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultPage => "/";

        private static Dictionary<string, string> appSettings;
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, string> AppSettings => appSettings;

        /// <summary>
        /// 
        /// </summary>
        public static  IDistributedCache _distributedCache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distributedCache"></param>
        public AppController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;

            appSettings = new Dictionary<string, string>
            {
                { "productName", Environment.GetEnvironmentVariable("productName") },
                { "webpages:Version", "2.0.1" }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public static IdentityUser CurrentUser
        {
            get
            {
                return JsonConvert.DeserializeObject<IdentityUser>(_distributedCache.GetString("user"));
            }
            set
            {
                //Microsoft.AspNetCore.Http.Abstraction.HttpContext.Current.Session.SetString("user", value);
            }
        }

    }
}