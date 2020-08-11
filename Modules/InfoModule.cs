using Discord;
using Discord.Commands;
using Discord.WebSocket;

using System;
using System.Threading.Tasks;


namespace Akan.Modules
{
    public class InfoModule : ModuleBase <SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingCommand() => await ReplyAsync($"Pong {(DateTimeOffset.Now - Context.Message.Timestamp).Milliseconds}ms!");
    }
}