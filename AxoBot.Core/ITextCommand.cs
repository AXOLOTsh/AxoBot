using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxoBot.Core {
    public interface ITextCommand : ICommand {
        public void ExecuteFromText();
    }
}
