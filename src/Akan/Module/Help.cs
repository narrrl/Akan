using Discord;
using Discord.Commands;
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
                          "\tCommands that require admin permission\n\n" +
                          "\t**myAnimeList**\n" +
                          "\tFor all you dirty weebs out there")
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
                          "\t**inviteBot**\n" +
                          "\tInvite link to add the bot to your server\n\n" +
                          "\t**test**\n" +
                          "\tJust a test message\n\n" +
                          "\t**8ball**\n" +
                          "\tAsk a yes/no question and get the answer!\n\n" +
                          "\t**invite**\n"+
                          "\tCreates a single use invite link for the current channel\n\n" +
                          "\t**neko**\n" +
                          "\tSends a random sfw neko pic from nekos.life\n\n" +
                          "\t**lewdNeko**\n" +
                          "\tSends random nsfw neko pic from nekos.life\n\n" +
                          "\t**rand** + [**min**] + [**max**]\n" +
                          "\tSends a random number between min and max")
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
                          "\tSets a special streaming status to promote someone\n\n" +
                          "\t**purge** + [**amount**]\n" +
                          "\tDeletes an amount of messages\n\n"+
                          "\t**createInvite** [**#channel**] + [**age**] + [**maxUses**]\n + [**Unique(true/false)**] + [**temp(true/false)**]\n" +
                          "\tCreates an invite for a channel with an age in seconds, max uses, if it'd be unique or temp\n\n" + 
                          "\t**delMessage** + [**MessageID**]\n" +
                          "\tDeletes a specific message\n\n" +
                          "\t**channel** **changeDesc** + [**#channel**] + [**\"Description\"**]\n" +
                          "\tChange the description of a channel")
                .WithImageUrl("https://media.giphy.com/media/kl3VSpDkwCYTK/giphy.gif");

            await ReplyAsync("", false, help.Build());
        }

        [Command("myAnimeList")]
        public async Task MALAsync()
        {
            EmbedBuilder help = new EmbedBuilder();

            help.WithTitle("Disgusting weebs!")
                .WithThumbnailUrl("https://i.imgur.com/m6VoMgy.png")
                .WithDescription("Type \"**akan!mal**\" + [**command**]")
                .WithColor(Color.DarkMagenta)
                .AddField("__**MyAnimeList Commands:**__",
                          "\t**top** + [**airing/upcoming/page number**]\n" +
                          "\tLists all top airing/upcoming anime. If you want to see the overall"+
                          "\ttop anime type the page number for example: 1 (Every page has 50 entries)\n\n" +
                          "\t**season** + [**page**] + [**year**] + [**spring/summer/fall/winter**]\n" +
                          "\tLists all anime that aired in that season. No year and season shows current season\n\n" +
                          "\t**search** + [**\"anime name\"**] + [**total results**]\n"+
                          "\tSearches for anime on MAL\n\n" +
                          "\t**nextSeason** + [**page**]\n" +
                          "\tShows next season\n\n" +
                          "\t**today** + [**pageNumber**]\n" +
                          "\tShows the anime airing today with time")
                .WithImageUrl("https://i.imgur.com/6NenalB.gif");

            await ReplyAsync("", false, help.Build());
        }
    }
}
