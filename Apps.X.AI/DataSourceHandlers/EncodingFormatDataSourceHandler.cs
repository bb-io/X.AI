using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.DataSourceHandlers
{
    public class EncodingFormatDataSourceHandler : IStaticDataSourceHandler
    {
        public Dictionary<string, string> GetData()
        {
            return new Dictionary<string, string>
            {
                { "float", "Float" },
                { "base64", "Base64" }
            };
        }
    }
}
