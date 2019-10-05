using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Models;

namespace AllInOne.Controllers
{
    /// <summary>
    /// Values Controller
    /// </summary>
    [ApiVersion("2.0")]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public EntityContext extityContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ValuesController(EntityContext context)
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
            var bookmarkList=extityContext.Bookmarks.ToList();
            return bookmarkList;
        }

        /// <summary>
        ///  GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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

        /// <summary>
        /// PUT api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// DELETE api/values/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }




   
}
