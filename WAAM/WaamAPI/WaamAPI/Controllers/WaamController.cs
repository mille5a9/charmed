using Microsoft.AspNetCore.Mvc;
using WaamAPI.Common;

namespace WaamAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WaamController : ControllerBase
    {
        static volatile public Blockchain master = new Blockchain();
        // GET api/values
        [HttpGet("mine")]
        public ActionResult<Block> Mine()
        {
            Block lastblock = master.LastBlock();
            uint lastproof = lastblock.GetProof();
            uint proof = master.ProofOfWork(lastproof);

            master.NewTransaction(new Transaction("0", "5", 1)); //this second argument is where accounts should go once implemented
            string previoushash = Blockchain.Hash(lastblock);
            master.NewBlock(proof, previoushash);

            return master.LastBlock();
        }

        // GET api/values/5
        [HttpGet("chain")]
        public ActionResult<Blockchain> Chain()
        {
            //master.RegisterNode();
            //master.ResolveConflicts();
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

        [HttpPost("register")]
        public IActionResult Register(string node_identifier)
        {
            DoubleLinkedList<string> list = master.GetNodes();
            foreach (string node in list) master.RegisterNode(node);
            master.RegisterNode(node_identifier);
            return StatusCode(202);
        }

        [HttpGet("resolve")]
        public ActionResult<Blockchain> Resolve()
        {
            master.ResolveConflicts();
            return master;
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
