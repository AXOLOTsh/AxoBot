using Discord;
using Discord.WebSocket;

namespace AxoBot.Core {
    public partial class CommandProvider {
        public IReadOnlyList<ICommand> Commands => _commands;
        private List<ICommand> _commands = new List<ICommand>();

        public DiscordSocketClient Client { get; private set; }
        public IGuild Guild { get; private set; }

        private CommandProvider() { }

        public static CommandProvider UseCommandProvider(DiscordSocketClient client, IGuild guild = null) {
            var output = new CommandProvider() {
                Client = client,
                Guild = guild
            };

            client.SlashCommandExecuted += output.ProceedSlashCommand;

            return output;
        }

        public async Task RegisterCommandsAsync(ICommand[] commands) {
            foreach (var command in commands) {
                await RegisterCommand(command);
            }
        }
        public async Task RegisterCommand(ICommand command) {
            Console.WriteLine("Register Command: " + command.Name);

            if (command is ISlashCommand) await RegisterSlashCommand(command as ISlashCommand);

            command.Client = Client;
            command.CommandProvider = this;

            _commands.Add(command);
        }
    }
}
