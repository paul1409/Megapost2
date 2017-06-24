using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.IO;
using Discord;

namespace Megapost2 {
    public class CommandHandler {
        
        private DiscordSocketClient client;
        private CommandService service;

        public async Task Initialize(DiscordSocketClient client) {
            this.client = client;
            service = new CommandService();
            await service.AddModulesAsync(Assembly.GetEntryAssembly());
            client.MessageReceived += HandleCommandAsync;
            client.UserJoined += Join;
            client.UserLeft += Leave;
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
            var welcome = (Path.Combine(Directory.GetCurrentDirectory(), "Messages\\welcome.txt"));
            var img = Path.Combine(Directory.GetCurrentDirectory(), "Newcomers.jpg");
            var channel = client.GetChannel(u.Guild.DefaultChannel.Id) as SocketTextChannel;
            await channel.SendFileAsync(img, string.Format(usrmsg(welcome), u.Mention));
            
        }

        private async Task Leave(SocketGuildUser u) {
            var leave = (Path.Combine(Directory.GetCurrentDirectory(), "Messages\\leave.txt"));
            var channel = client.GetChannel(u.Guild.DefaultChannel.Id) as SocketTextChannel;
            await channel.SendMessageAsync(string.Format(usrmsg(leave), u.ToString()));
        }

        private string usrmsg(string s) {
            var lines = File.ReadAllLines(s);
            Random r = new Random();
            return lines[r.Next(0, lines.Length)];
        }

    }
}
