using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
        public void AddTeam(Team here) { _teams.Add(here); }
        public void RemoveTeam(Team here) { _teams.Remove(here); }
        private readonly string _username;
        private List<Team> _teams;
        public bool admin;
        public int key;
    }

    public class Teamdto
    {
        public string Team { get; set; }
        public int UsernameID { get; set; }
        public int Id { get; set; }
    }

    public class Team
    {
        public Team(string name, List<Game> games)
        {
            _name = name;
            _games = games;
        }
        public string GetName() { return _name; }
        public List<Game> GetGames() { return _games; }
        private readonly string _name;
        private readonly List<Game> _games;
    }

    public class Gamedto
    {
        public DateTime GameTime { get; set; }
        public string Opponent { get; set; }
        public string TeamID { get; set; }
        public int Id { get; set; }
    }

    public class Game
    {
        public Game(DateTime time, string team, string opp)
        {
            _time = time;
            _team = team;
            _opp = opp;
        }
        public DateTime GetTime() { return _time; }
        public string GetTeam() { return _team; }
        public string GetOpp() { return _opp; }
        private readonly DateTime _time;
        private readonly string _team;
        private readonly string _opp;
    }

    public class SqlController
    {
        static readonly string conn = "Data Source=DESKTOP-53MS6TR;Initial Catalog=ATMdb;Integrated Security=True";
        public static void init()
        {
            string curr = Directory.GetCurrentDirectory();
            string input = File.ReadAllText(curr + "../../../../TeamGames.txt");
            string[] games = input.Split('\n');
            string[] gameargs;
            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Execute("DELETE FROM TeamGames");
                foreach (string x in games)
                {
                    if (x == "") break;
                    gameargs = x.Split('|');
                    db.Execute("INSERT INTO TeamGames (TeamID, GameTime, Opponent) VALUES (@TeamID, @GameTime, @Opponent)", new { TeamID = gameargs[1], GameTime = gameargs[2], Opponent = gameargs[3] });
                }
            }
            initUsers();
        }

        static void initUsers()
        {

            string curr = Directory.GetCurrentDirectory();
            string input = File.ReadAllText(curr + "../../../../Users.txt");
            string[] users = input.Split('\n');
            string[] userargs;
            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Execute("DELETE FROM Users");
                foreach (string x in users)
                {
                    if (x == "") break;
                    userargs = x.Split('|');
                    db.Execute("INSERT INTO Users (Username) VALUES (@Username)", new { Username = userargs[1]});
                }
            }
            initTeams();
        }

        static void initTeams()
        {

            string curr = Directory.GetCurrentDirectory();
            string input = File.ReadAllText(curr + "../../../../Teams.txt");
            string[] teams = input.Split('\n');
            string[] teamargs;
            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Execute("DELETE FROM Teams");
                foreach (string x in teams)
                {
                    if (x == "") break;
                    teamargs = x.Split('|');
                    db.Execute("INSERT INTO Teams (UsernameID, Team) VALUES (@UsernameID, @Team)", new { UsernameID = teamargs[1], Team = teamargs[2] });
                }
            }
        }

        public static void Write()
        {
            string curr = Directory.GetCurrentDirectory() + "../../../../";
            using (IDbConnection db = new SqlConnection(conn))
            {
                List<Gamedto> gamedtos = db.Query<Gamedto>("SELECT * FROM TeamGames").ToList();
                List<Teamdto> teamdtos = db.Query<Teamdto>("SELECT * FROM Teams").ToList();
                List<Userdto> userdtos = db.Query<Userdto>("SELECT * FROM Users").ToList();
                string txt = "";
                foreach (Gamedto x in gamedtos) txt += ("" + x.Id + '|' + x.TeamID + '|' + x.GameTime + '|' + x.Opponent + '\n');
                File.WriteAllText(curr + "TeamGames.txt", txt);
                txt = "";
                foreach (Teamdto x in teamdtos) txt += ("" + x.Id + '|' + x.UsernameID + '|' + x.Team + '\n');
                File.WriteAllText(curr + "Teams.txt", txt);
                txt = "";
                foreach (Userdto x in userdtos) txt += ("" + x.Id + '|' + x.Username + '\n');
                File.WriteAllText(curr + "Users.txt", txt);
            }
        }

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
            List<string> check2 = new List<string>();
            foreach (User x in check)
            {
                check2.Add(x.GetUsername());
            }
            if (check2.Contains(specifier) == false) return new User("NO");
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.QuerySingle<Userdto>("SELECT * FROM Users WHERE Username=@spec", new { spec = specifier });
                teamstransfer = db.Query<Teamdto>("SELECT * FROM Teams where UsernameID=@id", new { id = data.Id }).ToList<Teamdto>();
            }
            List<Team> teams = new List<Team>();
            foreach (Teamdto x in teamstransfer) teams.Add(GetTeam(x.Team));
            return new User(data.Username, data.Id, teams);
        }

        public static List<Team> GetTeams()
        {
            List<Team> output = new List<Team>();
            List<string> data;
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.Query<string>("SELECT DISTINCT TeamID FROM TeamGames").ToList();
                foreach (string x in data)
                {
                    List<Gamedto> gamestransfer = db.Query<Gamedto>("SELECT * FROM TeamGames WHERE TeamID=@name", new { name = x }).ToList();
                    List<Game> list = new List<Game>();
                    foreach (Gamedto y in gamestransfer) list.Add(new Game(y.GameTime, x, y.Opponent));
                    output.Add(new Team(x, list));
                }
            }
            return output;
        }

        public static Team GetTeam(string specifier)
        {
            List<Game> games = new List<Game>();
            string data;
            List<Team> list = GetTeams();
            List<string> list2 = new List<string>();
            foreach (Team x in list) list2.Add(x.GetName());
            if (list2.Contains(specifier) == false) return new Team("NO", new List<Game>());
            using (IDbConnection db = new SqlConnection(conn))
            {
                data = db.QuerySingle<string>("SELECT DISTINCT TeamID FROM TeamGames WHERE TeamID=@name", new { name = specifier });
                List<Gamedto> gamestransfer = db.Query<Gamedto>("SELECT * FROM TeamGames WHERE TeamID=@name", new { name = specifier }).ToList();
                foreach (Gamedto x in gamestransfer) games.Add(new Game(x.GameTime, x.TeamID, x.Opponent));
            }
            return new Team(data, games);
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

        public static void RemoveUser(User user)
        {
            //check if user is registered
            List<User> existingusers = GetUsers();
            bool bet = false;
            foreach (User x in existingusers) if (x.GetUsername() == user.GetUsername()) bet = true;
            if (bet == false) return;

            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Execute("DELETE FROM Teams WHERE (UsernameId=@id)", new { id = user.key});
                db.Execute("DELETE FROM Users WHERE Username=@name", new { name = user.GetUsername() });
            }

        }

        public static void AddTeam(User user, string team)
        {
            //check if user is registered, if team is registered, and if user already has team
            List<User> existingusers = GetUsers();
            bool bet = false;
            foreach (User x in existingusers) if (x.GetUsername() == user.GetUsername()) bet = true;
            if (bet == false) return;
            List<Team> existingteams = GetTeams();
            bet = false;
            foreach (Team x in existingteams) if (x.GetName() == team) bet = true;
            if (bet == false) return;
            bet = false;
            existingteams = GetUser(user.GetUsername()).GetTeams();
            foreach (Team x in existingteams) if (x.GetName() == team) bet = true;
            if (bet) return;

            using (IDbConnection db = new SqlConnection(conn))
            {
                db.Execute("INSERT INTO Teams (UsernameID, Team) VALUES (@id, @team)", new { id = user.key, team });
                user.AddTeam(GetTeam(team));
            }
        }

        public static void RemoveTeam(User user, string team)
        {
            //check if user is registered, if team is registered, and if user already has team
            List<User> existingusers = GetUsers();
            bool bet = false;
            foreach (User x in existingusers) if (x.GetUsername() == user.GetUsername()) bet = true;
            if (bet == false) return;
            List<Team> existingteams = GetTeams();
            bet = false;
            foreach (Team x in existingteams) if (x.GetName() == team) bet = true;
            if (bet == false) return;
            bet = false;
            existingteams = GetUser(user.GetUsername()).GetTeams();
            foreach (Team x in existingteams) if (x.GetName() == team) bet = true;
            if (bet == false) return;

            using (IDbConnection db = new SqlConnection(conn))
            {
                user.RemoveTeam(GetTeam(team));
                db.Execute("DELETE FROM Teams WHERE (UsernameID=@id, Team=@team)", new { id = user.key, team });
            }

        }
    }
}
