using Apps.X.AI.Models.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Response
{
    public class ChatCompletionResponse
    {
        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("system_fingerprint")]
        public string SystemFingerprint { get; set; }

        [JsonProperty("usage")]
        public Usage? Usage { get; set; }
    }
}

