using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JikanDotNet;

namespace Akan.Module
{
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
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
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
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
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

            }
            else
            {
                await ReplyAsync($"You aren't allowed to user purge, <@{idUser}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
            }
        }

        [Group("Channel")]
        public class ChannelModule : ModuleBase<SocketCommandContext>
        {
            [Command("changeDesc")]
            public async Task ChangeDesc(ITextChannel channel, string str)
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

            }
            else
            {
                await ReplyAsync("Only my masters can do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
            }
        }

        [Command("status")]
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
                    await ReplyAsync("Status was set to: Streaming" + "**" + echo + "**" + " with the url: " + split[1]);
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
                            await ReplyAsync("Status was set to: Listening to" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        case "playing":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Playing);
                            await ReplyAsync("Status was set to: Playing" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        case "watching":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Watching);
                            await ReplyAsync("Status was set to: Watching" + "**" + echo + "**");
                            await ReplyAsync("<a:remspin:643170585668747298>");
                            return;
                        default:
                            await ReplyAsync("listening/playing/watching + status");
                            await ReplyAsync("<:hmpfREM:476840909334511677>");
                            return;
                    }
                }
            }
            else
            {
                await ReplyAsync("Only my masters can do that!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
            }
        }

    }
}
