using Blackbird.Applications.Sdk.Common;

namespace Apps.XAI.Models.Response
{
    public class SummarizeResponse
    {
        [Display("Summary")]
        public string Summary { get; set; } = string.Empty;

        [Display("System prompt")]
        public string SystemPrompt { get; set; } = string.Empty;

        [Display("Usage")]
        public Usage Usage { get; set; } = new();
    }
}
