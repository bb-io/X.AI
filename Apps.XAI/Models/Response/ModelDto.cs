using System.Text.Json.Serialization;

namespace Apps.XAI.Models.Response
{
    public class ModelDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("owned_by")]
        public string OwnedBy { get; set; }
    }
    public class ModelsListResponse
    {
        [JsonPropertyName("data")]
        public List<ModelDto> Data { get; set; }
    }
}
