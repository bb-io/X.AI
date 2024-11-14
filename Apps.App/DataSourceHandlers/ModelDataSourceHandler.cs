using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.DataSourceHandlers
{
    public class ModelDataSourceHandler : IStaticDataSourceHandler
    {
        public Dictionary<string, string> GetData()
            => new()
            {
                { "grok-beta", "Grok Beta" },               
            };
    }
}
