using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {
    [Group("channel")]
    public class Channels : ModuleBase<SocketCommandContext> {

        [Command("create")]
        [Summary("Creates a new channel with a given name")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task create(string name) {
            await Context.Guild.CreateTextChannelAsync(name);
            await Context.Channel.SendMessageAsync($"`{name}`: is now a new channel");
        }

        [Command("destroy")]
        [Alias("delete")]
        [Summary("Destroys a channel provided a name")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task destroy(IGuildChannel c) {
            await c.DeleteAsync();
            await Context.Channel.SendMessageAsync("`" + c.Name + "`: is no longer a channel");
        }

    }
}
