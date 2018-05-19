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
        [Remarks("Joins a voice channel")]
        [RequireContext(ContextType.Guild)]
        public async Task join() {

        }
    }
}
