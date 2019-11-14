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

    [Group("help")]
    public class HelpModule : ModuleBase<SocketCommandContext>
    {
        [Command("")]
        public async Task ConverterAsync5()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("What can I do for you, senpai?")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!help**\" + [**category**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Categories:**__",
                          "**\tchat**\n" +
                          "\tDefault chat commands\n\n" +
                          "**\tmath**\n" +
                          "\tMath related commands\n\n" +
                          "**\tadmin**\n" +
                          "\tCommands that require admin permission")
                .WithImageUrl("https://media.giphy.com/media/mGVXF7qJ22dxGyJLng/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("math")]
        public async Task MathAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Let's do some homework together!")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!math**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Math Commands:**__",
                          "**\tpow** + [**number**] + [**pow**]\n" +
                          "\tCalculates the power of a number\n\n" +
                          "**\tsqrt** + [**number**]\n" +
                          "\tCalculates the squareroot of a number\n\n" +
                          "**\tabc** + [**a**] + [**b**] + [**c**]\n" +
                          "\tCalculates ax²+bx²+c = 0")
                .WithImageUrl("https://media.giphy.com/media/Kp9GUdNUYAqhq/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("chat")]
        public async Task ChatAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Let's chat a little bit <3")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Chat Commands:**__",
                          "\t**rep** + [**message**]\n" +
                          "\tRepeats a  message (Only numbers, letters, ? and !) in emotes\n\n" +
                          "\t**say** + [**message**]\n" +
                          "\tEchos a message\n\n" +
                          "\t**invite**\n" +
                          "\tInvite link to add the bot to your server\n\n" +
                          "\t**test**\n" +
                          "\tJust a test message\n\n" +
                          "\t**8ball**\n" +
                          "\tAsk a yes/no question and get the answer!" )
                .WithImageUrl("https://media.giphy.com/media/NjvprsVwBehvq/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("admin")]
        public async Task AdminAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Only for my masters!")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!admin**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**Admin Commands:**__",
                          "\t**ban** + [**@user**]\n" +
                          "\tBans a user from the server\n\n" +
                          "\t**kick** + [**@user**]\n" +
                          "\tKicks a user from the server\n\n" +
                          "\t**status** + [**playing/watching/listening**] + [**message**]\n" +
                          "\tSets the status of the bot\n\n" +
                          "\t**status** + [**streaming + twitch url**] + [**message**]\n" +
                          "\tSets a special streaming status to promote someone")
                .WithImageUrl("https://media.giphy.com/media/kl3VSpDkwCYTK/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }
    }
}
