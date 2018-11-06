using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WaamAPI.Common;

namespace WaamAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WaamController : ControllerBase
    {
        Blockchain master = new Blockchain();
        // GET api/values
        [HttpGet("mine")]
        public ActionResult<string> Mine()
        {
            return "This API URL will mine a new Block for the chain";
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("chain")]
        public ActionResult<string> Chain()
        {
            return "This API URL will return the current chain";
        }

        // POST api/values
        [HttpPost("new")]
        public IActionResult NewTransaction(Transaction trans)
        {
            if (trans.GetSender() == null || trans.GetRecipient() == null || trans.GetAmount() == 0 ) return StatusCode(400);
            master.NewTransaction(trans);
            return StatusCode(202);
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
