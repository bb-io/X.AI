using Apps.XAI.DataSourceHandlers;
using Apps.XAI.DataSourceHandlers.StaticHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XAI.Models.Request
{
    public class ShortenTextRequest
    {
        [Display("Text")]
        public string Text { get; set; } = string.Empty;

        [Display("Maximum length", Description = "Maximum number of characters of the shortened text.")]
        public int MaximumLength { get; set; }

        [Display("Model", Description = "This parameter controls which xAI model answers your request")]
        [DataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; } = string.Empty;

        [Display("Reasoning effort", Description = "How much the model reasons before it answers. Only some models support it, and not every model supports every value. By default, the model's own default is used.")]
        [StaticDataSource(typeof(ReasoningEffortDataSourceHandler))]
        public string? ReasoningEffort { get; set; }

        [Display("Retries", Description = "Number of extra attempts when the text is still too long. Default value: 3")]
        public int? Retries { get; set; }

        [Display("Additional instructions", Description = "Specify additional instructions to apply when shortening.")]
        public string? AdditionalInstructions { get; set; }

        [Display("Glossary")]
        public FileReference? Glossary { get; set; }
    }
}
