using Apps.XAI.Models.Request;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.XAI.Models.Response
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
        [Display("Prompt tokens")]
        public int PromptTokens { get; set; }

        [JsonProperty("completion_tokens")]
        [Display("Completion tokens")]
        public int CompletionTokens { get; set; }

        [JsonProperty("total_tokens")]
        [Display("Total tokens")]
        public int TotalTokens { get; set; }
    }

    public class ChatResponse
    {
        [JsonProperty("model")]
        [Display("Model", Description = "Currently using model")]
        public string Model { get; set; }

        [JsonProperty("message")]
        [Display("Message", Description = "Chat answer from X.AI service")]
        public string Message { get; set; }

        [Display("Usage", Description = "Description of the usage")]
        [JsonProperty("usage")]
        public Usage Usage { get; set; }
    }
}
