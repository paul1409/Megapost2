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
        public async Task Kick(IGuildUser u) {
            await u.KickAsync();
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [Remarks("Bans the mentioned users")]
        public async Task Ban(IGuildUser u) {
            await Context.Guild.AddBanAsync(u);
        }

        [Command("purge")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Remarks("Purges users who haven't been online in specified number of days")]
        public async Task Purge(int i) {
            await Context.Guild.PruneUsersAsync(i);
        }
    }
}
