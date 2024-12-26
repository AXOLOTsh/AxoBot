namespace AxoBot.Core {
    public interface IConsoleCommand : ICommand {
        public void ExecuteFromConsole();
    }
}
