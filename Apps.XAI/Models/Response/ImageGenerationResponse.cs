using Newtonsoft.Json;

namespace Apps.XAI.Models.Response
{
    public class ImageGenerationResponse
    {
        [JsonProperty("data")]
        public List<ImageGenerationDataDto> Data { get; set; }
    }
    public class ImageGenerationDataDto
    {
        [JsonProperty("b64_json")]
        public string B64Json { get; set; }

        [JsonProperty("revised_prompt")]
        public string RevisedPrompt { get; set; }
    }

}
