using System;
using System.Security.Cryptography;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            return mySHA256.ComputeHash(blockbytes).ToString();
        }
        public Block LastBlock()
        {
            return _chain.GetItem(_chain.Size());
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
            string guesshash = mySHA256.ComputeHash(guess).ToString();
            return (guesshash.Substring(Math.Max(0, guesshash.Length - 4)) == "0000");
        }
        private DoubleLinkedList<Block> _chain = new DoubleLinkedList<Block>();
        private DoubleLinkedList<Transaction> _currenttransactions = new DoubleLinkedList<Transaction>();
    }

    public struct Block
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

        //proof is the output of the Proof of Work algorithm
        //previoushash is the hash of the previous block
        private readonly uint _index;
        private readonly DateTime _timestamp;
        private readonly DoubleLinkedList<Transaction> _transactions;
        private readonly uint _proof;
        private readonly string _previoushash; 
    }

    public struct Transaction
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
        private readonly string _sender;
        private readonly string _recipient;
        private readonly decimal _amount;
    }
}
