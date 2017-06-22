using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {
    [RequireContext(ContextType.Guild)]
    public class Admin : ModuleBase {

        [Command("kick")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Remarks("Kicks all mentioned users")]
        public async Task Kick(IGuildUser[] users) {
            foreach (var u in users) await u.KickAsync();
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [Remarks("Bans the mentioned users")]
        public async Task Ban(IGuildUser[] users) {
            foreach (var u in users) await Context.Guild.AddBanAsync(u);
        }
    }
}
