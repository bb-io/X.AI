using Apps.XAI.DataSourceHandlers;
using Apps.XAI.DataSourceHandlers.StaticHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XAI.Models.Request
{
    public class SummarizeContentRequest
    {
        [Display("File")]
        public FileReference File { get; set; } = new();

        [Display("Model", Description = "This parameter controls which xAI model answers your request")]
        [DataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; } = string.Empty;

        [Display("Reasoning effort", Description = "How much the model reasons before it answers. Only some models support it, and not every model supports every value. By default, the model's own default is used.")]
        [StaticDataSource(typeof(ReasoningEffortDataSourceHandler))]
        public string? ReasoningEffort { get; set; }

        [Display("Language", Description = "Language of the summary. By default, the language of the content.")]
        [DataSource(typeof(LocaleDataSourceHandler))]
        public string? Language { get; set; }

        [Display("Maximum words", Description = "Maximum number of words of the summary.")]
        public int? MaximumWords { get; set; }

        [Display("Target audience")]
        public string? TargetAudience { get; set; }

        [Display("Additional instructions", Description = "Specify additional instructions for the summary. For example: 'Use bullet points.'")]
        public string? AdditionalInstructions { get; set; }

        [Display("Glossary")]
        public FileReference? Glossary { get; set; }
    }
}
