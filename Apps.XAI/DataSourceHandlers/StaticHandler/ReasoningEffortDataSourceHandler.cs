using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XAI.DataSourceHandlers.StaticHandler
{
    public class ReasoningEffortDataSourceHandler : IStaticDataSourceItemHandler
    {
        public IEnumerable<DataSourceItem> GetData()
        {
            return new List<DataSourceItem>
            {
                new DataSourceItem("none", "None"),
                new DataSourceItem("low", "Low"),
                new DataSourceItem("medium", "Medium"),
                new DataSourceItem("high", "High"),
                new DataSourceItem("xhigh", "Extra high")
            };
        }
    }
}
