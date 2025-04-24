using Newtonsoft.Json;

namespace Apps.XAI.Models.Response
{
    public class ChatCompletionResponse
    {
        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }

        [JsonProperty("usage")]
        public Usage Usage { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("system_fingerprint")]
        public string SystemFingerprint { get; set; }
    }
}

