using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {
    
    [RequireOwner]

    public class Owner : ModuleBase<SocketCommandContext> {
        
        [Command("rename")]
        [Remarks("Renames the bot")]
        public async Task Rename(string name) {
            await Context.Client.CurrentUser.ModifyAsync(u => u.Username = name);
        }
    }
}
