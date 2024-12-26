using AxoBot.Core;
using Discord;
using Discord.WebSocket;

namespace AxoBot.Commands {
    public class AboutCommand : BaseCommand, ISlashCommand {
        public override string Category => "Info";
        public override string Name => "About";
        public override string Description => "Provides information about the bot.";

        public string EmbedTitle => $"AxoBot {Program.Version}";
        public string EmbedDescription => "GitHub: https://github.com/AXOLOTsh/AxoBot";
        public string EmbedFooter => "Made by AXOLOTsh";

        public AboutCommand() { }

        public SlashCommandProperties RegisterAsSlash() => GetDefaultSlashCommandBuilder().Build();
        public async Task ExecuteFromSlash(SocketSlashCommand arg) {
            await arg.RespondAsync(embed:
                GetInfoEmbed(EmbedTitle)
                .WithDescription(EmbedDescription)
                .WithFooter(EmbedFooter).Build(), ephemeral: true);
        }
    }
}
