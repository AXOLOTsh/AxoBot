using AxoBot.Core;
using Discord;
using Discord.WebSocket;
using static AxoBot.Commands.CommandResources;

namespace AxoBot.Commands {
    public partial class CommandResources {
        public AboutCommandResources AboutCommand { get; set; } = new AboutCommandResources();
        public static AboutCommandResources GetAboutCommandResources() => instance.AboutCommand;
        public class AboutCommandResources : BaseCommandResources {
            public AboutCommandResources() : base("About", "Provides information about the bot.", InfoCategory) { }
        }
    }

    public class AboutCommand : BaseCommand {
        public AboutCommand() : base(GetAboutCommandResources()) { }

        public override SlashCommandProperties RegisterAsSlash(DiscordSocketClient client) => GetDefaultSlashCommandBuilder().Build();
        public override async Task ExecuteFromSlash(SocketSlashCommand arg) {
            await arg.RespondAsync(embed:
                GetInfoEmbed($"{Resources.GetBotName()} {Program.Version}")
                .WithFooter("Made by AXOLOTsh").Build(), ephemeral: true);
        }
    }
}
