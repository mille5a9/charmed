using System;
using System.Net.Http;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Dapper;
using System.Collections.Generic;
using DSharpPlus.Entities;
using System.IO;

namespace ATMBot
{



    public class MyCommands
    {
        [Command("close")]
        public async Task Close(CommandContext ctx, string pw)
        {
            if (pw == "a1b2c3d4e5")
            {
                SqlController.Write();
                Environment.Exit(0);
            }
        }

        [Command("helpme")]
        public async Task Help(CommandContext ctx)
        {
            string curr = Directory.GetCurrentDirectory();
            string output = File.ReadAllText(curr + "../../../../BotCmdNotes.txt");
            DiscordMember mem = ctx.Member;
            if (mem == null)
            {
                await ctx.Channel.SendMessageAsync(output);
                return;
            }
            await mem.SendMessageAsync(output);
        }

        [Command("users-addme")]
        public async Task addme(CommandContext ctx, string adminarg = "")
        {
            User newguy = new User(ctx.User.Username + "#" + ctx.User.Discriminator);
            if (adminarg != "" && newguy.admin == true)
            {
                SqlController.AddUser(new User(adminarg));
                await ctx.RespondAsync($"Hi [ADMIN], " + adminarg + " has been added!");
                return;
            }
            SqlController.AddUser(new User(ctx.User.Username + "#" + ctx.User.Discriminator));

            await ctx.RespondAsync($"Hi, { ctx.User.Username + "#" + ctx.User.Discriminator }! Welcome to my memories.");
        }

        [Command("users-removeme")]
        public async Task removeme(CommandContext ctx, string adminarg = "")
        {
            User oldguy = new User(ctx.User.Username + "#" + ctx.User.Discriminator);
            if (adminarg != "" && oldguy.admin == true)
            {
                SqlController.RemoveUser(SqlController.GetUser(adminarg));
                await ctx.RespondAsync($"Hi [ADMIN], " + adminarg + " has been added!");
                return;
            }
            List<User> list = SqlController.GetUsers();
            bool bet = false;
            foreach (User x in list) if (x.GetUsername() == oldguy.GetUsername()) bet = true;
            if (!bet) await ctx.RespondAsync($"User is not registered... oh well!");

            SqlController.RemoveUser(new User(ctx.User.Username + "#" + ctx.User.Discriminator));

            await ctx.RespondAsync($"{ ctx.User.Username + "#" + ctx.User.Discriminator } has been removed!");
        }

        [Command("users-ls")]
        public async Task GetUsers(CommandContext ctx, string specifier = "")
        {
            string output;
            if (specifier == "")
            {
                output = "List of all users:\n";
                List<User> people = SqlController.GetUsers();
                foreach (User x in people) output += x.GetUsername() + "\n";
                await ctx.RespondAsync($"{ output }");
            }
            else
            {
                output = "Check it out:\n";
                User person = SqlController.GetUser(specifier);
                if (person.GetUsername() == "NO")
                {
                    await ctx.RespondAsync($"User is not registered.");
                }
                output += "Name: " + person.GetUsername() + "\n";
                if (person.admin) output += "User is an admin\n";
                output += "Registered Teams:\n";
                foreach (Team x in person.GetTeams()) output += "    " + x.GetName() + "\n";
                await ctx.RespondAsync($"{ output }");
            }
        }

        [Command("teams-add")]
        public async Task AddTeam(CommandContext ctx, string specifier = "")
        {
            if (specifier == "")
            {
                await ctx.RespondAsync("Please specify a team!");
                return;
            }
            User jim = SqlController.GetUser(ctx.User.Username + "#" + ctx.User.Discriminator);
            specifier = specifier.Replace('_', ' ');
            SqlController.AddTeam(jim, specifier);
            await ctx.RespondAsync("Team added!");
        }

        [Command("teams-remove")]
        public async Task RemoveTeam(CommandContext ctx, string specifier = "")
        {
            if (specifier == "")
            {
                await ctx.RespondAsync("Please specify a team!");
                return;
            }
            User jim = SqlController.GetUser(ctx.User.Username + "#" + ctx.User.Discriminator);
            SqlController.RemoveTeam(jim, specifier);
            await ctx.RespondAsync("Team removed!");
        }

        [Command("teams-schedule")]
        public async Task Schedule(CommandContext ctx, string specifier = "")
        {
            if (specifier == "")
            {
                await ctx.RespondAsync("Please specify a team!");
                return;
            }
            specifier = specifier.Replace('_', ' ');
            Team ted = SqlController.GetTeam(specifier);
            List<Game> games = ted.GetGames();
            string output = ted.GetName() + " Schedule:\n";
            foreach(Game x in games) output += x.GetTime() + " against " + x.GetOpp() + "\n";
            await ctx.RespondAsync(output);

        }
    }

    class Program
    {
        static DiscordClient discord;
        static CommandsNextModule commands;

        static async Task Backup()
        {
            while (true)
            {
                SqlController.Write();
                await Task.Delay(10000);
                Console.Write("Writing Backup...\n");
            }
        }

        static void Main(string[] args)
        {
            SqlController.init();
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

            Backup();
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
