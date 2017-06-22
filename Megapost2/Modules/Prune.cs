using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {

    [Group("prune")]
    public class Prune : ModuleBase {
        
        [Command]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task purge(int i) {
            if (i < 0) await ReplyAsync("Cannot delete a negative number of messages");
            else if (i > 99) await ReplyAsync("Too many to delete");
            else {
                Console.WriteLine("Deleting: " + i);
                var m = await Context.Channel.GetMessagesAsync().Flatten();
                await Context.Channel.DeleteMessagesAsync(m);
                Console.WriteLine(Context.User.ToString() + " deleted " + i + " at " + DateTime.Now.ToString(@"dd\:hh\:mm"));
            }
        }
    }
}
