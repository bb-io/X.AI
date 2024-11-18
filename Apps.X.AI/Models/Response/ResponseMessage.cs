using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Response
{
    public class ResponseMessage
    {
        [Display("Text", Description = "Generated text from X.AI service, according to the prompt")]
        public string Text { get; set; }

        [Display("Usage", Description = "Description of the usage")]
        public Usage Usage {  get; set; }
    }
}
