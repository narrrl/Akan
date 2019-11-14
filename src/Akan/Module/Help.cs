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
        [Group("help")]
        public class HelpModule : ModuleBase<SocketCommandContext>
        {
            [Command("")]
            public async Task ConverterAsync5()
            {
                EmbedBuilder help = new EmbedBuilder();

                help.WithTitle("What can I do, senpai?")
                    .WithDescription("Type \"akan!\" + category")
                    .WithColor(Color.DarkMagenta)
                    .AddField("Commands:",
                              "1: \tchat\n" +
                              "2: \tmath\n" +
                              "3: \tadmin")
                    .WithImageUrl("https://media.giphy.com/media/mGVXF7qJ22dxGyJLng/giphy.gif");

                await ReplyAsync("", false, help.Build());
            }

            [Command("math")]
            public async Task MathAsync()
            {
                EmbedBuilder help = new EmbedBuilder();

                help.WithTitle("Let's do some homework together!")
                    .WithDescription("Type \"akan!math\" + command")
                    .WithColor(Color.DarkMagenta)
                    .AddField("Math Commands:",
                              "1: \tpow + number + pow\n" +
                              "2: \tsqrt + number\n" +
                              "3: \tabc + a + b + c")
                    .WithImageUrl("https://media.giphy.com/media/Kp9GUdNUYAqhq/giphy.gif");

                await ReplyAsync("", false, help.Build());
            }

            [Command("chat")]
            public async Task ChatAsync()
            {
                EmbedBuilder help = new EmbedBuilder();

                help.WithTitle("Let's chat a little bit <3")
                    .WithDescription("Type \"akan!\" + command")
                    .WithColor(Color.DarkMagenta)
                    .AddField("Chat Commands:",
                              "1: \trep + convert letters to emotes\n" +
                              "2: \tsay + Write something\n" +
                              "3: \ttest\n")
                    .WithImageUrl("https://media.giphy.com/media/NjvprsVwBehvq/giphy.gif");

                await ReplyAsync("", false, help.Build());
            }

            [Command("admin")]
            public async Task AdminAsync()
            {
                EmbedBuilder help = new EmbedBuilder();

                help.WithTitle("Only for my masters!")
                    .WithDescription("Type \"akan!admin\" + command")
                    .WithColor(Color.DarkMagenta)
                    .AddField("Admin Commands:",
                              "1: \tban + @user\n"+
                              "2: \tkick + @user")
                    .WithImageUrl("https://media.giphy.com/media/kl3VSpDkwCYTK/giphy.gif");

                await ReplyAsync("", false, help.Build());
            }
        }
    }

}
