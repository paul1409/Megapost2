using Discord.Commands;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Megapost2.Modules {
    [Group("imgur")]
    public class Imgur : ModuleBase {

        [Command("login")]
        [Remarks("Attempts to login")]
        public async Task login(string id, string secret) {
            try {
                ImgurClient client = new ImgurClient(id, secret);
                ImgurData.Endpoint = new ImageEndpoint(client);
                ImgurData.AlbumEndpoint = new AlbumEndpoint(client);
                await ReplyAsync("Successfully logged in");
            } catch (Exception e) {
                await ReplyAsync("Failed to create imgur endpoint: " + e.ToString());
            }
        }

        [Command("logout")]
        [Remarks("Attempts to search for an image")]
        public async Task logout() {
            ImgurData.Endpoint = null;
            ImgurData.AlbumEndpoint = null;
            await ReplyAsync("Successfully logged out");
        }

        [Group("upload")]
        [Remarks("Uploads an image via URL")]
        public class Uploader : ModuleBase {
            [Command]
            public async Task upload(string url) {
                try {
                    IImage image = await ImgurData.Endpoint.UploadImageUrlAsync(url);
                    await ReplyAsync("Successfully uploaded image: " + image.Link);
                } catch (Exception e) {
                    await ReplyAsync("Failed to upload image: " + e.ToString());
                }
            }
            //[Command]
            public async Task upload(params string[] urls) {
                try {
                    List<string> images = new List<string>();
                    foreach (string url in urls) {
                        Image image = (Image) await ImgurData.Endpoint.UploadImageUrlAsync(url);
                        images.Add(image.DeleteHash);
                    }
                    Album album = (Album)(await ImgurData.AlbumEndpoint.CreateAlbumAsync());
                    await ImgurData.AlbumEndpoint.AddAlbumImagesAsync(album.DeleteHash, urls);
                    await ReplyAsync("Successfully created album: " + album.Link);
                } catch (Exception e) {
                    await ReplyAsync("Failed to create album or error with image: " + e.ToString());
                }
            }
        }

    }
}
