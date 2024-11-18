using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.DataSourceHandlers
{
    public class ModelDataSourceHandler : IStaticDataSourceItemHandler
    {      
        IEnumerable<DataSourceItem> IStaticDataSourceItemHandler.GetData()
        {
            return new List<DataSourceItem>
            {
                new DataSourceItem("grok-beta", "Grok Beta")
            };
        }
    }
}
