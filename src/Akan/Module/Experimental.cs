using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using System.Net;
using Akan.Services;

namespace Akan.Module
{
    public class Experimental : ModuleBase<SocketCommandContext>
    {
        [Command("exper")]
        public async Task Exper()
        {
            await ReplyAsync("");
        }
    }
}
