using Discord;
using Discord.WebSocket;

namespace AxoBot.Core {
    public enum CommandType { Console, Text, Slash }
    public partial class BaseCommand : ICommand {
        public virtual DiscordSocketClient Client { get; set; }
        public virtual CommandProvider CommandProvider { get; set; }
        public virtual CommandType CommandType { get; set; }

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
