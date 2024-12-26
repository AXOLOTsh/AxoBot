using Discord.WebSocket;
using System.Text.Json.Serialization;

namespace AxoBot.Core {
    public partial class BaseCommand : ICommand {
        [JsonIgnore]
        public virtual DiscordSocketClient Client { get; set; }
        [JsonIgnore]
        public virtual CommandProvider CommandProvider { get; set; }

        public virtual string Category { get; private set; }
        public virtual string Name { get; private set; }
        public virtual string Description { get; private set; }

        public BaseCommand() { }
        public BaseCommand(string name, string description, string category = null) {
            Name = name;
            Description = description;
            Category = category;
        }
    }
}
