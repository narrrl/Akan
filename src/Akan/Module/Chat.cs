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


    }

    [Group("math")]
    public class ChatModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("pow")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);


        // get confirmation
        [Command("sqrt")]
        [Summary("Test command.")]
        public async Task PingAsync()
        {
            await ReplyAsync("Successful!");
        }


    }
