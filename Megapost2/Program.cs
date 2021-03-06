﻿using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using System.IO;

namespace Megapost2 {
    public class Program {
        readonly StreamReader token = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "token.txt"));
        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task StartAsync() {
            client = new DiscordSocketClient();
            try {
                await client.LoginAsync(TokenType.Bot, token.ReadLine());
            } catch (Exception e) {
                Console.WriteLine("Token missing: " + e.Message);
            }
            await client.StartAsync();
            handler = new CommandHandler();
            await handler.Initialize(client);
            await Task.Delay(-1);
        }
    }
}