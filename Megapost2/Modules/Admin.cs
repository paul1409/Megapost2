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
        public async Task Kick(params IGuildUser[] u) {
            foreach (var usr in u) await usr.KickAsync();
        }

        [Command("ban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [Remarks("Bans the mentioned users")]
        public async Task Ban(params IUser[] usr) {
            foreach (var u in usr) {
                await Context.Guild.AddBanAsync(u);
                await ReplyAsync($"{u} has been banned");
            }
        }

        [Command("softban")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [Remarks("Softbans members and prunes their messages in days")]
        public async Task Softban(int days = 7, params IGuildUser[] usr) {
            foreach (var u in usr) {
                ulong id = u.Id;
                await Context.Guild.AddBanAsync(u, days);
                await u.Guild.RemoveBanAsync(id);
            }
        }

        [Command("purge")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [Remarks("Purges users who haven't been online in specified number of days")]
        public async Task Purge(int i) => await Context.Guild.PruneUsersAsync(i);

        [Command("nickname")]
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        [Remarks("Allows a mod to change any user's nickname")]
        public async Task nickname(IGuildUser u, string name) =>
            await u.ModifyAsync(x => { x.Nickname = name; });

        [Command("move")]
        [RequireUserPermission(GuildPermission.MoveMembers)]
        [Remarks("Move all user from the src channel to the dst channel")]
        public async Task Move(IVoiceChannel src, IVoiceChannel dst) {
            var k = await src.GetUsersAsync().Flatten();
            foreach (IGuildUser u in k) {
                await u.ModifyAsync(x => { x.Channel = new Optional<IVoiceChannel>(dst); });
                await ReplyAsync(u.Username + ": :ok_hand:");
            }
        }

        [Command("mute")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        [Remarks("Mutes a member")]
        public async Task Mute(params IGuildUser[] usr) {
            foreach (IGuildUser u in usr) await u.ModifyAsync(user => user.Mute = true);
        }

        [Command("unmute")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        [Remarks("Unmutes a member")]
        public async Task Unmute(params IGuildUser[] usr) {
            foreach (IGuildUser u in usr) await u.ModifyAsync(user => user.Mute = false);
        }

        [Command("deafen")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        [Remarks("Deafens a member")]
        public async Task Deafen(params IGuildUser[] usr) {
            foreach (IGuildUser u in usr) await u.ModifyAsync(user => user.Deaf = true);
        }

        [Command("undeafen")]
        [RequireUserPermission(GuildPermission.MuteMembers)]
        [Remarks("Undefeans a member")]
        public async Task Undeafen(params IGuildUser[] usr) {
            foreach (IGuildUser u in usr) await u.ModifyAsync(user => user.Deaf = false);
        }
    }
}
