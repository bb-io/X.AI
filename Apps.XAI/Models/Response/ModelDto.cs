using Newtonsoft.Json;

namespace Apps.XAI.Models.Response
{
    public class ModelDto
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("aliases")]
        public List<string> Aliases { get; set; } = [];

        [JsonProperty("capabilities")]
        public ModelCapabilitiesDto? Capabilities { get; set; }
    }

    public class ModelCapabilitiesDto
    {
        [JsonProperty("reasoning_effort")]
        public List<string> ReasoningEffort { get; set; } = [];
    }

    public class ModelsResponse
    {
        [JsonProperty("models")]
        public List<ModelDto> Models { get; set; } = [];
    }
}
