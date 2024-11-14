using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Request
{
    public class CreateEmbeddingRequest
    {
        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("dimensions")]
        public int? Dimensions { get; set; }

        [JsonProperty("encoding_format")]
        public string EncodingFormat { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }
    }
}
