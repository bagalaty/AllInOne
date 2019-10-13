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
    /// Sample versioning REST API  
    /// </summary>
    [ApiVersion(ApiRoutes.VersionNumber)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase//AppController
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityContext extityContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BookmarkController(EntityContext context)
        {
            extityContext = context;
        }

        /// <summary>
        /// GET api/values
        /// </summary>
        /// <returns></returns>
       [HttpGet]
        public ActionResult<IEnumerable<Bookmark>> Get()
        {
            var bookmarkList = extityContext.Bookmarks.ToList();
            return bookmarkList;
        }


        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody] Bookmark value)
        {
            var item = value;
            extityContext.Bookmarks.Add(item);
            extityContext.SaveChanges();
        }
    }
}