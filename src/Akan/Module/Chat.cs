using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Akan.Module
{

    public class SayModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);
    }

    public class TestModule : ModuleBase<SocketCommandContext>
    {
        // get confirmation
        [Command("test")]
        [Summary("Test command.")]
        public async Task PingAsync()
        {
            await ReplyAsync("Successful!");
        }
    }

    public class RepModule : ModuleBase<SocketCommandContext>
    {
        // echoes input in reginoal indicator letters
        [Command("rep")]
        [Summary("Echoes input in reginoal indicator letters")]
        public async Task RepeatAsync([Remainder] string input)
        {
            string b = input.ToLower();
            string d = "";

            foreach (char c in b)
            {
                if (c.ToString() == " ")
                {
                    d = d + "\n";
                }

                else if (c.ToString() == "1" || c.ToString() == "2" || c.ToString() == "3" || c.ToString() == "4" || c.ToString() == "5" || c.ToString() == "6" || c.ToString() == "7" || c.ToString() == "8" || c.ToString() == "9" || c.ToString() == "0")
                {
                    switch (c.ToString())
                    {
                        case "1":
                            d = d + ":one: ";
                            break;

                        case "2":
                            d = d + ":two: ";
                            break;

                        case "3":
                            d = d + ":three: ";
                            break;

                        case "4":
                            d = d + ":four: ";
                            break;

                        case "5":
                            d = d + ":five: ";
                            break;

                        case "6":
                            d = d + ":six: ";
                            break;

                        case "7":
                            d = d + ":seven: ";
                            break;

                        case "8":
                            d = d + ":eight: ";
                            break;

                        case "9":
                            d = d + ":nine: ";
                            break;

                        case "0":
                            d = d + ":zero: ";
                            break;
                    }
                }

                else
                {
                    d = d + ":regional_indicator_" + c + ": ";
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
            if(randNum <= 50)
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
        [Command("invite")]
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
