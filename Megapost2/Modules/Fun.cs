using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Google.Apis.Services;
using Google.Apis.Customsearch.v1;

namespace Megapost2.Modules {

    public class Fun : ModuleBase<SocketCommandContext> {

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
            await Context.Channel.SendMessageAsync("Showing top 10 Google results for **" +  s + "**\n"+ result);
        }
    }
}
