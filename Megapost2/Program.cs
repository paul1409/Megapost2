using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;

namespace Megapost2 {
    public class Program {

        static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandHandler handler;

        public async Task StartAsync() {
            client = new DiscordSocketClient();
            new CommandHandler();
            await client.LoginAsync(TokenType.Bot, "MzA2OTU4OTg1MTc1NjI5ODI0.DC2bHQ.X7UX6haHvjrM7kXqhbsSvBHYDZU");
            await client.StartAsync();
            handler = new CommandHandler();
            await handler.Initialize(client);
            await Task.Delay(-1);
        }
    }
}
