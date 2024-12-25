using AxoBot.Core;
using Discord;
using Discord.WebSocket;
using static AxoBot.Commands.CommandResources;

namespace AxoBot.Commands {
    public class AboutCommand : BaseCommand, ISlashCommand {
        public override string Category => "Info";

        public override string Name => "About";

        public override string Description => "Provides information about the bot.";

        public SlashCommandProperties RegisterAsSlash() => GetDefaultSlashCommandBuilder().Build();
        public async Task ExecuteFromSlash(SocketSlashCommand arg) {
            await arg.RespondAsync(embed:
                GetInfoEmbed($"AxoBot {Program.Version}")
                .WithDescription("GitHub: https://github.com/AXOLOTsh/AxoBot")
                .WithFooter("Made by AXOLOTsh").Build(), ephemeral: true);
        }
    }
}
