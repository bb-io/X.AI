using Apps.XAI.DataSourceHandlers;
using Apps.XAI.DataSourceHandlers.StaticHandler;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;
using Newtonsoft.Json;

namespace Apps.XAI.Models.Request
{
    public class ChatCompletionRequest
    {
        [Display("Model", Description = "Model name for the chat completion request.")]
        [JsonProperty("model")]
        [DataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; }

        [Display("System prompt", Description = "Optional system prompt to set the context for the conversation.")]
        [JsonProperty("system_prompt")]
        public string? SystemPrompt { get; set; }

        [Display("Messages", Description = "A list of previous messages for the chat conversation.")]
        [JsonProperty("messages")]
        public List<string>? Messages { get; set; } = new();

        [Display("Parameters", Description = "Additional texts to include in the user prompt.")]
        [JsonProperty("parameters")]
        public IEnumerable<string>? Parameters { get; set; }

        [Display("File", Description = "Optional file (image or audio) to include in the request.")]
        [JsonProperty("file")]
        public FileReference? File { get; set; }

        [Display("Max tokens", Description = "The maximum number of tokens to generate before stopping.")]
        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; set; }

        [Display("Stop sequences", Description = "Sequences that will cause the model to stop generating completion text.")]
        [JsonProperty("stop")]
        public List<string>? Stop { get; set; }

        [Display("Temperature", Description = "Amount of randomness injected into the response. Higher values like 0.8 make the output more random, while lower values like 0.2 make it more focused and deterministic.")]
        [JsonProperty("temperature")]
        [StaticDataSource(typeof(TemperatureDataSourceHandler))]
        public double? Temperature { get; set; }

        [Display("Top P", Description = "An alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with `top_p` probability mass. 0.1 means only the tokens comprising the top 10% probability mass are considered.")]
        [JsonProperty("top_p")]
        [StaticDataSource(typeof(TopPDataSourceHandler))]
        public double? TopP { get; set; }

        [Display("Presence penalty", Description = "Higher values encourage the model to explore new topics and reduce repetition.")]
        [JsonProperty("presence_penalty")]
        public double? PresencePenalty { get; set; }

        [Display("Frequency penalty", Description = "Higher values discourage the model from repeating the same words or phrases multiple times in the same response.")]
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

