using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GPS.IdentityServer4.Controllers
{
    [Route("[controller]")]

    public class IdentityController : IdentityControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void GenerateToken([FromBody] string value)
        {

        }

        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void VerifyToken([FromBody] string value)
        {

        }
    }
}
