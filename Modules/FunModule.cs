using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Akan.Services;

namespace Akan.Modules
{
    public class FunModule : ModuleBase<SocketCommandContext>
    {
        public PictureService PictureService { get; set; }

        [Command("neko")]
        public async Task NekoAsync()
        {
            var image = await PictureService.GetPictureWithTagAsync("neko");

            // stream.Seek(0, SeekOrigin.Begin);

            // await Context.Channel.SendFileAsync(stream, "neko.png");

            EmbedBuilder emb = new EmbedBuilder();

            emb.WithImageUrl(image.getUrl()).WithColor(Akan._color);

            await ReplyAsync(null, false, emb.Build());
        }

        [Command("ero")]
        [RequireNsfw]
        public async Task EroCommand()
        {
            var image = await PictureService.GetPictureWithTagAsync("ero");

            EmbedBuilder emb = new EmbedBuilder();

            emb.WithImageUrl(image.getUrl()).WithColor(Akan._color);

            await ReplyAsync(null, false, emb.Build());
        }
    }
}