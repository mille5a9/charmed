using System;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;

namespace ATMBot.Waam
{
    public class Blockdto
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Proof { get; set; }
        public string PreviousHash { get; set; }
    }

    public class Transactiondto
    {
        public int Id { get; set; }
        public int Block_Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public decimal Amount { get; set; }
    }

    public class Walletdto
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public decimal Balance { get; set; }
    }

    public class Blockchain
    {
        public Blockchain()
        {
            //known as the "genesis" block
            NewBlock(100, 0);
        }
        public void NewBlock(uint proof, uint previoushash)
        {
            _chain.Add(new Block(_chain.Count, _currenttransactions, proof, previoushash));
            _currenttransactions.Clear();
        }
        public void NewTransaction(Transaction trans)
        {
            _currenttransactions.Add(trans);
        }
        public decimal BlockReward()
        {
            DateTime old = _chain[_chain.Count - 2].GetTimestamp();
            DateTime oldless = _chain[_chain.Count - 1].GetTimestamp();
            decimal actualresult = (decimal)Math.Pow(0.5, _chain.Count / 1000.0);
            return actualresult;
        }
        public Block LastBlock()
        {
            return _chain[_chain.Count - 1];
        }
        public async Task<bool> ProofOfWork(CommandContext ctx, uint previoushash)
        {
            InteractivityModule inter = ctx.Client.GetInteractivityModule();
            MessageContext msg;
            Random rand = new Random();
            int proof = rand.Next(1000, 1000000);
            uint result = 0;
            string lasthex = previoushash.ToString("X");
            string hex = proof.ToString("X");
            char[] hexes = (lasthex + hex).ToCharArray();
            await ctx.RespondAsync("Please add the following numbers:\n");
            for (int i = 1; i < hexes.Length; i++)
            {
                await ctx.RespondAsync("" + (int)hexes[i - 1] + " + " + (int)hexes[i] + " = ");
                msg = await inter.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(3));
                if (int.TryParse(msg.Message.Content, out int n) && n != (hexes[i - 1] + hexes[i])) return false;
                else result += (uint)n;
            }
            NewBlock(result, previoushash);
            return true;
        }
        private List<Block> _chain = new List<Block>();
        private List<Transaction> _currenttransactions = new List<Transaction>();
        //public void RegisterNode(string host)
        //{
        //    if (nodes.Contains(host) == false) nodes.Add(host);
        //}
        //public bool IsValidChain()
        //{
        //    Block checkblock, block = _chain[0];
        //    int index = 1;

        //    while (index < _chain.Count)
        //    {
        //        checkblock = _chain[index];
        //        if (checkblock.GetPreviousHash() != Hash(block)) return false;
        //        if (IsValidProof(block.GetProof(), checkblock.GetProof()) == false) return false;
        //        block = checkblock;
        //        index++;
        //    }
        //    return true;
        //}
        //public async void ResolveConflicts()
        //{
        //    List<string> neighbors = nodes;
        //    HttpClient client = new HttpClient();
        //    uint maxlength = _chain.Count;

        //    RegisterNode("http://localhost:5000");
        //    RegisterNode("http://localhost:5001");

        //    foreach (string x in nodes)
        //    {
        //        HttpResponseMessage response = await client.GetAsync(x + "/api/waam/chain");
        //        Blockchain body = await response.Content.ReadAsAsync<Blockchain>();
        //        if (body._chain.Count) > _chain.Count) _chain = body._chain;
        //    }
        //}
    }

    [Serializable]
    public struct Block : IComparable
    {
        public Block(int index, List<Transaction> transactions, uint proof, uint previoushash)
        {
            _index = index;
            _timestamp = DateTime.Now;
            _transactions = transactions;
            _proof = proof;
            _previoushash = previoushash;
        }
        public int GetIndex() { return _index; }
        public DateTime GetTimestamp() { return _timestamp; }
        public List<Transaction> GetTransactions() { return _transactions; }
        public uint GetProof() { return _proof; }
        public uint GetPreviousHash() { return _previoushash; }

        public int CompareTo(object obj)
        {
            return _index.CompareTo(obj);
        }

        //proof is the output of the Proof of Work algorithm
        //previoushash is the hash of the previous block
        private readonly int _index;
        private readonly DateTime _timestamp;
        private readonly List<Transaction> _transactions;
        private readonly uint _proof;
        private readonly uint _previoushash; 
    }
    [Serializable]
    public struct Transaction : IComparable
    {
        public Transaction(ulong sender, ulong recipient, decimal amount)
        {
            _sender = sender;
            _recipient = recipient;
            _amount = amount;
        }
        public ulong GetSender() { return _sender; }
        public ulong GetRecipient() { return _recipient; }
        public decimal GetAmount() { return _amount; }

        public int CompareTo(object obj)
        {
            string signature = _sender.ToString() + _recipient.ToString() + _amount;
            return signature.CompareTo(obj);
        }

        private readonly ulong _sender;
        private readonly ulong _recipient;
        private readonly decimal _amount;
    }
}
