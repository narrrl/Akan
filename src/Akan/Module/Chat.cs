using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Net;

namespace Akan.Module
{
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

                    else if (c == 'ä' || c == 'ö' || c == 'ü')
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
}
