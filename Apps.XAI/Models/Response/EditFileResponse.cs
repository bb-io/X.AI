using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Blueprints.Interfaces.Edit;

namespace Apps.XAI.Models.Response
{
    public class EditFileResponse : IEditFileOutput
    {
        public FileReference File { get; set; } = new();

        [Display("Usage")]
        public Usage Usage { get; set; } = new();

        [Display("Total segments reviewed")]
        public int TotalSegmentsReviewed { get; set; }

        [Display("Total segments updated")]
        public int TotalSegmentsUpdated { get; set; }
    }
}
