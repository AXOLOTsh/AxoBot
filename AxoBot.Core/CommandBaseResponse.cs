using Discord;
using static AxoBot.Core.Resources;

namespace AxoBot.Core {
    public partial class Resources {
        public string DefaultColor { get; set; } = "#5865f2";
        public static Color GetDefaultColor() => Color.Parse(instance.DefaultColor);
        public string SuccessColor { get; set; } = "#3ba55c";
        public static Color GetSuccessColor() => Color.Parse(instance.SuccessColor);
        public string WarningColor { get; set; } = "#ffd252";
        public static Color GetWarningColor() => Color.Parse(instance.WarningColor);
        public string ErrorColor { get; set; } = "#ed4245";
        public static Color GetErrorColor() => Color.Parse(instance.ErrorColor);
        public string DebugColor { get; set; } = "#4f545c";
        public static Color GetDebugColor() => Color.Parse(instance.DebugColor);

        public string CommandCompleteString { get; set; } = "Command completed.";
        public static string GetCommandCompleteString() => instance.CommandCompleteString;
        public string CommandErrorString { get; set; } = "An error occurred!";
        public static string GetCommandErrorString() => instance.CommandErrorString;
        public string CommandTreatmentString { get; set; } = "Please wait...";
        public static string GetCommandTreatmentString() => instance.CommandTreatmentString;
        public string CommandInfoString { get; set; } = "Information:";
        public static string GetCommandInfoString() => instance.CommandInfoString;
        public string CommandDebugString { get; set; } = "Debug Information:";
        public static string GetCommandDebugString() => instance.CommandDebugString;
    }
    public partial class BaseCommand {
        public EmbedBuilder GetDefaultEmbedColor() => new EmbedBuilder().WithColor(GetDefaultColor());
        public EmbedBuilder GetSuccessEmbedColor() => new EmbedBuilder().WithColor(GetSuccessColor());
        public EmbedBuilder GetWarningEmbedColor() => new EmbedBuilder().WithColor(GetWarningColor());
        public EmbedBuilder GetErrorEmbedColor() => new EmbedBuilder().WithColor(GetErrorColor());
        public EmbedBuilder GetDebugEmbedColor() => new EmbedBuilder().WithColor(GetDebugColor());

        public EmbedBuilder GetSuccessEmbed() => GetSuccessEmbed(GetCommandCompleteString());
        public EmbedBuilder GetSuccessEmbed(string name, string content = null) =>
            GetEmbed(name, content)
            .WithColor(GetSuccessColor());

        public EmbedBuilder GetErrorEmbed() => GetErrorEmbed(GetCommandErrorString());
        public EmbedBuilder GetErrorEmbed(string name, string content = null) =>
            GetEmbed(name, content)
            .WithColor(GetErrorColor());

        public EmbedBuilder GetTreatmentEmbed() => GetTreatmentEmbed(GetCommandTreatmentString());
        public EmbedBuilder GetTreatmentEmbed(string name, string content = null) =>
            GetEmbed(name, content)
            .WithColor(GetWarningColor());

        public EmbedBuilder GetInfoEmbed() => GetInfoEmbed(GetCommandInfoString());
        public EmbedBuilder GetInfoEmbed(string name, string content = null) =>
            GetEmbed(name, content)
            .WithColor(GetDefaultColor());

        public EmbedBuilder GetDebugEmbed() => GetDebugEmbed(GetCommandDebugString());
        public EmbedBuilder GetDebugEmbed(string name, string content = null) =>
            GetEmbed(name, content)
            .WithColor(GetDebugColor());

        public EmbedBuilder GetEmbed(string name, string content = null) {
            var output = new EmbedBuilder()
            .WithTitle(name);

            if (content != null)
                output.WithDescription(content);

            return output;
        }


        public SlashCommandBuilder GetDefaultSlashCommandBuilder() => new SlashCommandBuilder()
            .WithName(Name.ToLower())
            .WithDescription(Description);
    }
}
