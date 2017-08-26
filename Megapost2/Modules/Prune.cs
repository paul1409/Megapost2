﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {

    [Group("prune")]
    [RequireUserPermission(GuildPermission.ManageMessages)]
    public class Prune : ModuleBase<SocketCommandContext> {

        [Command]
        public async Task prune(int i) {
            if (i < 0) await ReplyAsync("Cannot delete a negative number of messages");
            else if (i > 99) await ReplyAsync("Too many to delete");
            else {
                Console.WriteLine("Deleting: " + i);
                var m = await Context.Channel.GetMessagesAsync(i + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(m);
                Console.WriteLine(Context.User.ToString() + " deleted " + i + " at " + DateTime.Now.ToString(@"dd\:hh\:mm"));
            }
        }

        [Command("embed")]
        public async Task embed(int i) {
            if (i < 0) await ReplyAsync("Cannot delete a negative number of messages");
            else if (i > 99) await ReplyAsync("Too many to delete");
            else {
                var m = await Context.Channel.GetMessagesAsync(i + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(m.Where(msg => msg.Embeds.Any() || msg.Attachments.Any()));
            }
        }

        [Command("emoji")]
        public async Task emoji(int i) {
            if (i < 0) await ReplyAsync("Cannot delete a negative number of messages");
            else if (i > 99) await ReplyAsync("Too many to delete");
            else {
                var m = await Context.Channel.GetMessagesAsync(i + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(m.Where(ms => ms.Tags.Any(t => t.Type == TagType.Emoji)));
            }
        }

        [Command("mine")]
        public async Task mine(int i) {
            if (i < 0) await ReplyAsync("Cannot delete a negative number of messages");
            else if (i > 99) await ReplyAsync("Too many to delete");
            else {
                var m = await Context.Channel.GetMessagesAsync(i + 1).Flatten();
                await Context.Channel.DeleteMessagesAsync(m.Where(msg => msg.Author.Id == Context.User.Id));
            }
        }

        [Command("bot")]
        public async Task bot(int i) {
            if (i < 0) await ReplyAsync("Cannot delete a negative number of messages");
            else if (i > 99) await ReplyAsync("Too many to delete");
            else {
                var m = await Context.Channel.GetMessagesAsync(i + 1).Flatten();
                var cmd = await Context.Channel.GetMessagesAsync(1).Flatten();
                await Context.Channel.DeleteMessagesAsync(cmd);
                await Context.Channel.DeleteMessagesAsync(m.Where(msg => msg.Author.IsBot));
            }
        }

    }
}
