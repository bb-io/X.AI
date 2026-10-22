using Apps.XAI.DataSourceHandlers;
using Apps.XAI.DataSourceHandlers.StaticHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Blueprints.Handlers;
using Blackbird.Applications.SDK.Blueprints.Interfaces.Review;

namespace Apps.XAI.Models.Request
{
    public class ReviewFileRequest : IReviewFileInput
    {
        [Display("File")]
        public FileReference File { get; set; } = new();

        [Display("Source language", Description = "Optional. If not set, it is taken from the file or detected from the content.")]
        [DataSource(typeof(LocaleDataSourceHandler))]
        public string? SourceLanguage { get; set; }

        [Display("Target language")]
        [DataSource(typeof(LocaleDataSourceHandler))]
        public string TargetLanguage { get; set; } = string.Empty;

        [Display("Output file handling", Description = "Determine the format of the output file. The default Blackbird behavior is to convert to XLIFF for future steps.")]
        [StaticDataSource(typeof(ProcessFileFormatHandler))]
        public string? OutputFileHandling { get; set; }

        [Display("Model", Description = "This parameter controls which xAI model answers your request")]
        [DataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; } = string.Empty;

        [Display("Reasoning effort", Description = "How much the model reasons before it answers. Only some models support it, and not every model supports every value. By default, the model's own default is used.")]
        [StaticDataSource(typeof(ReasoningEffortDataSourceHandler))]
        public string? ReasoningEffort { get; set; }

        [Display("Additional instructions", Description = "Specify quality assessment criteria. For example: 'Focus on technical terminology accuracy' or 'Prioritize cultural adaptation'")]
        public string? AdditionalInstructions { get; set; }

        [Display("Max tokens", Description = "The maximum number of tokens to generate before stopping.")]
        public int? MaxTokens { get; set; }

        [Display("Temperature", Description = "Amount of randomness injected into the response.")]
        [StaticDataSource(typeof(TemperatureDataSourceHandler))]
        public double? Temperature { get; set; }

        [Display("Top P", Description = "Use nucleus sampling.")]
        [StaticDataSource(typeof(TopPDataSourceHandler))]
        public double? TopP { get; set; }

        [Display("Score threshold", Description = "All segments above this score will automatically be finalized (0..1)")]
        public double? Threshold { get; set; }

        [Display("Generate document summary", Description = "By default, a short summary of the document is generated and given to the model as context for files of 20 or more segments. This costs one extra request. Set to false to skip it.")]
        public bool? GenerateDocumentSummary { get; set; }

        [Display("Glossary")]
        public FileReference? Glossary { get; set; }
    }
}
