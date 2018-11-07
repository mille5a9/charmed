using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ATMBot
{
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
        static string conn = "Data Source=DESKTOP-53MS6TR;Database=ATMdb;";
        public static List<User> GetUsers()
        {
            List<User> data;
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.Query<User>("select * from Users").ToList();
            }
            return data;
        }

        public static void AddUser(User user)
        {
            List<User> list = GetUsers();
            if (list.Contains(user)) return;
            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Execute("INSERT INTO Users (Username) VALUE @name", new { name = user.GetUsername() });
                int key = db.QuerySingle<int>("SELECT Id FROM Users WHERE Username=@name", new { name = user.GetUsername() });
                user.key = key;
            }
        }
    }
}
