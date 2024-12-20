using Discord;
using Discord.WebSocket;

namespace AxoBot.Core {
    public partial class CommandProvider {
        public IReadOnlyList<BaseCommand> Commands => _commands;
        private List<BaseCommand> _commands = new List<BaseCommand>();

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

        public async Task RegisterCommandsAsync(BaseCommand[] commands) {
            foreach (var command in commands) {
                await RegisterCommand(command);
            }
        }
        public async Task RegisterCommand(BaseCommand command) {
            Console.WriteLine("Register Command: " + command.Name);

            var slash = command.RegisterAsSlash(Client);
            if (slash != null) await RegisterSlashCommand(command, slash);

            command.Client = Client;
            command.CommandProvider = this;

            _commands.Add(command);
        }
    }
}
