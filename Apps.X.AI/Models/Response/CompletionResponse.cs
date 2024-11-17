using Apps.X.AI.Models.Request;
using Newtonsoft.Json;

namespace Apps.X.AI.Models.Response
{
    public class CompletionResponse
    {
        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }
    }

    public class Choice
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("message")]
        public Message? Message { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
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

    public class ChatResponse
    {
        public string Message { get; set; }
        public string SystemPrompt { get; set; }
        public string UserPrompt { get; set; }
        public Usage Usage { get; set; }
    }
}
