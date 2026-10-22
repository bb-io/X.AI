using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Blueprints.Interfaces.Translate;

namespace Apps.XAI.Models.Response
{
    public class TranslateFileResult : ITranslateFileOutput
    {
        [Display("Translated file")]
        public FileReference File { get; set; } = new();

        [Display("Total segments")]
        public int TotalSegmentsCount { get; set; }

        [Display("Total translatable segments")]
        public int TotalTranslatable { get; set; }

        [Display("Targets updated")]
        public int TargetsUpdatedCount { get; set; }

        [Display("Processed batches")]
        public int ProcessedBatchesCount { get; set; }

        [Display("System prompt")]
        public string? SystemPrompt { get; set; }

        [Display("Usage")]
        public Usage Usage { get; set; } = new();
    }
}
