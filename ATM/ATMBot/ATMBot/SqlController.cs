using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ATMBot
{

    public class Userdto
    {
        public string Username { get; set; }
        public int Id { get; set; }
    }
    public class User
    {
        public User(string Username, int Id = 0, List<Team> teams = null)
        {
            _username = Username;
            key = Id;
            if (_username == "Andonio#3882") admin = true;
            else admin = false;
            _teams = teams;
        }
        public string GetUsername() { return _username; }
        public List<Team> GetTeams() { return _teams; }
        private readonly string _username;
        private List<Team> _teams;
        public bool admin;
        public int key;
    }

    public class Teamdto
    {
        public string Team { get; set; }
        public int UsernameId { get; set; }
        public int Id { get; set; }
    }

    public class Team
    {
        public Team(string name, List<Game> games)
        {
            _name = name;
            _games = games;
        }
        private readonly string _name;
        private readonly List<Game> _games;
    }

    public class Gamedto
    {
        public DateTime GameTime { get; set; }
        public string Opponent { get; set; }
        public int Id { get; set; }
        public int TeamId { get; set; }
    }

    public class Game
    {
        public Game(DateTime time, string opp)
        {
            _time = time;
            _opp = opp;
        }
        private readonly DateTime _time;
        private readonly string _opp;
    }

    public class SqlController
    {
        static string conn = "Data Source=DESKTOP-53MS6TR;Initial Catalog=ATMdb;Integrated Security=True";
        public static List<User> GetUsers()
        {
            List<Userdto> data;
            List<User> output = new List<User>();
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.Query<Userdto>("select * from Users").ToList();
            }
            foreach (Userdto x in data) output.Add(new User(x.Username, x.Id));
            return output;
        }

        public static User GetUser(string specifier)
        {
            Userdto data;
            List<Teamdto> teamstransfer;
            List<User> check = GetUsers();
            List<string> check2 = new List<string>;
            foreach (User x in check)
            {
                check2.Add(x.GetUsername());
            }
            if (check2.Contains(specifier) == false) return new User("NO");
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.QuerySingle<Userdto>("select * from Users where Username=@spec", new { spec = specifier });
                teamstransfer = db.Query<Teamdto>("SELECT * FROM Teams where UsernameID=@id", new { id = data.Id }).ToList<Teamdto>();
            }
            List<Team> teams = new List<Team>();
            foreach (Teamdto x in teamstransfer) teams.Add(GetTeam(x.Team));
            return new User(data.Username, data.Id, teams);
        }

        public static List<Team> GetTeams()
        {
            List<Team> output = new List<Team>();
            List<Teamdto> data;
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.Query<Teamdto>("select * from Teams").ToList();
            }
            foreach (Teamdto x in data) output.Add(new Team(x.Team, new List<Game>()));
            return output;
        }

        public static Team GetTeam(string specifier)
        {
            List<Gamedto> gamestransfer;
            Teamdto data;
            Team output;
            List<Team> list = GetTeams();
            List<string> list2 = new List<string>();
            foreach (Team x in list) list2.Add(x.Team);
            if (list2.Contains(specifier) == false) return new Team("No", new List<Game>());
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.Query<Teamdto>("select * from Teams").ToList();
            }
        }

        public static void AddUser(User user)
        {
            List<User> list = GetUsers();
            List<string> names = new List<string>();
            foreach (User x in list) names.Insert(names.Count, x.GetUsername());
            if (list.Count != 0 && names.Contains(user.GetUsername())) return;
            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Execute("INSERT INTO Users (Username) VALUES (@name)", new { name = user.GetUsername() });
                int key = db.QuerySingle<int>("SELECT Id FROM Users WHERE Username=@name", new { name = user.GetUsername() });
                user.key = key;
            }

        }
    }
}
