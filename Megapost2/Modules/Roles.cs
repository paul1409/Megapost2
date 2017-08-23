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
    [RequireUserPermission(GuildPermission.ManageRoles)]
    public class Roles : ModuleBase {

        [Command("add")]
        public async Task add(IGuildUser u, string s) {
            var roles = Context.Guild.Roles;
            foreach (var v in roles)
                if (v.Name == s) {
                    await u.AddRoleAsync(v);
                    await Context.Channel.SendMessageAsync($"Role `{s}` has been added to " + u.Mention);
                }
        }

        [Command("take")]
        public async Task take(IGuildUser u, IRole r) {
            var roles = Context.Guild.Roles;
            if (roles.Contains(r)) {
                await u.RemoveRoleAsync(r);
                await Context.Channel.SendMessageAsync("Role `" + r.ToString() + "` has been taken from " + u.Mention);
            } else await ReplyAsync($"Role {r} could not be found.");

        }

        [Command("create")]
        public async Task create(string s) {
            await Context.Guild.CreateRoleAsync(s);
            await Context.Channel.SendMessageAsync($"Role `{s}` has been created");
        }

        [Command("destroy")]
        public async Task destroy(IRole r) {
            await Context.Channel.SendMessageAsync(r.ToString() + " has been deleted");
            await r.DeleteAsync();
        }

        [Command("color")]
        public async Task color(IRole r, string color) {
            uint colorVal;
            if (!TryParseColor(color, out colorVal)) {
                await Context.Channel.SendMessageAsync($"Could not parse {color} to a proper color value");
            }
            await r.ModifyAsync(role => { role.Color = new Optional<Color>(new Color(colorVal)); });
        }

        [Command("rename")]
        public async Task rename(IRole r, string name) {
            await r.ModifyAsync(role => { role.Name = name; });
        }

        bool TryParseColor(string color, out uint val) {
            return uint.TryParse(color, NumberStyles.HexNumber, null, out val);
        }
    }
}
