using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxoBot.Core {
    public partial interface ICommand {
        public DiscordSocketClient Client { get; set; }
        public CommandProvider CommandProvider { get; set; }

        public CommandType CommandType { get; set; }

        public string Category { get; }
        public string Name { get; }
        public string Description { get; }
    }
}
