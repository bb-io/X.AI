using Apps.XAI.DataSourceHandlers;
using Apps.XAI.DataSourceHandlers.StaticHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.XAI.Models.Request
{
    public class CompletionRequest
    {
        [Display("Model", Description = "This parameter controls which version of X.AI answers your request")]
        [DataSource(typeof(ModelDataSourceHandler))]
        [JsonProperty("model")]
        public string Model { get; set; }

        [Display("Prompt", Description = "The prompt that you want X.AI to complete.")]
        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [Display("Max tokens", Description = "The maximum number of tokens to generate before stopping.")]
        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; set; }

        [Display("Stop sequences", Description = "Sequences that will cause the model to stop generating completion text.")]
        [JsonProperty("stop")]
        public List<string>? Stop { get; set; }

        [Display("Temperature", Description = "Amount of randomness injected into the response.Higher values like 0.8 will make the output more random, while lower values like 0.2 will make it more focused and deterministic.")]
        [JsonProperty("temperature")]
        [StaticDataSource(typeof(TemperatureDataSourceHandler))]
        public double? Temperature { get; set; }

        [Display("top_p", Description = "An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with `top_p` probability mass. \nSo 0.1 means only the tokens comprising the top 10% probability mass are considered. \nWe generally recommend altering this or temperature but not both.")]
        [JsonProperty("top_p")]
        [StaticDataSource(typeof(TopPDataSourceHandler))]
        public double? TopP { get; set; }
    }
}
