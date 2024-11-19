using Apps.XAI.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XAI.Models.Request
{
    public class ChatCompletionRequest
    {
        [Display("Model", Description = "Model name for the chat completion request.")]
        [JsonProperty("model")]
        [StaticDataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; }

        [Display("Messages", Description = "A list of messages for the chat conversation.")]
        [JsonProperty("messages")]
        public List<string>? Messages { get; set; } = new();

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

        [Display("Presence penalty", Description = "Higher values encourage the model to explore new topics and reduce repetition.")]
        [JsonProperty("presence_penalty")]
        public double? PresencePenalty { get; set; }

        [Display("Frequency Penalty", Description = "Higher values discourage the model from repeating the same words or phrases multiple times in the same response")]
        [JsonProperty("frequency_penalty")]
        public double? FrequencyPenalty { get; set; }
    }

    public class Message
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("refusal")]
        public string? Refusal { get; set; }
    }
}

