using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;

namespace Megapost2 {
    public class CommandHandler {

        private DiscordSocketClient client;
        private CommandService service;

        public CommandHandler(DiscordSocketClient client) {
            this.client = client;
            service = new CommandService();
            service.AddModulesAsync(Assembly.GetEntryAssembly());
            client.MessageReceived += HandleCommandAsync;
            client.UserJoined += Join;
        }

        private async Task HandleCommandAsync(SocketMessage s) {
            var msg = s as SocketUserMessage;
            if (msg == null) return;
            var e = new SocketCommandContext(client, msg);
            int argPos = 0;
            if (msg.HasCharPrefix('`', ref argPos)) {
                var result = await service.ExecuteAsync(e, argPos);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) await e.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        private async Task Join(SocketGuildUser u) {
            var channel = client.GetChannel(u.Guild.DefaultChannel.Id) as SocketTextChannel;
            await channel.SendMessageAsync(u.Mention + " has joined the server");
            
        }
    }
}
