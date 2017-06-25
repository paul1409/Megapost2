using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megapost2.Modules {
    [Group("multi")]
    public class Multi : ModuleBase<SocketCommandContext> {

        string multidir = Path.Combine(Directory.GetCurrentDirectory(), "randmemes.txt");
        static cmdlist clist = new cmdlist();
        List<string> multicmds = clist.multiNames();

        [Command("add")]
        [Remarks("Adds a multi command")]
        public async Task add([Remainder] string s) {
            string[] str = s.Split(null);
            if (clist.multiExists(str[0]) && !clist.linkExists(str[0], str[1])) {
                clist.addMulti(str[0], str[1]);
                await Context.Channel.SendMessageAsync(":ok_hand");
            } else await Context.Channel.SendMessageAsync("Command exists");
        }

        [Command("new")]
        [Remarks("Makes a new multi command")]
        public async Task addNew([Remainder] string s) {
            string[] str = s.Split(null);
            if (!clist.multiExists(str[0])) {
                clist.createMulti(str[0], str[1]);
                await Context.Channel.SendMessageAsync(":ok_hand");
            } else await Context.Channel.SendMessageAsync("Command exists");
        }
    }
}
