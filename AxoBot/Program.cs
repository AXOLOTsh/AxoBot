using AxoBot.Commands;
using AxoBot.Core;
using AxoBot.Discord;

namespace AxoBot {
    internal class Program {
        public const string TempDirectory = "temp";
        public const string Version = "ALPHA 1";
        public static Bot Bot { get; private set; }
        static async Task Main(string[] args) {
            if (Directory.Exists(TempDirectory)) {
                //Directory.Delete(TempDirectory, true);
            }
            Directory.CreateDirectory(TempDirectory);

            Resources.Load();
            CommandResources.Load();

            Bot = new Bot();
            await Bot.StartAsync();
            Console.ReadLine();
            await Bot.StopAsync();

            Resources.Save();
            CommandResources.Save();
            //Directory.Delete(TempDirectory, true);
        }
    }
}
