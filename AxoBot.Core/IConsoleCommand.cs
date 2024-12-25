using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxoBot.Core {
    public interface IConsoleCommand : ICommand {
        public void ExecuteFromConsole();
    }
}
