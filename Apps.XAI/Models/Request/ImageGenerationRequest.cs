using Apps.XAI.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XAI.Models.Request
{
    public class ImageGenerationRequest
    {
        public string Prompt { get; set; }

        [Display("Response format")]
        public string? ResponseFormat { get; set; }

        [Display("Number of images to be generated")]
        public int? N { get; set; }

        [Display("Model")]
        [DataSource(typeof(ImageModelDataSourceHandler))]
        public string Model { get; set; }
    }
}
