﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Globalization;

namespace Megapost2.Modules {

    [Group("role")]
    [RequireUserPermission(GuildPermission.ManageRoles)]
    public class Roles : ModuleBase {

        [Command("add")]
        public async Task add(IGuildUser u, [Remainder] string s) {
            var roles = Context.Guild.Roles;
            foreach (var v in roles)
                if (v.Name == s) {
                    await u.AddRoleAsync(v);
                    await Context.Channel.SendMessageAsync("Role `" + s + "` has been added to " + u.Mention);
                }
        }

        [Command("take")]
        public async Task take(IGuildUser u, [Remainder] string s) {
            var roles = Context.Guild.Roles;
            foreach (var v in roles)
                if (v.Name == s) {
                    await u.RemoveRoleAsync(v);
                    await Context.Channel.SendMessageAsync("Role `" + s + "` has been taken from "+ u.Mention);
                }
        }

        [Command("create")]
        public async Task create([Remainder] string s) {
            await Context.Guild.CreateRoleAsync(s);
            await Context.Channel.SendMessageAsync("Role `" + s + "` has been created");
        }

    }
}