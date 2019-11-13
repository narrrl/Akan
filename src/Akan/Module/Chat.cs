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


}

    [Group("math")]
    public class ChatModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("pow")]
        [Summary("Echoes a message.")]
        public Task PowAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            String[] splittedEcho = echo.Split(" ");
            double number = Convert.ToDouble(splittedEcho[0]);
            double pow = Convert.ToDouble(splittedEcho[1]);
            double solution = Math.Pow(number, pow);
            echo = Convert.ToString(solution);
            return ReplyAsync(echo);
    }


        // get confirmation
        [Command("sqrt")]
        [Summary("Test command.")]
        public async Task SqrtAsync()
        {
            await ReplyAsync("Successful!");
        }


    }
