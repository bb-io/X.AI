using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Response
{
    public class ModelsResponse
    {
        public List<ModelDto> Data { get; set; }
    }

    public class ModelDto
    {
        public string Id { get; set; }
        public long Created { get; set; }
        public string Object { get; set; }
        [JsonProperty("owned_by")]
        public string OwnedBy { get; set; }
    }
}
