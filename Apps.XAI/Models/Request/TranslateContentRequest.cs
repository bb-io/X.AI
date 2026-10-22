using Apps.XAI.DataSourceHandlers;
using Apps.XAI.DataSourceHandlers.StaticHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Blueprints.Handlers;
using Blackbird.Applications.SDK.Blueprints.Interfaces.Translate;

namespace Apps.XAI.Models.Request
{
    public class TranslateContentRequest : ITranslateFileInput
    {
        [Display("File")]
        public FileReference File { get; set; } = new();

        [Display("Target language")]
        [DataSource(typeof(LocaleDataSourceHandler))]
        public string TargetLanguage { get; set; } = string.Empty;

        [Display("Model", Description = "This parameter controls which xAI model answers your request")]
        [DataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; } = string.Empty;

        [Display("Reasoning effort", Description = "How much the model reasons before it answers. Only some models support it, and not every model supports every value. By default, the model's own default is used.")]
        [StaticDataSource(typeof(ReasoningEffortDataSourceHandler))]
        public string? ReasoningEffort { get; set; }

        [Display("Source language", Description = "Optional. If not set, it is taken from the file or detected from the content.")]
        [DataSource(typeof(LocaleDataSourceHandler))]
        public string? SourceLanguage { get; set; }

        [Display("Output file handling", Description = "Determine the format of the output file. The default Blackbird behavior is to convert to XLIFF for future steps.")]
        [StaticDataSource(typeof(ProcessFileFormatHandler))]
        public string? OutputFileHandling { get; set; }

        [Display("Additional instructions", Description = "Specify additional instructions to be applied to the translation")]
        public string? AdditionalInstructions { get; set; }

        [Display("Bucket size", Description = "Specify the maximum number of source texts to be translated at once. Default value: 250. Buckets are also sized to fit the model's output limit. (See our documentation for an explanation)")]
        public int? BucketSize { get; set; }

        [Display("Generate document summary", Description = "By default, a short summary of the document is generated and given to the model as context for files of 20 or more segments. This costs one extra request. Set to false to skip it.")]
        public bool? GenerateDocumentSummary { get; set; }

        [Display("Glossary")]
        public FileReference? Glossary { get; set; }
    }
}
