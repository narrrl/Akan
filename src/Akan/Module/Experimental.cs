using Discord;
using Discord.Commands;
using Discord.WebSocket;
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

        [Command("exEmbed")]
        public async Task ExEmbed()
        {
            var left = Emote.Parse("<a:lickL:511557281033486366>");
            var right = Emote.Parse("<a:lickR:511554012005400577>");
            EmbedBuilder emb = new EmbedBuilder().WithTitle("This is a title");
            var botMessage = await ReplyAsync("", false, emb.Build());
            await botMessage.AddReactionAsync(left);
            await botMessage.AddReactionAsync(right);
            SocketUser user = Context.User;
            var reactions = botMessage.Reactions;
            /*foreach(var react in reactions)
            {
                if (react.Equals(left))
                {
                    SocketReaction react1 = react.ToString();
                }
            }
            Program._client.ReactionAdded(botMessage,Context.Channel)
            */

            

        }
    }
}
