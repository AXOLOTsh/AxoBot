using Discord.WebSocket;
using System.Text.Json.Serialization;

namespace AxoBot.Core {
    public partial interface ICommand {
        [JsonIgnore]
        public DiscordSocketClient Client { get; set; }
        [JsonIgnore]
        public CommandProvider CommandProvider { get; set; }

        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
    }
}
