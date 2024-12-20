using Discord;
using Discord.WebSocket;

namespace AxoBot.Core {
    public partial class CommandProvider {
        private Dictionary<string, BaseCommand> SlashCommands { get; set; } = new Dictionary<string, BaseCommand>();

        private async Task ProceedSlashCommand(SocketSlashCommand arg) {
            var command = arg.Data.Name;
            BaseCommand executer;
            SlashCommands.TryGetValue(command, out executer);
            if (executer != null) await executer.ExecuteFromSlash(arg);
        }

        private async Task RegisterSlashCommand(BaseCommand command, SlashCommandProperties properties) {
            try {
                if (Guild != null)
                    await Guild.CreateApplicationCommandAsync(properties);
                else
                    await Client.CreateGlobalApplicationCommandAsync(properties);
            }
            catch { }

            command.CommandType = command.CommandType | CommandType.Slash;
            foreach (var item in GetSlashCommandNames(properties)) {
                SlashCommands.Add(item, command);
            }
        }
        private static string[] GetSlashCommandNames(SlashCommandProperties properties) {
            var output = new List<string> {
                properties.Name.Value
            };
            try { //TODO
                foreach (var item in properties.Options.Value) {
                    if (item.Type == ApplicationCommandOptionType.SubCommand) {
                        output.Add(item.Name);
                    }
                }
            }
            catch { }
            return output.ToArray();
        }
    }
}
