using AxoBot.Commands;
using AxoBot.Core;
using Discord;
using Discord.WebSocket;
using System.Text.Json;

namespace AxoBot.Discord {
    public class Bot {
        public DiscordSocketClient Client { get; private set; }
        public CommandProvider CommandProvider { get; private set; }

        public async Task StartAsync() {
            CommandResources.Load();
            CommandResources.Save();

            Client = new DiscordSocketClient();

            await Client.LoginAsync(TokenType.Bot, File.ReadAllText("token"));

            Client.Log += async Task (message) => { Console.WriteLine(message.ToString()); };
            Client.Ready += async Task () => {
                CommandProvider = CommandProvider.UseCommandProvider(Client, Client.GetGuild(675651071581880320));

                await CommandProvider.RegisterCommandsAsync(CommandResources.GetBotCommands());
            };
            await Client.StartAsync();
        }
        public async Task StopAsync() {
            await Client.StopAsync();
            await Client.LogoutAsync();
        }
    }
}
