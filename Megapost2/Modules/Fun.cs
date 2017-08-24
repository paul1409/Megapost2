using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Google.Apis.Services;
using Google.Apis.Customsearch.v1;
using System.Text.RegularExpressions;

namespace Megapost2.Modules {

    public class Fun : ModuleBase<SocketCommandContext> {

        Random r = new Random();

        [Command("google")]
        [Remarks("Google searches the input")]
        public async Task google(string s) {
            string apiKey = "AIzaSyAi4_XbS4euGFf7LJYH9jLdERF92PRELE0";
            string cx = "006365420480420697386:a5kksrll-pc";
            var search = new CustomsearchService(new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = search.Cse.List(s);
            listRequest.Cx = cx;
            var results = listRequest.Execute();
            string result = "";
            foreach (var res in results.Items) {
                string title = string.Format("`{0}`", res.Title);
                string link = string.Format("<{0}>", res.Link);
                result += (title + " " + link + "\n");
            }
            await ReplyAsync($"Showing top 10 Google results for **{s}**\n" + result);
        }

        [Command("rtd")]
        [Remarks("Simulates a dice roll")]
        public async Task rtd(string s) {
            string[] str = s.Split('d');
            int i = int.Parse(str[0]);
            int j = int.Parse(str[1]);
            if (i > 5000 || j > 5000) await Context.Channel.SendMessageAsync("Enter something under 5000 you dumb fucks");
            if (i < 0 || j < 0) await Context.Channel.SendMessageAsync("Are you that retarded?");
            else await ReplyAsync("You rolled a `" + j + "` die" + " " + i + " times for a total of: " + rtd(i, j) + "");
        }

        [Command("choose")]
        [Remarks("Chooses between the given items")]
        public async Task choose(params string[] choices) {
            if (choices.Length <= 0) await Context.Channel.SendMessageAsync("Nothing to choose from");
            else await ReplyAsync("I choose: " + choices[r.Next(choices.Length)]);
        }

        [Command("8ball")]
        [Remarks("An 8ball thing, test your fortune or whatever")]
        public async Task eightBall([Remainder]string s) {
            string[] str = {"It is certain", "It is decidedly so", "Without a doubt", "Yes definitely", "You may rely on it", "As I see it, yes", "Most likely",
                "Outlook good", "Yes", "Signs point to yes", "Reply hazy try again", "Ask again later", "Better not tell you now", "Cannot predict now",
                "Concentrate and ask again", "Don't count on it", "My reply is no", "My sources say no", "Outlook not so good", " Very doubtful" };
            await ReplyAsync(str[r.Next(str.Length)]);
        }

        public int rtd(int x, int j) {
            int total = 0;
            int n = 0;
            for (int i = 0; i < x; i++) {
                n = r.Next(j + 1);
                if (n != 0) total += n;
            }
            return total;
        }
    }
}
