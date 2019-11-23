using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Net;
using Akan.Services;
using Newtonsoft.Json.Linq;

namespace Akan.Module
{
    public class Chat : ModuleBase<SocketCommandContext>
    {
        static int[] asciiSpecial = { 196, 228, 214, 246, 220, 252 };
        static string[] lewdKeyWords = { "femdom", "classic", 
            "erofeet", "erok", "les","hololewd", 
            "lewdk", "keta", "feetg", "nsfw_neko_gif", "eroyuri", 
            "kiss", "8ball", "kuni", "tits", "pussy_jpg", "cum_jpg", "pussy", 
            "lewd", "cum", "smallboobs", "Random_hentai_gif", 
            "fox_girl", "nsfw_avatar", "hug", "gecg", "boobs", "feet", "lewdkemo",
            "solog", "bj", "yuri", "trap", "anal", "blowjob", "holoero", 
            "gasm", "hentai", "futanari", "ero", "solo", "pwankg", "eron", "erokemo" };
        static string[] sfwKeyWords = { "tickle", "ngif", "meow", "poke", "slap", "cuddle", "spank",
            "goose", "avatar", "pat", "smug", "kemonomimi", "holo", "wallpaper", "woof", "baka", "feed",
            "neko", "waifu" };


        [Command("choice")]
        public async Task choice([Remainder] string str)
        {
            string[] strArray = str.Split(",");
            int randIdx = new Random().Next(strArray.Length);
            if(!strArray[randIdx].StartsWith(" "))
            {
                strArray[randIdx] = " " + strArray[randIdx];
            }

            EmbedBuilder emb = new EmbedBuilder();
            emb.WithTitle("Without any doubts")
                .WithDescription("In any case" + strArray[randIdx])
                .WithColor(Color.DarkMagenta)
                .WithFooter(Context.User.Username, Context.User.GetAvatarUrl().ToString())
                .WithCurrentTimestamp();

            await ReplyAsync("",false, emb.Build());


        }

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
            [Command("info")]
            [Summary("info command.")]
            public async Task PingAsync()
            {
                int ping = Context.Client.Latency;
                ulong guild = Context.Guild.Id;

                EmbedBuilder emb = new EmbedBuilder();
                emb.WithTitle($"Successful with a ping of {ping} ms")
                    .WithDescription($"Guild: {Context.Client.GetGuild(guild)}\n" +
                    $"User: <@{Context.User.Id}>")
                    .WithColor(Color.DarkMagenta)
                    .WithThumbnailUrl(Context.Guild.IconUrl);
                await ReplyAsync("",false, emb.Build());
            }
        }

        [Command("mock")]
        public async Task Mock([Remainder]string userTemp)
        {
			var userName = Context.User.Username;
            string avatarUrl = Context.User.GetAvatarUrl();
            var message = Context.Message;									 					  
            string userStr = userTemp.ToLower();
            string[] userWordsArray = userStr.Split(" ");
            int asciiInt;
            string finalMessage = "";
            Random rand = new Random();
            int randNumb;
            for(int i = 0; i <= userWordsArray.Length - 1; i++)
            {
                string tempWord = "";
                foreach (char ch in userWordsArray[i])
                {
                    string chaStr = "";
                    asciiInt = ch;
                    chaStr = ch.ToString();
                    if ((asciiInt >= 97 && asciiInt <= 122)|| (Array.TrueForAll(asciiSpecial, val => (asciiInt == val))))
                    {
                        randNumb = rand.Next(10000);
                        if(randNumb <= 5000)
                        {
                            chaStr = chaStr.ToUpper();
                        }
                    }
                    tempWord = tempWord + chaStr;
                }
                finalMessage = finalMessage + " " + tempWord;
            }
            EmbedBuilder emb = new EmbedBuilder();
            emb.WithDescription(finalMessage)
                .WithColor(Color.DarkMagenta)
                .WithFooter(userName,avatarUrl);
            await message.DeleteAsync();
            await ReplyAsync("",false,emb.Build());
        }

        public class NSFWModule : ModuleBase<SocketCommandContext>
        {
            [Command("neko")]
            public async Task SfwImage()
            {
                JToken obj;
                WebClient http = new WebClient();
                obj = JToken.Parse(http.DownloadString("https://nekos.life/api/neko"));
                
                EmbedBuilder neko = new EmbedBuilder();

                neko.WithTitle("Nyaa~")
                    .WithImageUrl($"{obj.Value<string>("neko")}")
                    .WithColor(Color.DarkMagenta);

                await ReplyAsync("", false, neko.Build());
            }

            [Command("pat")]
            public async Task Pat(SocketGuildUser userName)
            {
                JToken obj;
                WebClient http = new WebClient();
                obj = JToken.Parse(http.DownloadString("https://nekos.life/api/v2/img/pat"));
                EmbedBuilder neko = new EmbedBuilder();

                neko.WithDescription($"<@{Context.User.Id}> patted <@{userName.Id}>! <a:blushDS:639619041920548884>")
                    .WithImageUrl($"{obj.Value<string>("url")}")
                    .WithColor(Color.DarkMagenta);

                await ReplyAsync("", false, neko.Build());

            }

            [Command("LewdNeko")]
            public async Task NsfwImage()
            {
                var channel = Context.Channel as ITextChannel;
                if (channel.IsNsfw)
                {
                    JToken obj;
                    WebClient http = new WebClient();
                    obj = JToken.Parse(http.DownloadString("https://nekos.life/api/lewd/neko"));

                    EmbedBuilder neko = new EmbedBuilder();

                    neko.WithTitle("Nyaa~")
                        .WithImageUrl($"{obj.Value<string>("neko")}")
                        .WithColor(Color.DarkMagenta);

                    await ReplyAsync("", false, neko.Build());
                }
                else
                {
                    await ReplyAsync("Not here senpai!");
                    await ReplyAsync("<a:blushDS:639619041920548884>");
                }
            }

            [Command("randLewd")]
            public async Task randomLewd()
            {
                int randInd = new Random().Next(lewdKeyWords.Length - 1);
                string randKeyWord = lewdKeyWords[randInd];
                JToken obj;
                WebClient http = new WebClient();
                obj = JToken.Parse(http.DownloadString("https://nekos.life/api/v2/img/" + randKeyWord ));

                EmbedBuilder neko = new EmbedBuilder();

                neko.WithTitle("Nyaa~")
                    .WithImageUrl($"{obj.Value<string>("url")}")
                    .WithColor(Color.DarkMagenta)
                    .WithFooter("Keyword is: " + randKeyWord, Context.Guild.IconUrl);

                await ReplyAsync("", false, neko.Build());
            }

            [Command("lewd")]
            public async Task Lewd([Remainder] string str)
            {
                bool inArray = false;
                for (int i = 0; i <= lewdKeyWords.Length - 1; i++)
                {
                    if (lewdKeyWords[i].Equals(str))
                    {
                        inArray = true;
                    }
                }
                if (inArray)
                {
                    JToken obj;
                    WebClient http = new WebClient();
                    obj = JToken.Parse(http.DownloadString("https://nekos.life/api/v2/img/" + str));

                    EmbedBuilder neko = new EmbedBuilder();

                    neko.WithTitle("Nyaa~")
                        .WithImageUrl($"{obj.Value<string>("url")}")
                        .WithColor(Color.DarkMagenta)
                        .WithFooter("Keyword is: " + str, Context.Guild.IconUrl);

                    await ReplyAsync("", false, neko.Build());
                    return;
                }
                else
                {
                    await ReplyAsync("Unknown keyword");
                    return;
                }
            }

            [Command("lewdHelp")]
            public async Task lewdHelp()
            {
                string final = "";
                for(int i = 0; i <= lewdKeyWords.Length - 1; i++)
                {
                    final = final + lewdKeyWords[i] + "\n";
                }
                EmbedBuilder emb = new EmbedBuilder();
                emb.WithDescription(final)
                    .WithFooter("Lewd Keywords", Context.Guild.IconUrl)
                    .WithColor(Color.DarkMagenta);
                await ReplyAsync("", false, emb.Build());
                return;
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
                await ReplyAsync(Methods.RegionalIndicatorConverter(b));
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
                EmbedBuilder emb = new EmbedBuilder();
                if (Double.IsNaN(solution1) && !Double.IsNaN(solution2))
                {
                    emb.WithDescription($"Set of solutions for **({(int)a}x²) + ({(int)b}x) + ({(int)c}) = 0** is:\n" +
                    "x: **" + sol2 + "**")
                    .WithColor(Color.DarkMagenta);
                }
                else if (!Double.IsNaN(solution1) && Double.IsNaN(solution2))
                {
                    emb.WithDescription($"Set of solutions for **({(int)a}x²) + ({(int)b}x) + ({(int)c}) = 0** is:\n" +
                    "x: **" + sol1 + "**")
                    .WithColor(Color.DarkMagenta);
                }
                else if (Double.IsNaN(solution1) && Double.IsNaN(solution2))
                {
                    emb.WithDescription($"Set of solutions for **({(int)a}x²) + ({(int)b}x) + ({(int)c}) = 0** is:\n" +
                    "Set of solutions is **empty**")
                    .WithColor(Color.DarkMagenta);
                }
                else
                {
                    emb.WithDescription($"Set of solutions for ({(int)a}x²) + ({(int)b}x) + ({(int)c}) = 0 is:\n" +
                    "x_1: **" + sol1 + "** x_2: **" + sol2 + "**")
                    .WithColor(Color.DarkMagenta);
                }
                    
                await ReplyAsync("", false, emb.Build());
                
            }

        }

    }
}
