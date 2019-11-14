using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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

        [Command("status")]
        public async Task Status(string echo)
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
                    return;
                }
                else
                {
                    for (int i = 1; i <= length - 1; i++)
                    {
                        echo += split[i];
                    }
                    switch (split[0])
                    {
                        case "listening":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Listening);
                            return;
                        case "playing":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Playing);
                            return;
                        case "watching":
                            await Program._client.SetGameAsync(echo, null, ActivityType.Watching);
                            return;
                        default:
                            await ReplyAsync("listening/playing/watching + status");
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
