using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.SDK.Blueprints.Interfaces.Edit;

namespace Apps.XAI.Models.Response
{
    public class EditTextResponse : IEditTextOutput
    {
        [Display("Edited text")]
        public string EditedText { get; set; } = string.Empty;

        [Display("System prompt")]
        public string SystemPrompt { get; set; } = string.Empty;

        [Display("User prompt")]
        public string UserPrompt { get; set; } = string.Empty;

        [Display("Usage")]
        public Usage Usage { get; set; } = new();
    }
}
