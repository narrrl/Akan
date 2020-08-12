using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Akan.Services;
using Akan.Util;
using System;
using System.Text;

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

        [Command("mock")]
        public async Task MockCommand([Remainder]string echo)
        {
            Random rand = new Random();
            StringBuilder output = new StringBuilder();

            foreach (char c in echo)
            {
                int num = rand.Next(0, 2);
                char tmp = num == 0 ? Char.ToLower(c) : Char.ToUpper(c);
                output.Append(tmp);
            }
            await ReplyAsync(output.ToString());
        }

        [Command("rep")]
        public async Task RepCommand([Remainder]string echo) => await ReplyAsync(EmoteConverter.ConvertToRegionalIndicators(echo));
    }
}