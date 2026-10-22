using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.SDK.Blueprints.Interfaces.Translate;

namespace Apps.XAI.Models.Response
{
    public class TranslateTextResponse : ITranslateTextOutput
    {
        [Display("Translated text")]
        public string TranslatedText { get; set; } = string.Empty;

        [Display("System prompt")]
        public string SystemPrompt { get; set; } = string.Empty;

        [Display("User prompt")]
        public string UserPrompt { get; set; } = string.Empty;

        [Display("Usage")]
        public Usage Usage { get; set; } = new();
    }
}
