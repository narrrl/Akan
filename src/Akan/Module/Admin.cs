using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akan.Module
{
    public class CustomEmbeds : ModuleBase<SocketCommandContext>
    {
        [Command("embed")]
        public async Task CustomEmbed([Remainder]string context) // customEmbed .title("titel").desc("desc").color("color").field("title","field")
        {
            const long o1 = 208979474988007425;
            const long o2 = 411619522752282625;
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                var userMsg = Context.Message;
                string userAvatar = Context.User.GetAvatarUrl();
                string[] dotSplit = context.Split("|"); //.title("titel")|.desc("desc")|.field("title","field")
                bool title = false, desc = false, field = false, pic = false;
                int titleInd = 0, fieldInd = 0, descInd = 0, picInd = 0;
                int dotSplitLenght = dotSplit.Length;
                string[] dotContent = new string[dotSplitLenght];
                for (int i = 0; i <= dotSplitLenght - 1; i++)
                {
                    string[] tempArray = dotSplit[i].Split("(");
                    string temp = tempArray[0].Substring(1);
                    string temp2 = tempArray[1].Substring(0, tempArray[1].Length - 1);
                    switch (temp)
                    {
                        case "title":
                            title = true;
                            dotContent[i] = temp2;
                            titleInd = i;
                            break;
                        case "desc":
                            desc = true;
                            dotContent[i] = temp2;
                            descInd = i;
                            break;
                        case "field":
                            field = true;
                            string[] tempAr = temp2.Split(",");
                            dotContent[i] = tempAr[0] + "\"" + tempAr[1];
                            fieldInd = i;
                            break;
                        case "pic":
                            pic = true;
                            dotContent[i] = temp2;
                            picInd = i;
                            break;
                        default:
                            break;
                    }
                }

                EmbedBuilder customEmb = new EmbedBuilder();

                if (title)
                {
                    customEmb.WithTitle(dotContent[titleInd]);
                }
                if (desc)
                {
                    customEmb.WithDescription(dotContent[descInd]);
                }
                if (field)
                {
                    string[] fieldContent = dotContent[fieldInd].Split("\"");
                    customEmb.AddField(fieldContent[0],
                        fieldContent[1]);
                }
                if (pic)
                {
                    customEmb.WithImageUrl(dotContent[picInd]);
                }
                customEmb.WithThumbnailUrl(userAvatar);
                await userMsg.DeleteAsync();

                await ReplyAsync("", false, customEmb.Build());
                return;
            }
            else
            {
                await ReplyAsync($"You aren't allowed to do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }
    }

    [Group("admin")]
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        const long o1 = 208979474988007425;
        const long o2 = 411619522752282625;

        [Command("kick")]
        public async Task KickUser(SocketGuildUser userName)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            var id = userName.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                await ReplyAsync($"Bai Bai <@{id}>!");
                await ReplyAsync("<:remV:639621688887083018>");
                await userName.KickAsync();
                return;
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("status")]
        public async Task Activity([Remainder]string status)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                string statusLowerCase = status.ToLower();
                switch (statusLowerCase)
                {

                    case "online":
                        await Program._client.SetStatusAsync(UserStatus.Online);
                        var messagebot = await ReplyAsync("Status set to online!");
                        await Task.Delay(2000);
                        await messagebot.DeleteAsync();
                        return;
                    case "afk":
                        await Program._client.SetStatusAsync(UserStatus.AFK);
                        var messagebot2 = await ReplyAsync("Status set to online!");
                        await Task.Delay(2000);
                        await messagebot2.DeleteAsync();
                        return;
                    case "donotdisturb":
                        await Program._client.SetStatusAsync(UserStatus.DoNotDisturb);
                        var messagebot3 = await ReplyAsync("Status set to DoNotDisturb!");
                        await Task.Delay(2000);
                        await messagebot3.DeleteAsync();
                        return;
                    case "invisible":
                        await Program._client.SetStatusAsync(UserStatus.Invisible);
                        var messagebot4 = await ReplyAsync("Status set to invisible!");
                        await Task.Delay(2000);
                        await messagebot4.DeleteAsync();
                        return;
                    case "idle":
                        await Program._client.SetStatusAsync(UserStatus.Idle);
                        var messagebot5 = await ReplyAsync("Status set to idle!");
                        await Task.Delay(2000);
                        await messagebot5.DeleteAsync();
                        return;
                    case "offline":
                        await Program._client.SetStatusAsync(UserStatus.Offline);
                        var messagebot6 = await ReplyAsync("Status set to offline!");
                        await Task.Delay(2000);
                        await messagebot6.DeleteAsync();
                        return;
                    default:
                        await ReplyAsync("Error!");
                        return;
                }
            }
            else
            {
                await ReplyAsync($"You aren't allowed to do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("ban")]
        public async Task BanAsync(SocketGuildUser userName)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            var id = userName.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                await ReplyAsync($"You're banned <@{id}>!");
                await ReplyAsync("<:remV:639621688887083018>");
                await userName.BanAsync();
                return;
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("revRole")]
        public async Task RemoveRole([Remainder] string str)
        {
            var message = Context.Message;
            await message.DeleteAsync();
            var userTemp = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (userTemp.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                var user = Context.User;
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == str);
                await (user as IGuildUser).RemoveRoleAsync(role);
            }
            else
            {
                await ReplyAsync($"You aren't allowed to do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("addRole")]
        public async Task AddRole([Remainder] string str)
        {
            var message = Context.Message;
            await message.DeleteAsync();
            var userTemp = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (userTemp.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                var user = Context.User;
                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == str);
                await (user as IGuildUser).AddRoleAsync(role);
            }
            else
            {
                await ReplyAsync($"You aren't allowed to do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("purge")]
        public async Task Purge(int amount)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            var oldMessage = Context.Message;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(oldMessage, Direction.Before, amount).FlattenAsync();
                await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
                await oldMessage.DeleteAsync();
                IMessage botMsg = await ReplyAsync("Messages deleted!");
                IMessage botMsg2 = await ReplyAsync("<:remV:639621688887083018>");
                await Task.Delay(2500);
                await botMsg.DeleteAsync();
                await botMsg2.DeleteAsync();
                return;
            }
            else
            {
                await ReplyAsync($"You aren't allowed to user purge, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Group("channel")]
        public class ChannelModule : ModuleBase<SocketCommandContext>
        {
            [Command("changeName")]
            public async Task changeName(ITextChannel channel, string str)
            {
                var user = Context.User as SocketGuildUser;
                var idUser = Context.User.Id;
                if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
                {
                    var msg = Context.Message;
                    await channel.ModifyAsync(x =>
                    {
                        x.Name = str;
                    });
                    var botMsg = await ReplyAsync($"Name of **<#{channel.Id}>** successfully changed to **{str}**!");
                    await Task.Delay(2500);
                    await botMsg.DeleteAsync();
                    await msg.DeleteAsync();
                    return;
                }
                else
                {
                    await ReplyAsync("Only my masters can do that!");
                    await ReplyAsync("<:hmpfREM:476840909334511677>");
                    return;
                }
            }

            [Command("changeDesc")]
            public async Task ChangeDesc(ITextChannel channel, string str)
            {
                var user = Context.User as SocketGuildUser;
                var idUser = Context.User.Id;
                if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
                {
                    IMessage msg = Context.Message;
                    await msg.DeleteAsync();
                    await channel.ModifyAsync(x =>
                    {
                        x.Topic = str;
                    });
                    IMessage botMsg = await ReplyAsync($"**Description of** <#{channel.Id}> **successfully changed to** \"{str}\"**!**");
                    IMessage botMsg2 = await ReplyAsync("<:remV:639621688887083018>");
                    await Task.Delay(2500);
                    await botMsg.DeleteAsync();
                    await botMsg2.DeleteAsync();
                    return;
                }
                else
                {
                    await ReplyAsync("Only my masters can do that!");
                    await ReplyAsync("<:hmpfREM:476840909334511677>");
                    return;
                }
            }

            //Needs some bugfixing, means it doesn't work probably 
            [Command("changePos")]
            public async Task ChangePosition(IGuildChannel channel,int changePos)
            {
                var user = Context.User as SocketGuildUser;
                var idUser = Context.User.Id;
                if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
                {
                    IMessage msg = Context.Message;
                    await channel.ModifyAsync(x =>
                    {
                        x.Position = channel.Position + changePos + 1;
                    });
                    IMessage botMsg = await ReplyAsync($"Successfully moved channel by {changePos}");
                    IMessage botMsg2 = await ReplyAsync("<:remV:639621688887083018>");
                    await Task.Delay(2500);
                    await botMsg.DeleteAsync();
                    await botMsg2.DeleteAsync();
                    await msg.DeleteAsync();
                    return;
                }
                else
                {
                    await ReplyAsync("Only my masters can do that!");
                    await ReplyAsync("<:hmpfREM:476840909334511677>");
                    return;
                }
            }
        }

        [Command("delMessage")]
        public async Task DelMessage(ulong id)
        {
            IMessage userMessage = Context.Message;
            IMessage message = await Context.Channel.GetMessageAsync(id);
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                IMessage botmsg1 = await ReplyAsync("\"" + message.Content + "\"" + " **by** " + $"<@{message.Author.Id}>" + " **gets deleted!**");
                IMessage botmsg2 = await ReplyAsync("<:remV:639621688887083018>");
                await message.DeleteAsync();
                await Task.Delay(4000);
                IMessage botmsg3 = await ReplyAsync("Successful!");
                await botmsg1.DeleteAsync();
                await botmsg2.DeleteAsync();
                await Task.Delay(2000);
                await botmsg3.DeleteAsync();
                await userMessage.DeleteAsync();
                return;
            }
            else
            {
                await ReplyAsync("Only my masters can do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

        [Command("acitivity")]
        public async Task Status([Remainder]string echo)
        {
            var user = Context.User as SocketGuildUser;
            var idUser = Context.User.Id;
            if (user.GuildPermissions.Administrator || idUser == o1 || idUser == o2)
            {
                string[] split = echo.Split(" ");
                int length = split.Length;
                echo = "";
                if (split[0].Equals("streaming"))
                {
                    for (int i = 2; i <= length - 1; i++)
                    {
                        echo = echo + " " + split[i];
                    }

                    await Program._client.SetGameAsync(echo, split[1], ActivityType.Streaming);
                    await ReplyAsync("Activity was set to: Streaming" + "**" + echo + "**" + " with the url: " + split[1]);
                    await ReplyAsync("<a:remspin:643170585668747298>");
                    return;
                }
                else
                {
                    for (int i = 1; i <= length - 1; i++)
                    {
                        echo = echo + " " + split[i];
                    }
                    switch (split[0])
                    {
                        case "listening":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Listening);
                            await ReplyAsync("Activity was set to: Listening to" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        case "playing":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Playing);
                            await ReplyAsync("Activity was set to: Playing" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        case "watching":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Watching);
                            await ReplyAsync("Activity was set to: Watching" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        default:
                            await ReplyAsync("listening/playing/watching + activity");
                            await ReplyAsync("<:hmpfREM:476840909334511677>");
                            return;
                    }
                }
            }
            else
            {
                await ReplyAsync("Only my masters can do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
                return;
            }
        }

    }
}
