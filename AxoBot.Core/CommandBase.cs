using Discord;
using Discord.WebSocket;

namespace AxoBot.Core {
    public enum CommandType { Console, Text, Slash }
    public partial class BaseCommand { //TODO Реализовать интерфейсы
        public DiscordSocketClient Client { get; internal set; }
        public CommandProvider CommandProvider { get; internal set; }

        public CommandType CommandType { get; internal set; }

        public string Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public BaseCommand(BaseCommandResources resources) : this(resources.Name, resources.Description, resources.Category) { }
        public BaseCommand(string name, string description, string category = null) {
            Name = name;
            Description = description;
            Category = category;
        }

        public virtual void ExecuteFromConsole() {
        }

        public virtual void ExecuteFromText() {
        }

        public virtual SlashCommandProperties RegisterAsSlash(DiscordSocketClient client) {
            return null;
        }
        public virtual async Task ExecuteFromSlash(SocketSlashCommand arg) {
        }
    }
    public class BaseCommandResources {
        public BaseCommandResources() { }

        public BaseCommandResources(string name, string description, string category) {
            Name = name;
            Description = description;
            Category = category;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}
