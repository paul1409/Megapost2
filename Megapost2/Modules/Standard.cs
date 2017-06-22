using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Megapost2.Modules {
    public class Standard : ModuleBase {
        [Command("invite")]
        [Summary("Returns the OAuth2 Invite URL of the bot")]
        public async Task Invite() {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync(
                $"A user with `MANAGE_SERVER` can invite me to your server here: <http://nazr.in/ZzS>");

            //backup https://discordapp.com/oauth2/authorize?&client_id=269304726892314624&scope=bot&permissions=0xFFFFFFFFFFFF
        }

        [Command("leave")]
        [Summary("Instructs the bot to leave this Guild.")]
        [RequireUserPermission(GuildPermission.ManageGuild)]
        public async Task Leave() {
            if (Context.Guild == null) { await ReplyAsync("This command can only be run in a server."); return; }
            await ReplyAsync("Leaving");
            await Context.Guild.LeaveAsync();
        }

        [Command("say")]
        [Alias("echo")]
        [Summary("Echos the provided input")]
        public async Task Say([Remainder] string input) {
            await ReplyAsync(input);
        }

        [Command("info")]
        public async Task Info() {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync(
                $"{Format.Bold("Info")}\n" +
                $"- Author: {application.Owner.Username} (ID {application.Owner.Id})\n" +
                $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                $"- Uptime: {GetUptime()}\n\n" +

                $"{Format.Bold("Stats")}\n" +
                $"- Heap Size: {GetHeapSize()} MB\n" +
                $"- Guilds: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}" +
                $"- Users: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}"
            );
        }

        private static string GetUptime()
            => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
    }
}
