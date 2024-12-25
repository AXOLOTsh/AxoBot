using AxoBot.Core;
using Discord;
using Discord.WebSocket;
using static AxoBot.Commands.CommandResources;

namespace AxoBot.Commands {
    internal class HelpCommand : BaseCommand, ISlashCommand {
        public override string Name => "Help";
        public override string Description => "Provides a list of commands.";
        public override string Category => "Info";
        public string UnknownCategoryName { get; private set; } = "Other";

        public SlashCommandProperties RegisterAsSlash() => GetDefaultSlashCommandBuilder().Build();

        public async Task ExecuteFromSlash(SocketSlashCommand arg) {
            var dictionary = new Dictionary<string, List<BaseCommand>>() { { UnknownCategoryName, new List<BaseCommand>() } };
            foreach (var command in CommandProvider.Commands) {
                if (command.Category == null) {
                    dictionary[UnknownCategoryName].Add(command);
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
