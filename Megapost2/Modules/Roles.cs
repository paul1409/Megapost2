using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Globalization;

namespace Megapost2.Modules {

    [Group("role")]
    public class Roles : ModuleBase {

        [Command("add")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task add(string s, IGuildUser[] u) {
            var roles = Context.Guild.Roles;
            IRole role;
            foreach (var v in roles)
                if (v.ToString() == s) {
                    role = v;
                    foreach (IGuildUser usr in u) await usr.AddRoleAsync(role);
                } else await ReplyAsync("Role not found");
        }

        [Command("take")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task take(string s, IGuildUser[] u) {
            var roles = Context.Guild.Roles;
            IRole role;
            foreach (var v in roles)
                if (v.ToString() == s) {
                    role = v;
                    foreach (IGuildUser usr in u) await usr.RemoveRoleAsync(role);
                } else await ReplyAsync("Role not found");
        }

        [Command("create")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        public async Task create(string s) {
           await Context.Guild.CreateRoleAsync(s);
        }

    }
}
