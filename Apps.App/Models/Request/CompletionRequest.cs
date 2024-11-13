using Newtonsoft.Json;

namespace Apps.X.AI.Models.Request
{
    public class CompletionRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("best_of")]
        public int? BestOf { get; set; }

        [JsonProperty("echo")]
        public bool? Echo { get; set; }

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

        [JsonProperty("seed")]
        public int? Seed { get; set; }

        [JsonProperty("stop")]
        public List<string>? Stop { get; set; }

        [JsonProperty("stream")]
        public bool? Stream { get; set; }

        [JsonProperty("stream_options")]
        public string? StreamOptions { get; set; }

        [JsonProperty("suffix")]
        public string? Suffix { get; set; }

        [JsonProperty("temperature")]
        public double? Temperature { get; set; }

        [JsonProperty("top_p")]
        public double? TopP { get; set; }

        [JsonProperty("user")]
        public string? User { get; set; }
    }
}
