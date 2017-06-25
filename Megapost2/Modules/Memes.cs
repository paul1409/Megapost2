using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.IO;
using Discord.WebSocket;

namespace Megapost2.Modules {

    [Group("meme")]
    public class Memes : ModuleBase<SocketCommandContext> {

        string dir = Path.Combine(Directory.GetCurrentDirectory(), "memes.txt");
        static cmdlist clist = new cmdlist();
        List<Commands> cmds = clist.reader();
        string last;

        public string getCommand(string s) {
            if (clist.exists(s)) return s;
            else return null;
        }

        [Command]
        [Remarks("Attempts to get command")]
        public async Task meme(string s) {
            if (clist.exists(s))
                foreach (Commands c in cmds) {
                    if (c.name == s) {
                        await Context.Channel.SendMessageAsync(c.URL);
                        return;
                    }
                }
            if (clist.multiExists(s)) await Context.Channel.SendMessageAsync(clist.contained(s));
        }

        [Command("new")]
        [Remarks("Adds a new command")]
        public async Task NewMeme([Remainder] string s) {
            string[] str = s.Split(null);
            if (str.Length == 2) {
                clist.add(new Commands(str[0], str[1]));
                last = str[0];
                await Context.Channel.SendMessageAsync(":ok_hand:");
            }
        }

        [Command("undo")]
        [Remarks("Undo recently added command")]
        public async Task Undo() {
            if (last != null) {
                clist.undo(last);
                await Context.Channel.SendMessageAsync(":ok_hand:");
            }
        }

    }
}
