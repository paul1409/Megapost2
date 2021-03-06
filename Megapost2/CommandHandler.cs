﻿using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.IO;

namespace Megapost2 {
    public class CommandHandler {
        
        private DiscordSocketClient client;
        private CommandService service;
        private readonly string[] welcome = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Messages\\welcome.txt"));
        private readonly string[] leave = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Messages\\leave.txt"));
        Random r;

        public async Task Initialize(DiscordSocketClient client) {
            r = new Random();
            this.client = client;
            service = new CommandService();
            await service.AddModulesAsync(Assembly.GetEntryAssembly(), null);
            client.MessageReceived += HandleCommandAsync;
            client.UserJoined += Join;
            client.UserLeft += Leave;
        }

        private async Task HandleCommandAsync(SocketMessage s) {
            if (!(s is SocketUserMessage msg)) return;
            var e = new SocketCommandContext(client, msg);
            int argPos = 0;
            if (msg.HasCharPrefix('`', ref argPos)) {
                var result = await service.ExecuteAsync(e, argPos, null);
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand) await e.Channel.SendMessageAsync(result.ErrorReason);
            }
        }

        private async Task Join(SocketGuildUser u) {
            var img = Path.Combine(Directory.GetCurrentDirectory(), "Newcomers.jpg");
            var channel = client.GetChannel(u.Guild.DefaultChannel.Id) as SocketTextChannel;
            string msg = welcome[r.Next(0, welcome.Length)];
            await channel.SendFileAsync(img, (string.Format(msg, u.Mention)));
        }

        private async Task Leave(SocketGuildUser u) {
            var channel = client.GetChannel(u.Guild.DefaultChannel.Id) as SocketTextChannel;
            string msg = leave[r.Next(0, leave.Length)];
            await channel.SendMessageAsync(string.Format(msg, u.ToString()));
        }

        private string Usrmsg(string s) {
            var lines = File.ReadAllLines(s);
            Random r = new Random();
            return lines[r.Next(0, lines.Length)];
        }

    }
}
