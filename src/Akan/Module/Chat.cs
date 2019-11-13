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
    [Group("chat")]
    public class ChatModule : ModuleBase<SocketCommandContext>
    {
        [Group("info")]
        public class InfoModule : ModuleBase<SocketCommandContext>
        {
            // ~say hello world -> hello world
            [Command("say")]
            [Summary("Echoes a message.")]
            public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
                => ReplyAsync(echo);

            // ReplyAsync is a method on ModuleBase 
        }
    }

    [Group("test")]
    public class Repeat : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        [Summary("Test command.")]
        public async Task PingAsync()
        {
            await ReplyAsync("Successful!");
        }
    }
}
