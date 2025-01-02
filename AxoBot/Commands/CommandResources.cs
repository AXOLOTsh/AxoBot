using AxoBot.Core;
using System.Text.Json;

namespace AxoBot.Commands {
    public class CommandResources {
        const string file = "command_resources.json";
        static CommandResources instance { get; set; }

        public static void Load() {
            if (File.Exists(file)) {
                try { instance = JsonSerializer.Deserialize<CommandResources>(File.ReadAllText(file)); }
                catch { instance = new CommandResources(); File.Copy(file, file + ".corrupted"); }
            }
            else instance = new CommandResources();
        }
        public static void Save() {
            File.WriteAllText(file, JsonSerializer.Serialize(instance, new JsonSerializerOptions() { WriteIndented = true }));
        }

        public HelpCommand HelpCommand { get; set; } = new HelpCommand();
        public static HelpCommand GetHelpCommand() => instance.HelpCommand;

        public AboutCommand AboutCommand { get; set; } = new AboutCommand();
        public static AboutCommand GetAboutCommand() => instance.AboutCommand;

        public ShipCommand ShipCommand { get; set; } = new ShipCommand();
        public static ShipCommand GetShipCommand() => instance.ShipCommand;

        public static BaseCommand[] GetBotCommands() => [
            GetHelpCommand(),
            GetAboutCommand(),

            GetShipCommand(),
            ];
    }
}
