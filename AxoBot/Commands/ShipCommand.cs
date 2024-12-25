﻿using AxoBot.Core;
using Discord;
using Discord.WebSocket;
using System.Drawing;
using static AxoBot.Commands.CommandResources;

namespace AxoBot.Commands {
    public class ShipCommand : BaseCommand, ISlashCommand {
        public override string Name => "Ship";
        public override string Description => "Provides a relationship success ratio.";
        public override string Category => "Fun";


        public const string EmbedTitleUser1Id = "[\\1]";
        public const string EmbedTitleUser2Id = "[\\2]";

        public SlashCommandProperties RegisterAsSlash() => GetDefaultSlashCommandBuilder()
            .AddOption("user", ApplicationCommandOptionType.User, "The user you want to ship with.", isRequired: true)
            .Build();
        public async Task ExecuteFromSlash(SocketSlashCommand arg) {
            var guildUser1 = (SocketGuildUser)arg.User;
            var guildUser2 = (SocketGuildUser)arg.Data.Options.First().Value;

            await arg.RespondAsync(embed: GetInfoEmbed($"Relationship success ratio {EmbedTitleUser1Id} with {EmbedTitleUser2Id}:".Replace(EmbedTitleUser1Id, guildUser1.DisplayName).Replace(EmbedTitleUser1Id, guildUser2.DisplayName)).WithDescription($"{new Random().Next(0, 100)} %").WithImageUrl(await MergeAvatars(arg.User, guildUser2)).WithFooter("Work in Progress...").Build(), ephemeral: true);
        }

        private readonly HttpClient _httpClient = new HttpClient();

        private async Task<string> MergeAvatars(SocketUser user1, SocketUser user2) {
            var avatar1 = await DownloadImage(user1.GetAvatarUrl(ImageFormat.Png));
            var avatar2 = await DownloadImage(user2.GetAvatarUrl(ImageFormat.Png));

            var mergedImage = await MergeImages(avatar1, avatar2);

            using (var memoryStream = new MemoryStream()) {
                mergedImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var message = await (await CommandProvider.Client.GetChannelAsync(1319455186430984192) as ITextChannel).SendFileAsync(memoryStream, "merged_avatar.png");
                var attachments = message.Attachments.ToArray();
                return attachments[0].Url;
            }
        }
        private async Task<Bitmap> DownloadImage(string url) {
            using (var response = await _httpClient.GetAsync(url)) {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return new Bitmap(stream);
            }
        }
        private async Task<Bitmap> MergeImages(Bitmap img1, Bitmap img2) {
            int width = img1.Width + img2.Width;
            int height = Math.Max(img1.Height, img2.Height);
            var mergedImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(mergedImage)) {
                g.Clear(System.Drawing.Color.DarkGray);

                g.DrawImage(img1, new Point(0, 0));
                g.DrawImage(img2, new Point(width - img2.Width, 0));
                g.DrawString("❤", new Font("Calibri", 50), Brushes.HotPink, width / 2 - 47, height / 2 - 30);
            }
            return mergedImage;
        }
    }
}
