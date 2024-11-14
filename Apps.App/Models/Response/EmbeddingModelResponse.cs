using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Response
{
    public class EmbeddingModelResponse
    {
        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("input_modalities")]
        public List<string> InputModalities { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("owned_by")]
        public string OwnedBy { get; set; }

        [JsonProperty("prompt_image_token_price")]
        public double PromptImageTokenPrice { get; set; }

        [JsonProperty("prompt_text_token_price")]
        public double PromptTextTokenPrice { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}
