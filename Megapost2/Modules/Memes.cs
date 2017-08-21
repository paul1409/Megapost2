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

    public class Memes : ModuleBase<SocketCommandContext> {

        string dir = Path.Combine(Directory.GetCurrentDirectory(), "memes.txt");
        static cmdlist clist = new cmdlist();
        List<Commands> cmds = clist.reader();
        string last;

        public string getCommand(string s) {
            if (clist.exists(s)) return s;
            else return null;
        }

        [Command("meme")]
        [Remarks("Attempts to get command")]
        public async Task meme([Remainder] string s) {
            if (clist.exists(s))
                foreach (Commands c in cmds) {
                    if (c.name == s) {
                        await Context.Channel.SendMessageAsync(c.URL);
                        return;
                    }
                }
            else if (clist.multiExists(s)) await Context.Channel.SendMessageAsync(clist.contained(s));
            else {
                string[] str = s.Split(null);
                    foreach (Commands c in cmds) {
                        if (c.name == str[0]) {
                            await Context.Channel.SendMessageAsync(str[1]+ " "+ c.URL);
                            return;
                    }
                }
            }
        }

        [Command("new")]
        [Remarks("Adds a new command")]
        public async Task NewMeme([Remainder] string s) {
            try {
                string[] str = s.Split(null);
                if (str.Length == 2) {
                    clist.add(new Commands(str[0], str[1]));
                    last = str[0];
                    await Context.Channel.SendMessageAsync(":ok_hand:");
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        [Command("undo")]
        [Remarks("Undo recently added command")]
        public async Task Undo() {
            if (last != null) {
                clist.undo(last);
                await Context.Channel.SendMessageAsync(":ok_hand:");
                last = null;
            }
        }

    }
}
