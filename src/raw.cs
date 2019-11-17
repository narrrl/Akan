using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Net;
using System.Threading.Tasks;
using JikanDotNet;
using System.Collections.Generic;

namespace Akan
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        static public DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            var botToken = File.ReadAllText("token.txt");

            _client.Log += Log;

            await RegisterCommandAsync();

            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            await _client.SetGameAsync("akan!help", null, ActivityType.Listening);

            await _client.StartAsync();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);

            return Task.CompletedTask;
        }

        public async Task RegisterCommandAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;

            if (message is null || message.Author.IsBot) return;

            int argPos = 0;

            if (message.HasStringPrefix("akan!", ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(_client, message);

                var result = await _commands.ExecuteAsync(context, argPos, _services);

                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
        }
    }
	
	[Group("help")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("")]
        public async Task ConverterAsync5()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("What can I do for you, senpai?")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!help**\" + [**category**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Categories:**__",
                          "**\tchat**\n" +
                          "\tDefault chat commands\n\n" +
                          "**\tmath**\n" +
                          "\tMath related commands\n\n" +
                          "**\tadmin**\n" +
                          "\tCommands that require admin permission\n\n" +
                          "\t**myAnimeList**\n" +
                          "\tFor all you dirty weebs out there")
                .WithImageUrl("https://media.giphy.com/media/mGVXF7qJ22dxGyJLng/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("math")]
        public async Task MathAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Let's do some homework together!")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!math**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Math Commands:**__",
                          "**\tpow** + [**number**] + [**pow**]\n" +
                          "\tCalculates the power of a number\n\n" +
                          "**\tsqrt** + [**number**]\n" +
                          "\tCalculates the squareroot of a number\n\n" +
                          "**\tabc** + [**a**] + [**b**] + [**c**]\n" +
                          "\tCalculates ax²+bx²+c = 0")
                .WithImageUrl("https://media.giphy.com/media/Kp9GUdNUYAqhq/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("chat")]
        public async Task ChatAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Let's chat a little bit <3")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Chat Commands:**__",
                          "\t**rep** + [**message**]\n" +
                          "\tRepeats a  message (Only numbers, letters, ? and !) in emotes\n\n" +
                          "\t**say** + [**message**]\n" +
                          "\tEchos a message\n\n" +
                          "\t**inviteBot**\n" +
                          "\tInvite link to add the bot to your server\n\n" +
                          "\t**test**\n" +
                          "\tJust a test message\n\n" +
                          "\t**8ball**\n" +
                          "\tAsk a yes/no question and get the answer!\n\n" +
                          "\t**invite**\n"+
                          "\tCreates a single use invite link for the current channel\n\n" +
                          "\t**neko**\n" +
                          "\tSends a random sfw neko pic from nekos.life\n\n" +
                          "\t**lewdNeko**\n" +
                          "\tSends random nsfw neko pic from nekos.life\n\n" +
                          "\t**rand** + [**min**] + [**max**]\n" +
                          "\tSends a random number between min and max")
                .WithImageUrl("https://media.giphy.com/media/NjvprsVwBehvq/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("admin")]
        public async Task AdminAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Only for my masters!")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!admin**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Admin Commands:**__",
                          "\t**ban** + [**@user**]\n" +
                          "\tBans a user from the server\n\n" +
                          "\t**kick** + [**@user**]\n" +
                          "\tKicks a user from the server\n\n" +
                          "\t**status** + [**playing/watching/listening**] + [**message**]\n" +
                          "\tSets the status of the bot\n\n" +
                          "\t**status** + [**streaming + twitch url**] + [**message**]\n" +
                          "\tSets a special streaming status to promote someone\n\n" +
                          "\t**purge** + [**amount**]\n" +
                          "\tDeletes an amount of messages\n\n"+
                          "\t**createInvite** [**#channel**] + [**age**] + [**maxUses**]\n + [**Unique(true/false)**] + [**temp(true/false)**]\n" +
                          "\tCreates an invite for a channel with an age in seconds, max uses, if it'd be unique or temp\n\n" + 
                          "\t**delMessage** + [**MessageID**]\n" +
                          "\tDeletes a specific message\n\n" +
                          "\t**channel** **changeDesc** + [**#channel**] + [**\"Description\"**]\n" +
                          "\tChange the description of a channel")
                .WithImageUrl("https://media.giphy.com/media/kl3VSpDkwCYTK/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("myAnimeList")]
        public async Task MALAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Disgusting weebs!")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!mal**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**MyAnimeList Commands:**__",
                          "\t**top** + [**airing/upcoming/page number**]\n" +
                          "\tLists all top airing/upcoming anime. If you want to see the overall"+
                          "\ttop anime type the page number for example: 1 (Every page has 50 entries)\n\n" +
                          "\t**season** + [**year**] + [**spring/summer/fall/winter**]\n" +
                          "\tLists all anime that aired in that season. No year and season shows current season\n\n" +
                          "\t**search** + [**\"anime name\"**] + [**total results**]\n"+
                          "\tSearches for anime on MAL\n\n" +
                          "\t**seasonNext**\n" +
                          "\tShows next season\n\n")
                .WithImageUrl("https://i.imgur.com/6NenalB.gif");

            await ReplyAsync("", false, help.Build());
        }
    }
	
	public class Chat : ModuleBase<SocketCommandContext>
    {

        public class SayModule : ModuleBase<SocketCommandContext>
        {
            [Command("say")]
            [Summary("Echoes a message.")]
            public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            {
                var msg = Context.Message;
                await msg.DeleteAsync();
                await ReplyAsync(echo);
            }
        }

        public class TestModule : ModuleBase<SocketCommandContext>
        {
            [Command("test")]
            [Summary("Test command.")]
            public async Task PingAsync()
            {
                await ReplyAsync("Successful!");
            }
        }

        class NekoModule : ModuleBase<SocketCommandContext>
        {
            [Command("neko")]
            public async Task SfwImage()
            {
                var url = new WebClient().DownloadString("https://nekos.life/api/neko");
                string[] url2 = url.Split(":\"");
                string[] url3 = url2[1].Split("\"");

                
                EmbedBuilder neko = new EmbedBuilder();

                neko.WithTitle("Nyaa~")
                    .WithImageUrl(url3[0]);

                await ReplyAsync("", false, neko.Build());
            }

            [Command("LewdNeko")]
            public async Task NsfwImage()
            {
                var channel = Context.Channel as ITextChannel;
                if (channel.IsNsfw)
                {
                    var url = new WebClient().DownloadString("https://nekos.life/api/lewd/neko");
                    string[] url2 = url.Split(":\"");
                    string[] url3 = url2[1].Split("\"");


                    EmbedBuilder neko = new EmbedBuilder();

                    neko.WithTitle("Nyaa~ <:LewdNeko:604346078913101845>")
                        .WithImageUrl(url3[0]);

                    await ReplyAsync("", false, neko.Build());
                }
                else
                {
                    await ReplyAsync("Not here senpai!");
                    await ReplyAsync("<a:blushDS:639619041920548884>");
                }
            }
        }

        public class RandModule : ModuleBase<SocketCommandContext>
        {
            [Command("rand")]
            public async Task RandomNum(int min, int max)
            {
                Random rand = new Random();
                int result = rand.Next(min, max);
                var msg1 = await ReplyAsync("Your number is...");
                await Task.Delay(1000);
                await msg1.ModifyAsync(msg => msg.Content = "**" + result.ToString() + "**");
                return;
            }
        }

        public class RepModule : ModuleBase<SocketCommandContext>
        {
            // echoes input in reginoal indicator letters
            [Command("rep")]
            [Summary("Echoes input in reginoal indicator letters")]
            public async Task RepeatAsync([Remainder] string input)
            {
                var msg = Context.Message as SocketMessage;
                await msg.DeleteAsync();
                string b = input.ToLower();
                string d = "";
                int asciiWert;

                foreach (char c in b)
                {
                    asciiWert = c;

                    if (asciiWert == 32)
                    {
                        d = d + "\n";
                    }

                    else if ((asciiWert >= 97) && (asciiWert <= 122))
                    {
                        d = d + ":regional_indicator_" + c + ": ";
                    }

                    else if ((asciiWert >= 48) && (asciiWert <= 57))
                    {
                        switch (asciiWert)
                        {
                            case (48):
                                d = d + ":zero: ";
                                break;

                            case (49):
                                d = d + ":one: ";
                                break;

                            case (50):
                                d = d + ":two: ";
                                break;

                            case (51):
                                d = d + ":three: ";
                                break;

                            case (52):
                                d = d + ":four: ";
                                break;

                            case (53):
                                d = d + ":five: ";
                                break;

                            case (54):
                                d = d + ":six: ";
                                break;

                            case (55):
                                d = d + ":seven: ";
                                break;

                            case (56):
                                d = d + ":eight: ";
                                break;

                            case (57):
                                d = d + ":nine: ";
                                break;
                        }
                    }

                    else if (asciiWert == 33)
                    {
                        d = d + ":exclamation: ";
                    }

                    else if (asciiWert == 63)
                    {
                        d = d + ":question: ";
                    }

                    else if ((asciiWert >= 35) && (asciiWert <= 36))
                    {
                        switch (asciiWert)
                        {
                            case (35):
                                d = d + ":hash: ";
                                break;

                            case (36):
                                d = d + ":heavy_dollar_sign: ";
                                break;
                        }
                    }

                    else if ((asciiWert >= 42) && (asciiWert <= 43))
                    {
                        switch (asciiWert)
                        {
                            case (42):
                                d = d + ":heavy_multiplication_x: ";
                                break;

                            case (43):
                                d = d + ":heavy_plus_sign: ";
                                break;
                        }
                    }

                    else if (asciiWert == 45)
                    {
                        d = d + ":heavy_minus_sign: ";
                    }

                    else if (asciiWert == 47)
                    {
                        d = d + ":heavy_division_sign: ";
                    }

                    else if (c == 'ä' || c == 'ö' || c == 'ü' || c == 'ß')
                    {
                        switch (c)
                        {
                            case 'ä':
                                d = d + ":regional_indicator_" + "a" + ": " + ":regional_indicator_" + "e" + ": ";
                                break;
                            case 'ö':
                                d = d + ":regional_indicator_" + "o" + ": " + ":regional_indicator_" + "e" + ": ";
                                break;
                            case 'ü':
                                d = d + ":regional_indicator_" + "u" + ": " + ":regional_indicator_" + "e" + ": ";
                                break;
                            case 'ß':
                                d = d + ":regional_indicator_" + "s" + ": " + ":regional_indicator_" + "s" + ": ";
                                break;
                            default:
                                break;
                        }
                    }

                    else
                    {
                        d = d + c;
                    }
                }
                await ReplyAsync(d);
            }
        }

        public class EightBallModule : ModuleBase<SocketCommandContext>
        {
            [Command("8ball")]
            public async Task EightBall([Remainder]string echo)
            {
                Random random = new Random();
                int randNum = random.Next(0, 100);
                if (randNum <= 50)
                {
                    await ReplyAsync("No!");
                    await ReplyAsync("<a:TriggeredWeeb:641178939208892416>");
                }
                else
                {
                    await ReplyAsync("Yes!");
                    await ReplyAsync("<a:blushDS:639619041920548884>");
                }
            }
        }

        public class InviteModule : ModuleBase<SocketCommandContext>
        {
            [Command("inviteBot")]
            public async Task InviteAsync()
            {
                EmbedBuilder help = new EmbedBuilder();

                help.WithTitle("Wanna play with me on your own server?")
                    .WithColor(Color.DarkMagenta)
                    .WithUrl("https://discordapp.com/api/oauth2/authorize?client_id=642884956040724490&permissions=8&scope=bot")
                    .WithImageUrl("https://media.giphy.com/media/yRMsqqezvtfYk/giphy.gif");

                await ReplyAsync("", false, help.Build());
            }
        }

        [Command("createInvite")]
        public async Task createInvite(ITextChannel channel, int age = 0, int max = 1, bool use = false, bool temp = false)
        {
            var invite = await channel.CreateInviteAsync(age, max, use, temp, null);
            await ReplyAsync(invite.Url);
        }

        [Command("invite")]
        public async Task SingleInvite()
        {
            ITextChannel channel = Context.Channel as SocketTextChannel;
            var invite = await channel.CreateInviteAsync(86400, 1, false, false, null);
            await ReplyAsync(invite.Url);
        }

        [Command("jibril")]
        public async Task Jibril()
        {
            await ReplyAsync("<a:J1:642067038361092096><a:J2:642067038554030090><a:J3:642067040047464463><a:J4:642067044451483678><a:J5:642067043402645515><a:J6:642067043809624074>\n" +
                             "<a:J7:642067040953171985><a:J8:642067040470827031><a:J9:642067043574743091><a:J10:642067043406970901><a:J11:642067043935584266><a:J12:642067043394256942>\n" +
                             "<a:J13:642067043893641274><a:J14:642067042865905689><a:J15:642067042845065217><a:J16:642067042995929099><a:J17:642067043377741834><a:J18:642067044443095040>");
            return;
        }


        [Group("math")]
        public class ChatModule : ModuleBase<SocketCommandContext>
        {
            // ~say hello world -> hello world
            [Command("pow")]
            [Summary("Pow x of a number y")]
            public async Task PowAsync([Remainder] [Summary("Pow x of a number y")] string echo)
            {
                String[] splittedEcho = echo.Split(" ");
                double number = Convert.ToDouble(splittedEcho[0]);
                double pow = Convert.ToDouble(splittedEcho[1]);
                double solution = Math.Pow(number, pow);
                echo = Convert.ToString(solution);
                await ReplyAsync(echo);
            }


            // get confirmation
            [Command("sqrt")]
            [Summary("Squareroot of a number")]
            public async Task SqrtAsync([Remainder] [Summary("Sqrt or a number")] string echo)
            {
                double number = Convert.ToDouble(echo);
                double solution = Math.Sqrt(number);
                echo = Convert.ToString(solution);
                await ReplyAsync(echo);
            }

            [Command("abc")]
            [Summary("Abc Formel")]
            public async Task AbcAsync([Remainder] [Summary("Abc Formel")] string echo)
            {
                string[] splitEcho = echo.Split(" ");
                double a, b, c, solution1, solution2;
                a = Convert.ToDouble(splitEcho[0]);
                b = Convert.ToDouble(splitEcho[1]);
                c = Convert.ToDouble(splitEcho[2]);
                solution1 = ((-b) + Math.Sqrt((Math.Pow(b, 2) - 4 * a * c))) / (2 * a);
                solution2 = ((-b) - Math.Sqrt((Math.Pow(b, 2) - 4 * a * c))) / (2 * a);
                string sol1 = Convert.ToString(solution1);
                string sol2 = Convert.ToString(solution2);

                await ReplyAsync("x_1: " + sol1 + " x_2: " + sol2);
            }

        }

    }
	
	class Methods
    {
        static public int getYear()
        {
            DateTime thisDay = DateTime.Today;
            string str = thisDay.Year.ToString();
            return Convert.ToInt32(str);
        }


        static public string getSeason()
        {
            DateTime thisDay = DateTime.Today;
            int month = thisDay.Month;
            string monthStr = "";
            switch (month)
            {
                case 1:
                    monthStr = "winter";
                    break;
                case 2:
                    monthStr = "winter";
                    break;
                case 3:
                    monthStr = "winter";
                    break;
                case 4:
                    monthStr = "spring";
                    break;
                case 5:
                    monthStr = "spring";
                    break;
                case 6:
                    monthStr = "spring";
                    break;
                case 7:
                    monthStr = "summer";
                    break;
                case 8:
                    monthStr = "summer";
                    break;
                case 9:
                    monthStr = "summer";
                    break;
                case 10:
                    monthStr = "fall";
                    break;
                case 11:
                    monthStr = "fall";
                    break;
                case 12:
                    monthStr = "fall";
                    break;

            }
            return monthStr;
        }

        static public string getNextSeason()
        {
            DateTime thisDay = DateTime.Today;
            int month = thisDay.Month + 3;
            if(month > 12)
            {
                month = 1;
            }
            string monthStr = "";
            switch (month)
            {
                case 1:
                    monthStr = "winter";
                    break;
                case 2:
                    monthStr = "winter";
                    break;
                case 3:
                    monthStr = "winter";
                    break;
                case 4:
                    monthStr = "spring";
                    break;
                case 5:
                    monthStr = "spring";
                    break;
                case 6:
                    monthStr = "spring";
                    break;
                case 7:
                    monthStr = "summer";
                    break;
                case 8:
                    monthStr = "summer";
                    break;
                case 9:
                    monthStr = "summer";
                    break;
                case 10:
                    monthStr = "fall";
                    break;
                case 11:
                    monthStr = "fall";
                    break;
                case 12:
                    monthStr = "fall";
                    break;

            }
            return monthStr;
        }
    }
	
	[Group("mal")]
    public class Anime : ModuleBase<SocketCommandContext>
    {
        static int yearCurrently = Methods.getYear();
        static string seasonCurrently = Methods.getSeason();
        [Command("season")]
        public async Task Season(int year = 0, string seasonStr = null)
        {
            if(year == 0)
            {
                year = yearCurrently;
            }
            if(seasonStr == null)
            {
                seasonStr = seasonCurrently;
                if (seasonStr.Equals("winter"))
                {
                    year++;
                }
            }
            IJikan jikan = new Jikan(true);

            Season season;
            switch (seasonStr)
            {
                case "spring":
                    season = jikan.GetSeason(year,Seasons.Spring).Result;
                    break;
                case "summer":
                    season = jikan.GetSeason(year, Seasons.Summer).Result;
                    break;
                case "fall":
                    season = jikan.GetSeason(year, Seasons.Fall).Result;
                    break;
                case "winter":
                    season = jikan.GetSeason(year, Seasons.Winter).Result;
                    break;
                default:
                    await ReplyAsync("Usage: **akan!mal** **season** + [**year**] + [**spring**/**summer**/**fall**/**winter**]");
                    return;
            }



            EmbedBuilder seasonEmb = new EmbedBuilder();

            string animeList = "";
            foreach (var seasonEntry in season.SeasonEntries)
            {
                animeList = animeList + "[" + seasonEntry.Title + "](" + seasonEntry.URL + ")" + "\n";
            }

            string[] slicedList = animeList.Split("\n");
            string finalAnimeList = "";

            for(int i = 0; i <= slicedList.Length - 1 && finalAnimeList.Length < 1900; i++)
            {
                finalAnimeList = finalAnimeList + slicedList[i] + "\n";
            }

            seasonEmb.WithTitle("Season : " + season.SeasonYear + " " + season.SeasonName)
                     .WithDescription(finalAnimeList)
                     .WithColor(0x2e51a2);

            await ReplyAsync("", false, seasonEmb.Build());

            
        }

        [Command("nextSeason")]
        public async Task SeasonNext()
        {
            int year = Methods.getYear();
            string seasonStr = Methods.getNextSeason();
            if (seasonStr.Equals("winter"))
            {
                year++;
            }
            IJikan jikan = new Jikan(true);

            Season season;
            switch (seasonStr)
            {
                case "spring":
                    season = jikan.GetSeason(year, Seasons.Spring).Result;
                    break;
                case "summer":
                    season = jikan.GetSeason(year, Seasons.Summer).Result;
                    break;
                case "fall":
                    season = jikan.GetSeason(year, Seasons.Fall).Result;
                    break;
                case "winter":
                    season = jikan.GetSeason(year, Seasons.Winter).Result;
                    break;
                default:
                    await ReplyAsync("Usage: **akan!mal** **season** + [**year**] + [**spring**/**summer**/**fall**/**winter**]");
                    return;
            }



            EmbedBuilder seasonEmb = new EmbedBuilder();

            string animeList = "";
            foreach (var seasonEntry in season.SeasonEntries)
            {
                animeList = animeList + "[" + seasonEntry.Title + "](" + seasonEntry.URL + ")" + "\n";
            }

            string[] slicedList = animeList.Split("\n");
            string finalAnimeList = "";

            for (int i = 0; i <= slicedList.Length - 1 && finalAnimeList.Length < 1900; i++)
            {
                finalAnimeList = finalAnimeList + slicedList[i] + "\n";
            }

            seasonEmb.WithTitle("Season : " + season.SeasonYear + " " + season.SeasonName)
                     .WithDescription(finalAnimeList)
                     .WithColor(0x2e51a2);

            await ReplyAsync("", false, seasonEmb.Build());


        }

        [Command("top")]
        public async Task TopAnime([Remainder]string str = "1")
        {
            str = str.ToLower();
            IJikan jikan = new Jikan();
            AnimeTop topAnimeList;
            if (str.Length <= 5)
            {
                int pageNum = Convert.ToInt32(str);
                topAnimeList = await jikan.GetAnimeTop(pageNum);
            }
            else if(str.Equals("airing"))
            {

                topAnimeList = await jikan.GetAnimeTop(TopAnimeExtension.TopAiring);
            }
            else if (str.Equals("upcoming"))
            {
                topAnimeList = await jikan.GetAnimeTop(TopAnimeExtension.TopUpcoming);
            }
            else
            {
                await ReplyAsync("Error!");
                return;
            }

            int place = 1;
            //string animeList = "";
            string[] list = new string[50];
            int index = 0;
            foreach (var topAnime in topAnimeList.Top)
            {
                list[index] = place.ToString() + " [" + topAnime.Title + "](" + topAnime.Url + ")" + "\n";
                place++;
                index++;
            }
            string firstFinalAnimeList = "";
            string secondFinalAnimeList = "";
            string thirdFinalAnimeList = "";
            string al4 = "";
            string al5 = "";

            for (int i = 0; i <= 9; i++)
            {
                firstFinalAnimeList = firstFinalAnimeList + list[i];
            }
            for (int i = 10; i <= 19; i++)
            {
                secondFinalAnimeList = secondFinalAnimeList + list[i];
            }
            for (int i = 20; i <= 29; i++)
            {
                thirdFinalAnimeList = thirdFinalAnimeList + list[i];
            }
            for (int i = 30; i <= 39; i++)
            {
                al4 = al4 + list[i];
            }
            for (int i = 40; i <= 49; i++)
            {
                al5 = al5 + list[i];
            }


            EmbedBuilder firstEmb = new EmbedBuilder();
            firstEmb.WithTitle("Top Anime on MyAnimeList:")
                  .WithDescription(firstFinalAnimeList)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, firstEmb.Build());

            EmbedBuilder secondEmb = new EmbedBuilder();
            secondEmb
                  .WithDescription(secondFinalAnimeList)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, secondEmb.Build());

            EmbedBuilder thirdEmb = new EmbedBuilder();
            thirdEmb
                  .WithDescription(thirdFinalAnimeList)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, thirdEmb.Build());

            EmbedBuilder Emb4 = new EmbedBuilder();
            Emb4
                  .WithDescription(al4)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, Emb4.Build());

            EmbedBuilder Emb5 = new EmbedBuilder();
            Emb5
                  .WithDescription(al5)
                  .WithColor(0x2e51a2);
            await ReplyAsync("", false, Emb5.Build());
            
            
            return;
        }


        [Command("search")]
        public async Task AnimeSearch(string searchTask, int resultNum = 1)
        {
            IJikan jikan = new Jikan();
            await ReplyAsync("Results for " + searchTask + ":");

            AnimeSearchResult animeSearchResult = await jikan.SearchAnime(searchTask);
            int i = 0;
            foreach (var result in animeSearchResult.Results)
            {
                if(i == resultNum)
                {
                    return;
                }
                EmbedBuilder topEmb = new EmbedBuilder();
                topEmb.WithTitle(result.Title)
                      .WithUrl(result.URL)
                      .AddField("Description:\n",
                                result.Description +"\n\n" +
                                $"**Score: {result.Score} Rated:{result.Rated}**\n")
                      .WithThumbnailUrl(result.ImageURL)
                      .WithDescription($"Currently Airing: {result.Airing}\n" + $"Started airing: {result.StartDate}\n" + $"Ended airing: {result.EndDate}")
                      .WithColor(0x2e51a2);


                await ReplyAsync("", false, topEmb.Build());
                i = i + 1;
            }
            return;
        }

        //Not working
        [Command("searchChar")]
        public async Task SearchChar(string searchTask, int resultNum = 1)
        {
            IJikan jikan = new Jikan();
            await ReplyAsync("Results for " + searchTask + ":");

            CharacterSearchResult charSearchResult = await jikan.SearchCharacter(searchTask);
            int i = 0;
            foreach (var result in charSearchResult.Results)
            {
                if (i == resultNum)
                {
                    return;
                }

                //Doesn't needed but nice to have
                /*string[] array = new string[result.AlternativeNames.Count];
                result.AlternativeNames.CopyTo(array, 0);
                int arrayLenght = array.Length;
                string alternativeNames = "";

                for(int n = 0; n <= arrayLenght - 1; n++)
                {
                    alternativeNames = alternativeNames + array[n] + "\n";
                }
                */
                string anime = "";
                foreach(MALSubItem subItem in result.Animeography)
                {
                    anime = anime + "[" + subItem.Name + "](" + subItem.Url + ")" + "\n";
                }

                if(anime.Length > 2048)
                {
                    EmbedBuilder topEmb = new EmbedBuilder();
                    topEmb.WithTitle(result.Name)
                          .WithUrl(result.URL)
                          .WithDescription("Anime:\n" +
                                    "Embedded anime list was to large! Too many anime <a:AYAYATriggered:623230429486645261>")
                          .WithThumbnailUrl(result.ImageURL)
                          .WithColor(0x2e51a2);
                    await ReplyAsync("", false, topEmb.Build());
                }
                else
                {

                    EmbedBuilder topEmb = new EmbedBuilder();
                    topEmb.WithTitle(result.Name)
                          .WithUrl(result.URL)
                          .WithDescription("Anime:\n" +
                                    anime)
                          .WithThumbnailUrl(result.ImageURL)
                          .WithColor(0x2e51a2);
                    await ReplyAsync("", false, topEmb.Build());
                }
                i = i + 1;
            }
            return;
        }

        //Not working
        [Command("topChars")]
        public async Task TopChars([Remainder]string str)
        {
            /*
            str = str.ToLower();
            IJikan jikan = new Jikan();
            CharactersTop charTop;
            int pageNum = Convert.ToInt32(str);
            charTop = await jikan.GetCharactersTop(pageNum);

            string charList = "";
            foreach (var topChar in charTop.Top)
            {
                charList = charList + topChar.Rank + " [" + topChar.Name + "](" + topChar.Url + ")\n";
            }

            string[] slicedList = charList.Split("\n");
            string finalCharList = "";

            for (int i = 0; i <= slicedList.Length - 1; i++)
            {
                finalCharList = finalCharList + slicedList[i] + "\n";
            }

            string firstString = "";
            string secondString = "";
            int lenght = finalCharList.Length;
            if(lenght > 2048)
            {
                firstString = finalCharList.Substring(0, 2048);
                secondString = finalCharList.Substring(2049, lenght - 2048);
            }


            EmbedBuilder topEmb = new EmbedBuilder();
            topEmb.WithTitle("Top Anime on MyAnimeList")
                  .WithDescription(firstString + secondString);
            await ReplyAsync("", false, topEmb.Build());
            return;
            */
        }
    }
	
	[Group("admin")]
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        const long o1 = 208979474988007425;
        const long o2 = 411619522752282625;

        [Command("kick")]
        public async Task KickUser(SocketGuildUser userName)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            var id = userName.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                await ReplyAsync($"Bai Bai <@{id}>!");
                await ReplyAsync("<:remV:639621688887083018>");
                await userName.KickAsync();
                return;
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("ban")]
        public async Task BanAsync(SocketGuildUser userName)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            var id = userName.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                await ReplyAsync($"You're banned <@{id}>!");
                await ReplyAsync("<:remV:639621688887083018>");
                await userName.BanAsync();
                return;
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("purge")]
        public async Task Purge(int amount)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            var oldMessage = Context.Message;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(oldMessage, Direction.Before, amount).FlattenAsync();
                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
                await oldMessage.DeleteAsync();
                IMessage botMsg = await ReplyAsync("Messages deleted!");
                IMessage botMsg2 = await ReplyAsync("<:remV:639621688887083018>");
                await Task.Delay(2500);
                await botMsg.DeleteAsync();
                await botMsg2.DeleteAsync();
                return;
            }
            else
            {
                await ReplyAsync($"You aren't allowed to user purge, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Group("Channel")]
        public class ChannelModule : ModuleBase<SocketCommandContext>
        {
            [Command("changeDesc")]
            public async Task ChangeDesc(ITextChannel channel, string str)
            {
                IMessage msg = Context.Message;
                await msg.DeleteAsync();
                await channel.ModifyAsync(x => 
                {
                    x.Topic = str;
                });
                IMessage botMsg = await ReplyAsync($"**Description of** <#{channel.Id}> **successfully changed to** \"{str}\"**!**");
                IMessage botMsg2 = await ReplyAsync("<:remV:639621688887083018>");
                await Task.Delay(2500);
                await botMsg.DeleteAsync();
                await botMsg2.DeleteAsync();
                return;
            }

            //Needs some bugfixing, means it doesn't work probably 
            [Command("changePos")]
            public async Task ChangePosition(IGuildChannel channel,int changePos)
            {
                IMessage msg = Context.Message;
                await channel.ModifyAsync(x =>
                {
                    x.Position = channel.Position + changePos + 1;
                });
                IMessage botMsg = await ReplyAsync($"Successfully moved channel by {changePos}");
                IMessage botMsg2 = await ReplyAsync("<:remV:639621688887083018>");
                await Task.Delay(2500);
                await botMsg.DeleteAsync();
                await botMsg2.DeleteAsync();
                await msg.DeleteAsync();
                return;
            }
        }

        [Command("delMessage")]
        public async Task DelMessage(ulong id)
        {
            IMessage userMessage = Context.Message;
            IMessage message = await Context.Channel.GetMessageAsync(id);
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                IMessage botmsg1 = await ReplyAsync("\"" + message.Content + "\"" + " **by** " + $"<@{message.Author.Id}>" + " **gets deleted!**");
                IMessage botmsg2 = await ReplyAsync("<:remV:639621688887083018>");
                await message.DeleteAsync();
                await Task.Delay(4000);
                IMessage botmsg3 = await ReplyAsync("Successful!");
                await botmsg1.DeleteAsync();
                await botmsg2.DeleteAsync();
                await Task.Delay(2000);
                await botmsg3.DeleteAsync();
                await userMessage.DeleteAsync();
                return;
            }
            else
            {
                await ReplyAsync("Only my masters can do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("status")]
        public async Task Status([Remainder]string echo)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                string[] split = echo.Split(" ");
                int length = split.Length;
                echo = "";
                if (split[0].Equals("streaming"))
                {
                    for (int i = 2; i <= length - 1; i++)
                    {
                        echo = echo + " " + split[i];
                    }

                    await Program._client.SetGameAsync(echo, split[1], ActivityType.Streaming);
                    await ReplyAsync("Status was set to: Streaming" + "**" + echo + "**" + " with the url: " + split[1]);
                    await ReplyAsync("<a:remspin:643170585668747298>");
                    return;
                }
                else
                {
                    for (int i = 1; i <= length - 1; i++)
                    {
                        echo = echo + " " + split[i];
                    }
                    switch (split[0])
                    {
                        case "listening":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Listening);
                            await ReplyAsync("Status was set to: Listening to" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        case "playing":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Playing);
                            await ReplyAsync("Status was set to: Playing" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        case "watching":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Watching);
                            await ReplyAsync("Status was set to: Watching" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        default:
                            await ReplyAsync("listening/playing/watching + status");
                            await ReplyAsync("<:hmpfREM:476840909334511677>");
                            return;
                    }
                }
            }
            else
            {
                await ReplyAsync("Only my masters can do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

    }
	
	public class Experimental : ModuleBase<SocketCommandContext>
    {
        [Command("exper")]
        public async Task Exper()
        {
            await ReplyAsync("");
        }
    }
}