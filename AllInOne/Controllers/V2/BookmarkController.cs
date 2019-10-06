using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllInOne.Contract.V2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace AllInOne.Controllers.V2
{
    /// <summary>
    /// 
    /// </summary>
    [ApiVersion(ApiRoutes.VersionNumber)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
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
        [HttpGet(ApiRoutes.Bookmark.GetAll)]
        public ActionResult<IEnumerable<Bookmark>> Get()
        {
            var bookmarkList = extityContext.Bookmarks.ToList();
            return bookmarkList;
        }
    }
}