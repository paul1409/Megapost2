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

        [Command("destroy")]
        [Alias("delete")]
        [Summary("Destroys a channel provided a name")]
        [RequireUserPermission(GuildPermission.ManageChannels)]
        public async Task destroy(IGuildChannel c) {
            string cname = c.Name;
            await c.DeleteAsync();
            await Context.Channel.SendMessageAsync($"`{cname}`: is no longer a channel");
        }

        [Group("text")]
        public class TextChannel : ModuleBase<SocketCommandContext> {
            [Command("create")]
            [Summary("Creates a new channel with a given name")]
            [RequireUserPermission(GuildPermission.ManageChannels)]
            public async Task create(string name) {
                await Context.Guild.CreateTextChannelAsync(name);
                await Context.Channel.SendMessageAsync($"`{name}`: is now a new channel");
            }
        }

        [Group("voice")]
        public class VoiceChannel : ModuleBase<SocketCommandContext> {
            [Command("create")]
            [Summary("Creates a new channel with a given name")]
            [RequireUserPermission(GuildPermission.ManageChannels)]
            public async Task create(string name) {
                await Context.Guild.CreateVoiceChannelAsync(name);
                await Context.Channel.SendMessageAsync($"`{name}`: is now a new channel");
            }
        }

    }
}
