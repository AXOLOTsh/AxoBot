using System.Text.Json;

namespace AxoBot.Commands {
    public partial class CommandResources {
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

        private const string InfoCategory = "Info";
        private const string FunCategoryId = "Fun";
    }
}
