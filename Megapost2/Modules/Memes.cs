﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Commands;
using System.IO;

namespace Megapost2.Modules {

    public class Memes : ModuleBase<SocketCommandContext> {
        static readonly cmdlist clist = new cmdlist();
        readonly List<Commands> cmds = clist.reader();
        string last;

        public string getCommand(string s) {
            return clist.exists(s) ? s : null;
        }

        [Command("meme")]
        [Remarks("Attempts to get command")]
        public async Task Meme([Remainder] string s) {
            if (clist.exists(s))
                foreach (Commands c in cmds) {
                    if (c.name == s) {
                        await Context.Channel.SendMessageAsync(c.URL);
                        return;
                    }
                } else if (clist.multiExists(s)) await Context.Channel.SendMessageAsync(clist.contained(s));
            else {
                string[] str = s.Split(null);
                foreach (Commands c in cmds) {
                    if (c.name == str[0]) {
                        await Context.Channel.SendMessageAsync(str[1] + " " + c.URL);
                        return;
                    }
                }
            }
        }

        [Command("new")]
        [Remarks("Adds a new command")]
        public async Task NewMeme(string name, string link) {
            try {
                clist.add(new Commands(name, link));
                last = link;
                await Context.Channel.SendMessageAsync(":ok_hand:");
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

        [Command("remove")]
        [Remarks("Removes a meme")]
        public async Task Remove(string name) {
            clist.remove(name);
            await Context.Channel.SendMessageAsync(":ok_hand:");
        }

        [Group("multi")]
        public class Multi : ModuleBase<SocketCommandContext> {
            static readonly cmdlist clist = new cmdlist();

            [Command("add")]
            [Remarks("Adds a multi command")]
            public async Task add([Remainder] string s) {
                string[] str = s.Split(null);
                if (clist.multiExists(str[0]) && !clist.linkExists(str[0], str[1])) {
                    clist.addMulti(str[0], str[1]);
                    await Context.Channel.SendMessageAsync(":ok_hand:");
                } else await Context.Channel.SendMessageAsync("Command exists");
            }

            [Command("new")]
            [Remarks("Makes a new multi command")]
            public async Task addNew([Remainder] string s) {
                string[] str = s.Split(null);
                if (!clist.multiExists(str[0])) {
                    clist.createMulti(str[0], str[1]);
                    await Context.Channel.SendMessageAsync(":ok_hand:");
                } else await Context.Channel.SendMessageAsync("Command exists");
            }
        }
    }
}
