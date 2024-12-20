using AxoBot.Core;
using Discord;
using Discord.WebSocket;
using static AxoBot.Commands.CommandResources;

namespace AxoBot.Commands {
    public partial class CommandResources {
        public HelpCommandResources HelpCommand { get; set; } = new HelpCommandResources();
        public static HelpCommandResources GetHelpCommandResources() => instance.HelpCommand;
        public class HelpCommandResources : BaseCommandResources {
            public HelpCommandResources() : base("Help", "Provides a list of commands.", InfoCategory) { }
            public string OtherCategoryName { get; set; } = "Other";
        }
    }

    internal class HelpCommand : BaseCommand {
        public string OtherCategoryName { get; private set; }
        public HelpCommand() : base(GetHelpCommandResources()) {
            var resources = GetHelpCommandResources();
            OtherCategoryName = resources.OtherCategoryName;
        }

        public override SlashCommandProperties RegisterAsSlash(DiscordSocketClient client) => GetDefaultSlashCommandBuilder().Build();

        public override async Task ExecuteFromSlash(SocketSlashCommand arg) { //TODO Следует проверить
            var dictionary = new Dictionary<string, List<BaseCommand>>() { { OtherCategoryName, new List<BaseCommand>() } };
            foreach (var command in CommandProvider.Commands) {
                if (command.Category == null) {
                    dictionary[OtherCategoryName].Add(command);
                }
                else {
                    var category = command.Category;
                    if (!dictionary.ContainsKey(category))
                        dictionary.Add(category, new List<BaseCommand>() { command });
                    else
                        dictionary[category].Add(command);
                }
            }

            var embed = GetDefaultEmbedColor()
                .WithTitle(Name);

            foreach (var key in dictionary.Keys) {
                var list = dictionary[key];
                var description = "";
                foreach (var item in list) {
                    description += $"**{item.Name}**\n{item.Description}\n\n";
                }
                if (description != string.Empty) embed.AddField(key, description);
            }

            await arg.RespondAsync(embed: embed.Build(), ephemeral: true);
        }
    }
}
