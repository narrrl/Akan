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

        [Command("kick")]
        public async Task KickUser(SocketGuildUser userName)
        {
            var user = Context.User as SocketGuildUser;
            var id = Context.User.Id;
            if (user.GuildPermissions.Administrator)
            {
                await ReplyAsync($"Bai Bai <@{id}>!");
                await ReplyAsync("<:remV:639621688887083018>");
                await userName.KickAsync();
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{id}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
            }
        }

        [Command("ban")]
        public async Task BanAsync(SocketGuildUser userName)
        {
            var user = Context.User as SocketGuildUser;
            var id = Context.User.Id;
            if (user.GuildPermissions.Administrator)
            {
                await ReplyAsync($"You're banned <@{id}>!");
                await ReplyAsync("<:remV:639621688887083018>");
                await userName.BanAsync();
            }
            else
            {
                await ReplyAsync($"You aren't allowed to bully, <@{id}>!");
                await ReplyAsync("<:hmpfREM:476840909334511677>");
            }
        }

    }
}
