using Apps.X.AI.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Newtonsoft.Json;

namespace Apps.X.AI.Models.Request
{
    public class ChatCompletionRequest
    {
        [JsonProperty("model")]
        [StaticDataSource(typeof(ModelDataSourceHandler))]
        public string Model { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }

        [JsonProperty("frequency_penalty")]
        public double? FrequencyPenalty { get; set; }

        [JsonProperty("logit_bias")]
        public Dictionary<string, int>? LogitBias { get; set; }

        [JsonProperty("logprobs")]
        public bool? Logprobs { get; set; }

        [JsonProperty("max_tokens")]
        public int? MaxTokens { get; set; }

        [JsonProperty("n")]
        public int? N { get; set; }

        [JsonProperty("presence_penalty")]
        public double? PresencePenalty { get; set; }

        [JsonProperty("response_format")]
        public string? ResponseFormat { get; set; }

        [JsonProperty("seed")]
        public int? Seed { get; set; }

        [JsonProperty("stop")]
        public List<string>? Stop { get; set; }

        [JsonProperty("stream")]
        public bool? Stream { get; set; }

        [JsonProperty("stream_options")]
        public string? StreamOptions { get; set; }

        [JsonProperty("temperature")]
        public double? Temperature { get; set; }

        [JsonProperty("top_p")]
        public double? TopP { get; set; }

        [JsonProperty("user")]
        public string? User { get; set; }
    }

    public class Message
    {
        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }
    }
}

