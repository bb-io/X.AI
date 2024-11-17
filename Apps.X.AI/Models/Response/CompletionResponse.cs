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

        [JsonProperty("usage")]
        public Usage Usage { get; set; }
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
        [JsonProperty("message")]
        public string Message { get; set; } 

        [JsonProperty("system_prompt")]
        public string SystemPrompt { get; set; } 

        [JsonProperty("user_prompt")]
        public string UserPrompt { get; set; } 

        [JsonProperty("usage")]
        public Usage Usage { get; set; }

        [JsonProperty("history")]
        public List<Message> History { get; set; }
    }
}
