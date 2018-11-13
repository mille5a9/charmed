using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

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
        public string Username { get; set; }
        public decimal Balance { get; set; }
    }

    public class Blockchain
    {
        public Blockchain()
        {
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                List<Blockdto> blockdtos = db.Query<Blockdto>("SELECT * FROM Blocks").ToList();
                List<Block> blocks = new List<Block>();
                foreach (Blockdto x in blockdtos)
                {
                    bool a = uint.TryParse(x.Proof, out uint n);
                    a = uint.TryParse(x.PreviousHash, out uint m);
                    List<Transactiondto> transactiondtos = db.Query<Transactiondto>("SELECT * FROM Transactions").ToList();
                    List<Transaction> transactions = new List<Transaction>();
                    foreach (Transactiondto z in transactiondtos)
                    {
                        a = ulong.TryParse(z.Sender, out ulong send);
                        a = ulong.TryParse(z.Recipient, out ulong recip);
                        transactions.Add(new Transaction(send, recip, z.Amount));
                    }
                    Block y = new Block(Count(), transactions, n, m);
                    blocks.Add(y);
                }
                if (blockdtos.Count == 0) NewBlock(100, 0);
                else _chain = blocks;
            }
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
        public int Count() { return _chain.Count; }
        private List<Block> _chain = new List<Block>();
        private List<Transaction> _currenttransactions = new List<Transaction>();
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

    public class Wallet
    {
        public static Walletdto GetWallet(ulong user_id, string username) // for sender
        {
            bool newmoney = false;
            Walletdto x = new Walletdto();
            if (user_id == 0 && username == "root")
            {
                x = new Walletdto
                {
                    User_Id = "0",
                    Username = "root",
                    Balance = decimal.MaxValue - 1
                };
                newmoney = true;
            }
            string user_str = user_id.ToString();
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                List<Walletdto> output = new List<Walletdto>();
                if (newmoney) output.Add(x);
                else
                {
                    output = db.Query<Walletdto>("SELECT * FROM Wallets WHERE User_Id=@user_id", new { user_id = user_str }).ToList();
                    if (output.Count == 0)
                    {
                        Walletdto create = new Walletdto
                        {
                            User_Id = user_str,
                            Username = username,
                            Balance = 0
                        };
                        db.Execute("INSERT INTO Wallets (User_Id, Username, Balance) VALUES (@User_Id, @Username, @Balance)", new { create.User_Id, create.Username, create.Balance });
                        return db.QuerySingle<Walletdto>("SELECT * FROM Wallets WHERE User_Id=@User_Id", new { create.User_Id });
                    }
                }
                return output[0];
            }
        }

        public static Walletdto GetWallet(string username) // for recipient
        {
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                List<Walletdto> output = db.Query<Walletdto>("SELECT * FROM Wallets WHERE Username=@username", new { username }).ToList();
                if (output.Count == 0) { return null; }
                else return output[0];
            }
        }

        public static bool CreateWallet(CommandContext ctx)
        {
            Walletdto wal = new Walletdto
            {
                User_Id = ctx.User.Id.ToString(),
                Username = ctx.User.Username + "#" + ctx.User.Discriminator,
                Balance = 0
            };
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                List<Walletdto> check = db.Query<Walletdto>("SELECT * FROM Wallets WHERE User_Id=@User_Id", new { wal.User_Id }).ToList();
                if (check.Count != 0) return false;
                db.Execute("INSERT INTO Wallets (User_Id, Username, Balance) VALUES (@User_Id, @Username, @Balance)", new { wal.User_Id, wal.Username, wal.Balance });
                wal.Id = db.QuerySingle<int>("SELECT Id FROM Wallets WHERE User_Id=@User_Id", new { wal.User_Id });
            }
            return true;
        }

        public static bool UpdateWallet(CommandContext ctx, ulong user_id, decimal change)
        {
            if (user_id == 0) return true;
            string user_str = user_id.ToString();
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                List<Walletdto> output = db.Query<Walletdto>("SELECT * FROM Wallets WHERE User_Id=@user_id", new { user_id = user_str }).ToList();
                if (output.Count == 0) CreateWallet(ctx);
                if (change < 0 && output[0].Balance < change) return false;
                output[0].Balance += change;
                db.Execute("UPDATE Wallets SET Balance=@Balance WHERE User_Id=@user_str", new { output[0].Balance, user_str });
                return true;
            }
        }

        public static bool Transfer(CommandContext ctx, Blockchain master, Walletdto sender, Walletdto recipient, decimal amt)
        {
            bool a = ulong.TryParse(sender.User_Id, out ulong send);
            a = ulong.TryParse(recipient.User_Id, out ulong recip);
            master.NewTransaction(new Transaction(send, recip, amt));
            using (IDbConnection db = new SqlConnection(SqlController.conn))
            {
                db.Execute("INSERT INTO Transactions (Block_Id, Sender, Recipient, Amount) VALUES (@Block_Id, @Sender, @Recipient, @Amount)", new { Block_Id = master.Count(), Sender = sender.User_Id, Recipient = recipient.User_Id, Amount = amt });
            }
            a = UpdateWallet(ctx, send, (amt * -1));
            if (a && UpdateWallet(ctx, recip, amt)) return true;
            else return false;
        }
    }
}
