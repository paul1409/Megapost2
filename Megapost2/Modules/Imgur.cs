using Discord.Commands;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Megapost2.Modules
{
    [Group("imgur")]
    public class Imgur : ModuleBase {

        [Command("login")]
        [Remarks("Attempts to login")]
        public async Task Login(string id, string secret) {
            try {
                ApiClient client = new ApiClient(id, secret);
                ImageEndpoint endpoint = new ImageEndpoint(client, new HttpClient());
                await ReplyAsync("Successfully logged in");
            } catch (Exception e) {
                await ReplyAsync("Failed to create imgur endpoint: " + e.ToString());
            }
        }

        [Command("logout")]
        [Remarks("Attempts to search for an image")]
        public async Task Logout() {
            ImgurData.Endpoint = null;
            await ReplyAsync("Successfully logged out");
        }

        [Group("upload")]
        [Remarks("Uploads an image via URL")]
        public class Uploader : ModuleBase {
            [Command]
            public async Task Upload(string url) {
                try {
                    IImage image = await ImgurData.Endpoint.UploadImageAsync(url);
                    await ReplyAsync("Successfully uploaded image: " + image.Link);
                } catch (Exception e) {
                    await ReplyAsync("Failed to upload image: " + e.ToString());
                }
            }

            [Command("video")]
            public async Task UploadVideo(string videoUrl) {
                try {
                    IImage image = await ImgurData.Endpoint.UploadVideoAsync(GetStream(videoUrl));
                    await ReplyAsync("Successfully uploaded video: " + image.Link);
                } catch (Exception e) {
                    await ReplyAsync("Failed to upload image: " + e.ToString());
                }
            }
            protected Stream GetStream(string videoUrl) {
                HttpWebRequest aRequest = (HttpWebRequest) WebRequest.Create(videoUrl);
                HttpWebResponse aResponse = (HttpWebResponse) aRequest.GetResponse();
                return aResponse.GetResponseStream();
            }
        }

    }
}
