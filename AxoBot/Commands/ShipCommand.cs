using AxoBot.Core;
using Discord;
using Discord.WebSocket;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using SkiaSharp;

namespace AxoBot.Commands {
    public class ShipCommand : BaseCommand, ISlashCommand {
        public override string Name => "Ship";
        public override string Description => "Provides a relationship success ratio.";
        public override string Category => "Fun";

        public string EmbedTitle => "Relationship success ratio {0} with {1}:";
        public string EmbedDescription => "{0}%";

        public SKPoint User1Position => new SKPoint(16, 11);
        public SKPoint User2Position => new SKPoint(160, 11);
        public string ImagePath => "ship.png";

        public SlashCommandProperties RegisterAsSlash() => GetDefaultSlashCommandBuilder()
            .AddOption("user", ApplicationCommandOptionType.User, "The user you want to ship with.", isRequired: true)
            .Build();
        public async Task ExecuteFromSlash(SocketSlashCommand arg) {
            var guildUser1 = (SocketGuildUser)arg.User;
            var guildUser2 = (SocketGuildUser)arg.Data.Options.First().Value;

            var user1 = guildUser1.DisplayName;
            var user2 = guildUser2.DisplayName;

            await arg.RespondAsync(embed: GetInfoEmbed(
                string.Format(EmbedTitle, guildUser1.DisplayName, guildUser2.DisplayName),
                string.Format(EmbedDescription, GetShipRatio(user1, user2)))
                .WithImageUrl(await MergeAvatarsAsync(guildUser1, guildUser2)).Build());
        }

        public int GetShipRatio(string input1, string input2) {
            string combinedInput = input1 + input2;
            byte[] inputBytes = Encoding.UTF8.GetBytes(combinedInput);

            using (var hashAlgorithm = SHA256.Create()) {
                byte[] hashBytes = hashAlgorithm.ComputeHash(inputBytes);
                int hashValue = BitConverter.ToInt32(hashBytes, 0);

                int result = Math.Abs(hashValue) % 101;
                return result;
            }
        }

        private readonly HttpClient _httpClient = new HttpClient();

        private async Task<string> MergeAvatarsAsync(SocketUser user1, SocketUser user2) {
            var avatar1 = await DownloadImage(user1.GetAvatarUrl(ImageFormat.Png));
            var avatar2 = await DownloadImage(user2.GetAvatarUrl(ImageFormat.Png));

            var mergedImage = MergeImages(avatar1, avatar2);

            using (var memoryStream = new MemoryStream()) {
                using (var skImage = SKImage.FromBitmap(mergedImage))
                using (var skData = skImage.Encode(SKEncodedImageFormat.Png, 100)) {
                    skData.SaveTo(memoryStream);
                }
                memoryStream.Seek(0, SeekOrigin.Begin);
                var message = await (await CommandProvider.Client.GetChannelAsync(1319455186430984192) as ITextChannel)
                    .SendFileAsync(memoryStream, "merged_avatar.png"); var attachments = message.Attachments.ToArray();
                return attachments[0].Url; }
        }
        private async Task<SKBitmap> DownloadImage(string url) {
            using (var response = await _httpClient.GetAsync(url)) {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                return SKBitmap.Decode(stream);
            }
        }
        private SKBitmap MergeImages(SKBitmap img1, SKBitmap img2) {
            var ship = LoadBitmap(ImagePath);
            var mergedImage = new SKBitmap(ship.Width, ship.Height);

            using (var canvas = new SKCanvas(mergedImage)) {
                canvas.Clear(SKColors.DarkGray);

                canvas.DrawBitmap(img1, User1Position);
                canvas.DrawBitmap(img2, User2Position);

                canvas.DrawBitmap(ship, new SKPoint(0, 0));
            }

            return mergedImage;
        }

        private SKBitmap LoadBitmap(string imagePath) {
            using (var stream = new SKFileStream(imagePath)) {
                return SKBitmap.Decode(stream);
            }
        }
    }
}
