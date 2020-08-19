using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {
    public class Music : ModuleBase<SocketCommandContext> {

        [Command("join")]
        [Remarks("Joins the voice channel of the one who called the command")]
        [RequireContext(ContextType.Guild)]
        public async Task Join() => await (Context.User as IVoiceState).VoiceChannel.ConnectAsync();

        [Command("drop")]
        [Remarks("Disconnects from the voice channel")]
        [RequireContext(ContextType.Guild)]
        public async Task Drop() => await (Context.User as IVoiceState).VoiceChannel.DisconnectAsync();
    }
}
