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
        public async Task google([Remainder] string s) {
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
            await Context.Channel.SendMessageAsync("Showing top 10 Google results for **" + s + "**\n" + result);
        }

        [Command("rtd")]
        [Remarks("Simulates a dice roll")]
        public async Task rtd(string s) {
            string[] str = s.Split('d');
            int i = int.Parse(str[0]);
            int j = int.Parse(str[1]);
            if (i > 5000 || j > 5000) await Context.Channel.SendMessageAsync("Enter something under 5000 you dumb fucks");
            if (i < 0 || j < 0) await Context.Channel.SendMessageAsync("Are you that retarded?");
            else await Context.Channel.SendMessageAsync("You rolled a `" + j + "` die" + " " + i + " times for a total of: " + rtd(i, j) + "");
        }

        [Command("choose")]
        [Remarks("Chooses between the given items")]
        public async Task choose(params string[] choices) {
            if (choices.Length <= 0) await Context.Channel.SendMessageAsync("Nothing to choose from");
            else await Context.Channel.SendMessageAsync("I choose: " + choices[r.Next(choices.Length)]);
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
