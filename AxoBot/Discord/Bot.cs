﻿using AxoBot.Commands;
using AxoBot.Core;
using Discord;
using Discord.WebSocket;

namespace AxoBot.Discord {
    public class Bot {
        public DiscordSocketClient Client { get; private set; }
        public CommandProvider CommandProvider { get; private set; }
        public async Task StartAsync() {
            Client = new DiscordSocketClient();

            await Client.LoginAsync(TokenType.Bot, File.ReadAllText("token"));

            Client.Log += async Task (message) => { Console.WriteLine(message.ToString()); };
            Client.Ready += async Task () => {
                CommandProvider = CommandProvider.UseCommandProvider(Client, Client.GetGuild(1222025024144412733));

                await CommandProvider.RegisterCommandsAsync([
                    new HelpCommand(),
                    new AboutCommand(),

                    new ShipCommand(),
                    ]);
            };

            await Client.StartAsync();
        }

        public async Task StopAsync() {
            await Client.StopAsync();
            await Client.LogoutAsync();
        }
    }
}