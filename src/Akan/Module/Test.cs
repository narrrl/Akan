using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akan.Module
{
    public class Repeat : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task PingAsync()
        {
            await ReplyAsync("Successful!");
        }
    }
}
