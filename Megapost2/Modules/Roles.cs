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
        [Remarks("Adds a role to the provided users")]
        public async Task add(IRole r, params IGuildUser[] usr) {
            var roles = Context.Guild.Roles;
            if (roles.Contains(r)) {
                foreach (IGuildUser u in usr) {
                    await u.AddRoleAsync(r);
                    await Context.Channel.SendMessageAsync($"Role `{r.ToString()}` has been added to " + u.Mention);
                }
            }
        }

        [Command("take")]
        [Remarks("Takes a role from the provided users")]
        public async Task take(IRole r, params IGuildUser[] usr) {
            var roles = Context.Guild.Roles;
            if (roles.Contains(r)) {
                foreach (IGuildUser u in usr) {
                    await u.RemoveRoleAsync(r);
                    await Context.Channel.SendMessageAsync($"Role `{r.ToString()}` has been taken from " + u.Mention);
                }
            } else await ReplyAsync($"Role {r} could not be found.");

        }

        [Command("create")]
        [Remarks("Creates a new role")]
        public async Task create(string s) {
            await Context.Guild.CreateRoleAsync(s);
            await Context.Channel.SendMessageAsync($"Role `{s}` has been created");
        }

        [Command("destroy")]
        [Remarks("Destroy the provided role")]
        public async Task destroy(IRole r) {
            if (Context.Guild.Roles.Contains(r)) {
                await r.DeleteAsync();
                await ReplyAsync($"{r} has been deleted");
            } else await ReplyAsync("Role does not exist");
        }

        [Command("color")]
        [Remarks("Changes the color of a role")]
        public async Task color(IRole r, string color) {
            uint colorVal;
            if (!TryParseColor(color, out colorVal))
                await Context.Channel.SendMessageAsync($"Could not parse {color} to a proper color value");
            else {
                await r.ModifyAsync(role => { role.Color = new Optional<Color>(new Color(colorVal)); });
                await ReplyAsync(":thumbsup:");
            }
        }

        [Command("rename")]
        [Remarks("Renames a role")]
        public async Task rename(IRole r, string name) {
            if (Context.Guild.Roles.Contains(r)) {
                await r.ModifyAsync(role => { role.Name = name; });
                await ReplyAsync(":thumbsup:");
            } else await ReplyAsync("Role does not exist");
        }

        [Command("has")]
        [Remarks("Gets the user's list of roles")]
        public async Task get(IRole r) {
            List<string> u = new List<string>();
            foreach (IGuildUser user in await Context.Guild.GetUsersAsync())
                if (user.RoleIds.Contains(r.Id)) u.Add(user.Mention);
            await ReplyAsync($"Users that contain role {r}: {string.Join(" , ", u)}");
        }

        bool TryParseColor(string color, out uint val) {
            return uint.TryParse(color, NumberStyles.HexNumber, null, out val);
        }
    }
}
