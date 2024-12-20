namespace AxoBot.Core {
    public partial class BaseCommand {/*

        const string InfoButtonId = "StatusProfile";
        const string InfoButtonNameValueId = "Name";
        const string InfoButtonContentValueId = "Info";
        public static ComponentBuilder GetInfoButton(string content, string label = "Информация", ButtonStyle style = ButtonStyle.Primary) =>
            new ComponentBuilder().WithButton(label,);
        public static async Task ProceedInfoButton(DiscordClient client, InteractionCreateEventArgs e, DiscordInteraction interaction, InteractionModel data) {
            var name = data.Values[InfoButtonNameValueId] as string;
            var content = data.Values[InfoButtonContentValueId] as string;
            await RespondAsync(interaction, GetInfoMessage(name, content));
        }

        public static async Task ProceedResponse(SocketMessageComponent arg) {
        }

        public string GetComponentId(string name, Dictionary<string, string> data = null) => $"{name}:{JsonSerializer.Serialize(data)}";
        public string GetComponentName(string data) => data.Split(':')[0];
        public Dictionary<string, string> GetComponentData(string data) => JsonSerializer.Deserialize<Dictionary<string, string>>(data.Replace(GetComponentName(data) + ':', ""));*/
    }
}
