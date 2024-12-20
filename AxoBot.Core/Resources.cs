using System.Text.Json;

namespace AxoBot.Core {
    public partial class Resources {
        const string file = "resources.json";
        static Resources instance { get; set; }

        public static void Load() {
            if (File.Exists(file)) {
                try { instance = JsonSerializer.Deserialize<Resources>(File.ReadAllText(file)); }
                catch { instance = new Resources(); File.Copy(file, file + ".corrupted"); }
            }
            else instance = new Resources();
        }
        public static void Save() {
            File.WriteAllText(file, JsonSerializer.Serialize(instance));
        }

        public string BotName { get; set; } = "AxoBot";
        public static string GetBotName() => instance.BotName;
    }
}
