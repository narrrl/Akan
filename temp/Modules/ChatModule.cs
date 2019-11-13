using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;

namespace AkanDiscordBot.Modules
{
    [Group("chat")]
    public class ChatModule : ModuleBase<SocketCommandContext>
    {
        [Group("test")]
        public class TestModule : ModuleBase<SocketCommandContext>
        {
            // ~say hello world -> hello world
            [Command("say")]
            [Summary("Echoes a message.")]
            public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
                => ReplyAsync(echo);

            // ReplyAsync is a method on ModuleBase 
        }
    }
}
