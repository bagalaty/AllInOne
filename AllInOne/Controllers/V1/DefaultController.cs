using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllInOne.Contract.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace AllInOne.Controllers.V1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion(ApiRoutes.VersionNumber)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        /// <summary>
        /// 
        /// </summary>
        public EntityContext extityContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public DefaultController(EntityContext context)
        {
            extityContext = context;
        }

        [ApiVersion("1.0")]
        [HttpGet]
        public ActionResult<IEnumerable<Bookmark>> Get()
        {
            var bookmarkList = extityContext.Bookmarks.ToList();
            return bookmarkList;
        }
    }
}