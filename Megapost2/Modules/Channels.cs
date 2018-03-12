using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {
    [Group("channel")]
    [RequireUserPermission(GuildPermission.ManageChannels)]
    public class Channels : ModuleBase<SocketCommandContext> {

        [Command("destroy")]
        [Alias("delete")]
        [Summary("Destroys a channel provided a name")]
        public async Task destroy(params IGuildChannel[] channels) {
            foreach (IGuildChannel c in channels) {
                string cname = c.Name;
                await c.DeleteAsync();
                await Context.Channel.SendMessageAsync($"`{cname}`: is no longer a channel");
            }
        }

        [Command("rename")]
        [Summary("Renames a channel")]
        public async Task rename(IGuildChannel c, string s) {
            await c.ModifyAsync(xc => { xc.Name = s; });
        }

        [Group("text")]
        public class TextChannel : ModuleBase<SocketCommandContext> {
            [Command("create")]
            [Summary("Creates a new channel with a given name")]
            public async Task create(string name) {
                await Context.Guild.CreateTextChannelAsync(name);
                await Context.Channel.SendMessageAsync($"`{name}`: is now a new channel");
            }

            [Command("topic")]
            [Remarks("sets a topic for this channel")]
            public async Task topic(ITextChannel channel, string s) {
                await channel.ModifyAsync(c => c.Topic = s);
                await ReplyAsync($"Channel {channel} has its topic changed");
            }
        }

        [Group("voice")]
        public class VoiceChannel : ModuleBase<SocketCommandContext> {
            [Command("create")]
            [Summary("Creates a new channel with a given name")]
            public async Task create(string name) {
                await Context.Guild.CreateVoiceChannelAsync(name);
                await Context.Channel.SendMessageAsync($"`{name}`: is now a new channel");
            }
        }

    }
}
