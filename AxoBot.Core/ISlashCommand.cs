using Discord;
using Discord.WebSocket;

namespace AxoBot.Core {
    public interface ISlashCommand : ICommand {
        public SlashCommandProperties RegisterAsSlash();
        public Task ExecuteFromSlash(SocketSlashCommand arg);
    }
}
