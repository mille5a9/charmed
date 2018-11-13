using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.CommandsNext.Attributes;
using Ofl.Twitch.V5;
using ATMBot.Reminder;
using ATMBot.Waam;
using System.Net.Http;

namespace ATMBot
{

    public class MyCommands
    {
        public static Blockchain Coin = new Blockchain();

        [Command("coin-bal")]
        public async Task CoinBalance(CommandContext ctx)
        {
            Walletdto userwallet = Wallet.GetWallet(ctx.User.Id, (ctx.User.Username + "#" + ctx.User.Discriminator));
            decimal output = userwallet.Balance;
            await ctx.RespondAsync("Your balance is " + output + " Waamcoins");
        }

        [Command("coin-pay")]
        public async Task CoinPay(CommandContext ctx, [Description("Name#Number of destination user")] string username, [Description("Number of Waamcoins to send")] decimal amt)
        {
            Walletdto sender = Wallet.GetWallet(ctx.User.Id, (ctx.User.Username + "#" + ctx.User.Discriminator)), recipient = Wallet.GetWallet(username);
            if (recipient == null) await ctx.RespondAsync("That recipient does not have a wallet! They need to use coin-mine to earn their first stake in Waamcoin.");
            else
            {
                if (Wallet.Transfer(ctx, Coin, sender, recipient, amt)) await ctx.RespondAsync("Transfer Complete!");
                else await ctx.RespondAsync("Transfer Unsuccessful. Make sure the recipient has a wallet and you have enough Waamcoin!");
            }
        }

        [Command("coin-mine")]
        public async Task CoinMine(CommandContext ctx)
        {
            if (ctx.Channel is DiscordDmChannel)
            {
                if (await Coin.ProofOfWork(ctx, Coin.LastBlock().GetPreviousHash()))
                {
                    //gib coin
                    decimal amt = Coin.BlockReward();
                    Walletdto sender = Wallet.GetWallet(0, "root");
                    Walletdto recipient = Wallet.GetWallet(ctx.User.Id, (ctx.User.Username + "#" + ctx.User.Discriminator));
                    bool success = Wallet.Transfer(ctx, Coin, sender, recipient, amt);
                    if (success) await ctx.RespondAsync("Success! You've been awarded with " + amt + " Waamcoin!");
                    else await ctx.RespondAsync("You did good, but something went wrong.");
                }
                else await ctx.RespondAsync("That, was an incorrect answer.");
            }
            else
            {
                DiscordDmChannel x = await ctx.Member.CreateDmChannelAsync();
                await x.SendMessageAsync("You must mine in a DM, to prevent spam.");
            }
        }

        [Command("remind-ls")]
        [Description("Produces a list containing every reminder you have pending")]
        public async Task ListReminders(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            List<Reminderdto> list = ReminderModule.GetReminders(ctx.User.Id.ToString());
            string output = "Reminders for " + ctx.User.Mention + ":\n";
            foreach (Reminderdto x in list)
            {
                output += "\t" + x.Time + "\n\t\t" + x.Message;
            }
            await ctx.RespondAsync(output);
        }

        [Command("remind-new")]
        [Description("Used to create a new reminder for the user")]
        public async Task NewReminder(CommandContext ctx,
            [RemainingText, Description("Extra arg for one-line reminding: separate time and message with '>'")] string elaboration = null)
        {
            await ctx.TriggerTypingAsync();

            if (elaboration != null)
            {
                string[] command = elaboration.Split('>');
                DateTime parse = ReminderModule.ParseReminderTime(command[0]);
                ReminderModule.SaveReminder(ctx, command[1], parse);
                DiscordDmChannel x = await ctx.Member.CreateDmChannelAsync();
                await x.SendMessageAsync("" + parse + " Reminder Created at " + DateTime.Now);
            }
            else
            {
                DateTime parse = new DateTime();
                await ctx.Channel.SendMessageAsync("Let's make a new reminder! Please type the time you want to be reminded, I'll do my best to understand it:", false);
                InteractivityModule inter = ctx.Client.GetInteractivityModule();
                MessageContext msg = await inter.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(3));
                await ctx.TriggerTypingAsync();
                if (msg != null) parse = ReminderModule.ParseReminderTime(msg.Message.Content);
                await ctx.RespondAsync("What would you like your reminder at " + parse + " to say?");
                msg = await inter.WaitForMessageAsync(xm => xm.Author.Id == ctx.User.Id, TimeSpan.FromMinutes(3));
                ReminderModule.SaveReminder(ctx, msg.Message.Content, parse);
                await ctx.RespondAsync("Okay, I'll remind you when it's time");
            }
        }

        [Command("close")]
        [Description("Used with the appropriate password will take the bot offline")]
        public async Task Close(CommandContext ctx, [Description("The secret code")] string pw)
        {
            if (pw == "a1b2c3d4e5")
            {
                SqlController.Write();
                await Task.Delay(1);
                Environment.Exit(0);
            }
        }

        [Command("helpme")]
        [Description("Shows the raw text of the BotCmdNotes.txt file")]
        public async Task Help(CommandContext ctx)
        {
            string curr = Directory.GetCurrentDirectory();
            string output = File.ReadAllText(curr + "../../../../SqlBackups/BotCmdNotes.txt");
            DiscordMember mem = ctx.Member;
            if (mem == null)
            {
                await ctx.TriggerTypingAsync();
                await ctx.Channel.SendMessageAsync(output);
                return;
            }
            await mem.SendMessageAsync(output);
        }

        [Command("users-addme")]
        [Description("Adds you to ATMbot's list of users")]
        public async Task Addme(CommandContext ctx, [Description("If you're an admin, this can be anybody")] string adminarg = "")
        {
            User newguy = new User(ctx.User.Username + "#" + ctx.User.Discriminator);
            if (adminarg != "" && newguy.admin == true)
            {
                await ctx.TriggerTypingAsync();
                SqlController.AddUser(new User(adminarg));
                await ctx.RespondAsync($"Hi [ADMIN], " + adminarg + " has been added!");
                return;
            }
            await ctx.TriggerTypingAsync();
            SqlController.AddUser(new User(ctx.User.Username + "#" + ctx.User.Discriminator));
            await ctx.RespondAsync($"Hi, { ctx.User.Username + "#" + ctx.User.Discriminator }! Welcome to my memories.");
        }

        [Command("users-removeme")]
        [Description("Removes you from ATMbot's list of users")]
        public async Task Removeme(CommandContext ctx, [Description("If you're an admin, this can be anybody")] string adminarg = "")
        {
            User oldguy = new User(ctx.User.Username + "#" + ctx.User.Discriminator);
            if (adminarg != "" && oldguy.admin == true)
            {
                await ctx.TriggerTypingAsync();
                SqlController.RemoveUser(SqlController.GetUser(adminarg));
                await ctx.RespondAsync($"Hi [ADMIN], " + adminarg + " has been added!");
                return;
            }
            List<User> list = SqlController.GetUsers();
            bool bet = false;
            foreach (User x in list) if (x.GetUsername() == oldguy.GetUsername()) bet = true;
            await ctx.TriggerTypingAsync();
            if (!bet) await ctx.RespondAsync($"User is not registered... oh well!");

            SqlController.RemoveUser(new User(ctx.User.Username + "#" + ctx.User.Discriminator));

            await ctx.RespondAsync($"{ ctx.User.Username + "#" + ctx.User.Discriminator } has been removed!");
        }

        [Command("users-ls")]
        [Description("Prints a list of all known ATMbot users")]
        public async Task GetUsers(CommandContext ctx, [Description("Specifying a single user will print more detailed information")]  string specifier = "")
        {
            string output;
            await ctx.TriggerTypingAsync();
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
        [Description("Adds a given team to your ATMbot user profile")]
        public async Task AddTeam(CommandContext ctx, [Description("The team name here, must be written_like_this [place], [sport] e.g. Ohio_State_University_Men's_Basketball")] string specifier = "")
        {
            await ctx.TriggerTypingAsync();
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
        [Description("Removes a given team from your ATMbot user profile")]
        public async Task RemoveTeam(CommandContext ctx, [Description("This should be a team that you have but you don't want")] string specifier = "")
        {
            await ctx.TriggerTypingAsync();
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
        [Description("Prints the schedule of a given team if ATMbot knows about them")]
        public async Task Schedule(CommandContext ctx, [Description("This should be the team's name if you can spell it right")] string specifier = "")
        {
            await ctx.TriggerTypingAsync();
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
        static InteractivityModule inter;

        static async Task Backup()
        {
            while (true)
            {
                SqlController.Write();
                await Task.Delay(10000);
                Console.Write("Writing Backup...\n");
            }
        }

        static async Task RemindLoop()
        {
            try
            {
                while (true)
                {
                    List<Reminderdto> list = ReminderModule.GetReminders();
                    List<Reminderdto> removeals = new List<Reminderdto>();
                    foreach (Reminderdto x in list)
                    {
                        if (DateTime.Compare(x.Time, DateTime.Now) < 0)
                        {
                            string output = "You wanted me to remind you about this:\n";
                            output += x.Message;

                            DiscordUser user;
                            UInt64.TryParse(x.User_Id, out ulong n);
                            user = await discord.GetUserAsync(n);
                            DiscordDmChannel chan = await discord.CreateDmAsync(user);
                            await chan.TriggerTypingAsync();
                            await chan.SendMessageAsync(output);
                            removeals.Add(x);
                        }
                    }
                    ReminderModule.RemoveReminders(removeals);
                    Console.Write("Reminders Processed, Idling...\n");
                    await Task.Delay(10000);
                    Console.Write("Processing Reminders...\n");
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        static void Main(string[] args)
        {
            SqlController.Init();
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
            inter = discord.UseInteractivity(new InteractivityConfiguration());
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "@ATM "
            });
            commands.RegisterCommands<MyCommands>();

            Backup();
            RemindLoop();
            await discord.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
