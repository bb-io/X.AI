using Apps.X.AI.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
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
        [Display("Input", Description = "This parameter takes the text to create the embeddings")]
        public string Input { get; set; }

        [Display("Model", Description = "This parameter controls which version of X.AI answers your request")]
        [StaticDataSource(typeof(ModelDataSourceHandler))]
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("dimensions")]
        [Display("Dimensions", Description = "The number of dimensions the resulting output embeddings should have.")]
        public int? Dimensions { get; set; }

        [StaticDataSource(typeof(EncodingFormatDataSourceHandler))]
        [JsonProperty("encoding_format")]
        [Display("Encoding format", Description = "Select the encoding format: 'float' or 'base64'")]
        public string EncodingFormat { get; set; }

        [JsonProperty("user")]
        public string? User { get; set; }
    }
}
