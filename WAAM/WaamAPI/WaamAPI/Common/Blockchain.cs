using System;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Http;

namespace WaamAPI.Common
{

    public class Blockchain
    {
        public Blockchain()
        {
            //known as the "genesis" block
            NewBlock(100, "1");
        }
        public void NewBlock(uint proof, string previoushash = null)
        {
            _chain.Append(new Block(_chain.Size(), _currenttransactions, proof, previoushash));
            _currenttransactions.Clear();
        }
        public void NewTransaction(Transaction trans)
        {
            _currenttransactions.Append(trans);
        }
        public static string Hash(Block block)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memStream, block);
            byte[] blockbytes = memStream.ToArray();
            SHA256 mySHA256 = SHA256.Create();
            byte[] answer = mySHA256.ComputeHash(blockbytes);
            string guesshash = string.Empty;
            foreach (byte x in answer) guesshash += String.Format("{0:x2}", x);
            return guesshash;
        }
        public Block LastBlock()
        {
            return _chain.GetItem(_chain.Size() - 1);
        }
        public uint ProofOfWork(uint lastproof)
        {
            uint proof = 0;
            while (IsValidProof(lastproof, proof) == false) proof += 1;
            return proof;
        }
        private bool IsValidProof(uint lastproof, uint proof)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memStream, lastproof);
            byte[] lprf = memStream.ToArray();
            bf.Serialize(memStream, proof);
            byte[] prf = memStream.ToArray();
            byte[] guess = new byte[lprf.Length + prf.Length];
            Buffer.BlockCopy(lprf, 0, guess, 0, lprf.Length);
            Buffer.BlockCopy(prf, 0, guess, lprf.Length, prf.Length);
            SHA256 mySHA256 = SHA256.Create();
            byte[] answer = mySHA256.ComputeHash(guess);
            string guesshash = string.Empty;
            foreach (byte x in answer) guesshash += String.Format("{0:x2}", x);
            return (guesshash.Substring(Math.Max(0, guesshash.Length - 4)) == "0000");
        }
        public void RegisterNode(string host)
        {
            if (nodes.Contains(host) == false) nodes.Append(host);
        }
        public bool IsValidChain()
        {
            Block block = _chain.GetItem(0), checkblock;
            uint index = 1;

            while (index < _chain.Size())
            {
                checkblock = _chain.GetItem(index);
                if (checkblock.GetPreviousHash() != Hash(block)) return false;
                if (IsValidProof(block.GetProof(), checkblock.GetProof()) == false) return false;
                block = checkblock;
                index++;
            }
            return true;
        }
        public async void ResolveConflicts()
        {
            DoubleLinkedList<string> neighbors = nodes;
            HttpClient client = new HttpClient();
            uint maxlength = _chain.Size();

            RegisterNode("http://localhost:5000");
            RegisterNode("http://localhost:5001");

            foreach (string x in nodes)
            {
                HttpResponseMessage response = await client.GetAsync(x + "/api/waam/chain");
                Blockchain body = await response.Content.ReadAsAsync<Blockchain>();
                if (body._chain.Size() > _chain.Size()) _chain = body._chain;
            }
        }
        public DoubleLinkedList<string> GetNodes() { return nodes; }
        private DoubleLinkedList<Block> _chain = new DoubleLinkedList<Block>();
        private DoubleLinkedList<Transaction> _currenttransactions = new DoubleLinkedList<Transaction>();
        private DoubleLinkedList<string> nodes = new DoubleLinkedList<string>();
    }

    [Serializable]
    public struct Block : IComparable
    {
        public Block(uint index, DoubleLinkedList<Transaction> transactions, uint proof, string previoushash = null)
        {
            _index = index;
            _timestamp = DateTime.Now;
            _transactions = transactions;
            _proof = proof;
            _previoushash = previoushash;
        }
        public uint GetIndex() { return _index; }
        public DateTime GetTimestamp() { return _timestamp; }
        public DoubleLinkedList<Transaction> GetTransactions() { return _transactions; }
        public uint GetProof() { return _proof; }
        public string GetPreviousHash() { return _previoushash; }

        public int CompareTo(object obj)
        {
            return _index.CompareTo(obj);
        }

        //proof is the output of the Proof of Work algorithm
        //previoushash is the hash of the previous block
        private readonly uint _index;
        private readonly DateTime _timestamp;
        private readonly DoubleLinkedList<Transaction> _transactions;
        private readonly uint _proof;
        private readonly string _previoushash; 
    }
    [Serializable]
    public struct Transaction : IComparable
    {
        public Transaction(string sender, string recipient, decimal amount)
        {
            _sender = sender;
            _recipient = recipient;
            _amount = amount;
        }
        public string GetSender() { return _sender; }
        public string GetRecipient() { return _recipient; }
        public decimal GetAmount() { return _amount; }

        public int CompareTo(object obj)
        {
            string signature = _sender + _recipient + _amount;
            return signature.CompareTo(obj);
        }

        private readonly string _sender;
        private readonly string _recipient;
        private readonly decimal _amount;
    }
}
