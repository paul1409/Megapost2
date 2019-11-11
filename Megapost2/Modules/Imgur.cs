using Discord.Commands;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using System;
using System.Threading.Tasks;

namespace Megapost2.Modules {
    [Group("imgur")]
    public class Imgur : ModuleBase {

        [Command("login")]
        [Remarks("Attempts to login")]
        public async Task login(string id, string secret) {
            try {
                ImgurData.Endpoint = new ImageEndpoint(new ImgurClient(id, secret));
                await ReplyAsync("Successfully logged in");
            } catch (Exception e) {
                await ReplyAsync("Failed to create imgur endpoint: " + e.ToString());
            }
        }

        [Command("logout")]
        [Remarks("Attempts to search for an image")]
        public async Task logout() {
            ImgurData.Endpoint = null;
            await ReplyAsync("Successfully logged out");
        }

        [Command("search")]
        [Remarks("Attempts to search for an image")]
        public async Task search() {

        }

        [Command("upload")]
        [Remarks("Uploads an image via URL")]
        public async Task upload(string url) {
            try {
                IImage image = ImgurData.Endpoint.UploadImageUrlAsync(url).GetAwaiter().GetResult();
                await ReplyAsync("Successfully uploaded image: " + image.Link);
            } catch (Exception e) {
                await ReplyAsync("Failed to upload image: " + e.ToString());
            }
        }

    }
}
