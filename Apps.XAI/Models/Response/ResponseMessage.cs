using Blackbird.Applications.Sdk.Common;

namespace Apps.XAI.Models.Response
{
    public class ResponseMessage
    {
        [Display("Text", Description = "Generated text from X.AI service, according to the prompt")]
        public string Text { get; set; }

        [Display("Usage", Description = "Description of the usage")]
        public Usage Usage { get; set; }
    }
}
