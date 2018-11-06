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
        static volatile public Blockchain master = new Blockchain();
        string node_identifier = Guid.NewGuid().ToString();
        // GET api/values
        [HttpGet("mine")]
        public ActionResult<Block> Mine()
        {
            Block lastblock = master.LastBlock();
            uint lastproof = lastblock.GetProof();
            uint proof = master.ProofOfWork(lastproof);

            master.NewTransaction(new Transaction("0", node_identifier, 1)); //this second argument is where accounts should go once implemented
            string previoushash = Blockchain.Hash(lastblock);
            master.NewBlock(proof, previoushash);
            return master.LastBlock();
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("chain")]
        public ActionResult<Blockchain> Chain()
        {
            return master;
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
