using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.X.AI.DataSourceHandlers
{
    public class TemperatureDataSourceHandler : IStaticDataSourceItemHandler
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
                new DataSourceItem("1.1", "1.1"),
                new DataSourceItem("1.2", "1.2"),
                new DataSourceItem("1.3", "1.3"),
                new DataSourceItem("1.4", "1.4"),
                new DataSourceItem("1.5", "1.5"),
                new DataSourceItem("1.6", "1.6"),
                new DataSourceItem("1.7", "1.7"),
                new DataSourceItem("1.8", "1.8"),
                new DataSourceItem("1.9", "1.9"),
                new DataSourceItem("2.0", "2.0")

            };
        }
    };
}

