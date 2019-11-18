using Discord.Commands;
using System.Threading.Tasks;

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
