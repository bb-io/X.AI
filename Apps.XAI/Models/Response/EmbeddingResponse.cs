using Newtonsoft.Json;

namespace Apps.XAI.Models.Response
{
    public class EmbeddingResponse
    {
        [JsonProperty("data")]
        public List<float> Data { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }


    }
}
