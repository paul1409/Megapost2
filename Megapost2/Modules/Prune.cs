using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Megapost2.Modules {

    [Group("prune")]
    [RequireUserPermission(GuildPermission.ManageMessages)]
    public class Prune : ModuleBase<SocketCommandContext> {

        [Command]
        public async Task prune(int i) {
            Console.WriteLine("Deleting: " + i);
            await PruneMsg(i, m => true);
            Console.WriteLine($"{Context.User.ToString()} deleted {i} at {DateTime.Now.ToString(@"dd\:hh\:mm")}");
        }

        [Command("embed")]
        [Summary("Deletes all messages containing embeds/attachments in the last i messages")]
        public async Task embed(int i) => await PruneMsg(i, msg => msg.Embeds.Any() || msg.Attachments.Any());

        [Command("emoji")]
        [Summary("Deletes all messages containing emoji in the last i messages")]
        public async Task emoji(int i) => await PruneMsg(i, ms => ms.Tags.Any(t => t.Type == TagType.Emoji));

        [Command("mine")]
        [Summary("Deletes all messages sent by the user in the last i messages")]
        public async Task mine(int i) => await PruneMsg(i, msg => msg.Author.Id == Context.User.Id);

        [Command("ping")]
        [Summary("Deletes all mentions in the last i messages")]
        public async Task ping(int i) => await PruneMsg(i, msg => msg.MentionedUserIds.Any() || msg.MentionedRoleIds.Any());

        [Command("bot")]
        [Summary("Deletes all messages sent by bots in the last i messages")]
        public async Task bot(int i) => await PruneMsg(i, m => m.Author.IsBot);

        [Command("user")]
        [Summary("Deletes all messages sent by a specified user in the last i messages")]
        public async Task user(IGuildUser u, int i) => await PruneMsg(i, m => m.Author.Id == u.Id);

        [Command("reactions")]
        public async Task react(int i) {
            var cmd = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
            await Context.Channel.DeleteMessageAsync(cmd.FirstOrDefault());
            var msgs = await Context.Channel.GetMessagesAsync(i).FlattenAsync();
            foreach (IUserMessage m in msgs) await m.RemoveAllReactionsAsync();
        }

        async Task PruneMsg(int i, Func<IMessage, bool> pred = null) {
            if (i < 0) {
                await ReplyAsync("Cannot delete a negative number of messages");
                return;
            } else if (i > 99) {
                await ReplyAsync("Too many to delete");
                return;
            } else {
                var cmd = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
                await Context.Channel.DeleteMessageAsync(cmd.FirstOrDefault());
                var m = await Context.Channel.GetMessagesAsync(i).FlattenAsync();
                if (pred != null) m = m.Where(pred);
                foreach (var msg in m) await Context.Channel.DeleteMessageAsync(msg);
            }
        }

    }
}
