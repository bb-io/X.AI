using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Response
{
    public class LanguageModelsResponse
    {
        public List<LanguageModelDto> Models { get; set; }
    }

    public class LanguageModelDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
