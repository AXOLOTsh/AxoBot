using Discord;
using Discord.WebSocket;

namespace AxoBot.Core {
    public partial class CommandProvider {
        private Dictionary<string, ISlashCommand> SlashCommands { get; set; } = new Dictionary<string, ISlashCommand>();

        private async Task ProceedSlashCommand(SocketSlashCommand arg) {
            Console.WriteLine();
            Console.WriteLine($"Slash command received. Delay time: {DateTime.Now - arg.CreatedAt}");

            var command = arg.Data.Name;
            ISlashCommand executer;
            SlashCommands.TryGetValue(command, out executer);
            if (executer != null) await executer.ExecuteFromSlash(arg);
            Console.WriteLine($"Slash command responded. Delay time: {DateTime.Now - arg.CreatedAt}");
        }

        private async Task RegisterSlashCommand(ISlashCommand command) {
            var properties = command.RegisterAsSlash();
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
