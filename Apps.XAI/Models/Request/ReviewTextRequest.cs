using Apps.XAI.DataSourceHandlers;
using Apps.XAI.DataSourceHandlers.StaticHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Blueprints.Interfaces.Review;

namespace Apps.XAI.Models.Request
{
    public class ReviewTextRequest : IReviewTextInput
    {
        [Display("Source text")]
        public string SourceText { get; set; } = string.Empty;

        [Display("Target text")]
        public string TargetText { get; set; } = string.Empty;

        [Display("Source language")]
        [DataSource(typeof(LocaleDataSourceHandler))]
        public string SourceLanguage { get; set; } = string.Empty;

        [Display("Target language")]
        [DataSource(typeof(LocaleDataSourceHandler))]
        public string TargetLanguage { get; set; } = string.Empty;

        [Display("Model", Description = "This parameter controls which xAI model answers your request")]
        [DataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; } = string.Empty;

        [Display("Reasoning effort", Description = "How much the model reasons before it answers. Only some models support it, and not every model supports every value. By default, the model's own default is used.")]
        [StaticDataSource(typeof(ReasoningEffortDataSourceHandler))]
        public string? ReasoningEffort { get; set; }

        [Display("Additional instructions", Description = "Additional instructions applied to the review.\nFor example: 'Focus on technical terminology accuracy.'")]
        public string? AdditionalInstructions { get; set; }

        [Display("Max tokens", Description = "The maximum number of tokens to generate before stopping.")]
        public int? MaxTokens { get; set; }

        [Display("Temperature", Description = "Amount of randomness injected into the response.")]
        [StaticDataSource(typeof(TemperatureDataSourceHandler))]
        public double? Temperature { get; set; }

        [Display("Top P", Description = "Use nucleus sampling.")]
        [StaticDataSource(typeof(TopPDataSourceHandler))]
        public double? TopP { get; set; }

        [Display("Glossary")]
        public FileReference? Glossary { get; set; }
    }
}
