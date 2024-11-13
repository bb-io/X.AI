using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Response
{
    public class CompletionResponse
    {
        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("system_fingerprint")]
        public string SystemFingerprint { get; set; }

        [JsonProperty("usage")]
        public Usage? Usage { get; set; }
    }

    public class Choice
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("logprobs")]
        public Logprobs? Logprobs { get; set; }

        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }

    public class Logprobs
    {
        [JsonProperty("tokens")]
        public List<string> Tokens { get; set; }

        [JsonProperty("token_logprobs")]
        public List<double> TokenLogprobs { get; set; }

        [JsonProperty("top_logprobs")]
        public List<Dictionary<string, double>> TopLogprobs { get; set; }
    }

    public class Usage
    {
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
