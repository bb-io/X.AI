using System.Text.Json.Serialization;

namespace Apps.XAI.Models.Response
{
    public class ImageModelsResponse
    {
        [JsonPropertyName("models")]
        public IEnumerable<ImageModelDto> Models { get; set; }
    }

    public class ImageModelDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("fingerprint")]
        public string Fingerprint { get; set; }

        [JsonPropertyName("max_prompt_length")]
        public int MaxPromptLength { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("owned_by")]
        public string OwnedBy { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("prompt_text_token_price")]
        public long PromptTextTokenPrice { get; set; }

        [JsonPropertyName("prompt_image_token_price")]
        public long PromptImageTokenPrice { get; set; }

        [JsonPropertyName("generated_image_token_price")]
        public long GeneratedImageTokenPrice { get; set; }

        [JsonPropertyName("aliases")]
        public List<string> Aliases { get; set; }
    }
}
