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
    using Discord;
    using Discord.Commands;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    namespace Testbot.Modules
    {
        public class help : ModuleBase<SocketCommandContext>
        {
            [Command("help")]
            public async Task ConverterAsync5()
            {
                EmbedBuilder help = new EmbedBuilder();

                help.WithTitle("What can I do, senpai?")
                    .WithDescription("Type \"akan!\" + command")
                    .WithColor(Color.DarkMagenta)
                    .AddField("Commands:",
                              "1: \trep + Type in what you want to repeat in Icon-Letters\n" +
                              "2: \tsay + What the bot should echo\n" +
                              "3: \ttest")
                    .WithImageUrl("https://i.imgur.com/DSLHEzI.png");

                await ReplyAsync("", false, help.Build());
            }
        }
    }

}
