using Apps.X.AI.Extentions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.X.AI.DataSourceHandlers
{
    public class TopPDataSourceHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            return new List<DataSourceItem>
            {
                new DataSourceItem("0.0", "0.0"),
                new DataSourceItem("0.1", "0.1"),
                new DataSourceItem("0.2", "0.2"),
                new DataSourceItem("0.3", "0.3"),
                new DataSourceItem("0.4", "0.4"),
                new DataSourceItem("0.5", "0.5"),
                new DataSourceItem("0.6", "0.6"),
                new DataSourceItem("0.7", "0.7"),
                new DataSourceItem("0.8", "0.8"),
                new DataSourceItem("0.9", "0.9"),
                new DataSourceItem("1.0", "1.0"),                
            };
        }
    }
}
