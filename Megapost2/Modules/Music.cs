using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Megapost2.Modules {
    public class Music : ModuleBase<SocketCommandContext> {

        [Command("join")]
        [Remarks("Joins the voice channel of the one who called the command")]
        [RequireContext(ContextType.Guild)]
        public async Task join() => await (Context.User as IVoiceState).VoiceChannel.ConnectAsync();

        [Command("drop")]
        [Remarks("Disconnects from the voice channel")]
        [RequireContext(ContextType.Guild)]
        public async Task drop() => await (Context.User as IVoiceState).VoiceChannel.ConnectAsync();
    }
}
