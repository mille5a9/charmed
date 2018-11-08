using System;
using System.Net.Http;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Dapper;
using System.Collections.Generic;

namespace ATMBot
{



    public class MyCommands
    {
        [Command("users-addme")]
        public async Task Hi(CommandContext ctx)
        {
            User newguy = new User(ctx.User.Username + "#" + ctx.User.Discriminator);
            SqlController.AddUser( new User(ctx.User.Username + "#" + ctx.User.Discriminator) );

            await ctx.RespondAsync($"Hi, { ctx.User.Username + "#" + ctx.User.Discriminator }");
        }

        [Command("users-ls")]
        public async Task GetUsers(CommandContext ctx, string specifier = "")
        {
            string output = "";
            if (specifier == "")
            {
                List<User> people = SqlController.GetUsers();
                foreach (User x in people) output += "Don't forget about " + x.GetUsername() + "!\n";
                await ctx.RespondAsync($"Hi, { output }");
            }
            else
            {
                User person = SqlController.GetUser(specifier);
                await ctx.RespondAsync($"Hi, { output }");
            }
        }
    }

    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "NTA5NzIzNzgyMTk4MzI5MzQ1.DsR9pg.KBGVl5aiAGO7iIYdZrpt51gdYws",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "@ATM "
            });
            commands.RegisterCommands<MyCommands>();

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}

/*
    How to get data from website with API
    [HttpGet]
        public string Get()
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("APIKEY", header);
            var data = http.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return data;
        }


        string conn = "Server=localhost;Database=ATMdb;";
        using (IDbConnection db = new SqlConnection(conn))
        {
             data = db.Query<DapperTest>("select * from testTable").ToList();
        }

 
*/
