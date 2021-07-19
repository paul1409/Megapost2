using Discord.Commands;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megapost2.Modules {
    [Group("imgur")]
    [Alias("i")]
    public class Imgur : ModuleBase {

        [Command("login")]
        [Remarks("Attempts to login")]
        public async Task Login(string id, string secret) {
            try {
                ApiClient client = new ApiClient(id, secret);
                ImageEndpoint endpoint = new ImageEndpoint(client, new HttpClient());
                ImgurData.Endpoint = endpoint;
                await ReplyAsync("Successfully logged in");
            } catch (Exception e) {
                await ReplyAsync("Failed to create imgur endpoint: " + e.Message);
            }
        }

        [Command("logout")]
        [Remarks("Logs out the endpoint client")]
        public async Task Logout() {
            ImgurData.Endpoint = null;
            await ReplyAsync("Successfully logged out");
        }

        [Command("upload")]
        [Alias("u")]
        [Remarks("Uploads an image via URL")]
        public async Task Upload(string url) {
            try {
                IImage image = await ImgurData.Endpoint.UploadImageAsync(url);
                await ReplyAsync("Successfully uploaded image: " + image.Link);
            } catch (Exception e) {
                await ReplyAsync("Failed to upload image: " + e.Message);
            }
        }

    }
}
