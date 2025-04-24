using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XAI.DataSourceHandlers.StaticHandler
{
    public class EncodingFormatDataSourceHandler : IStaticDataSourceItemHandler
    {
        IEnumerable<DataSourceItem> IStaticDataSourceItemHandler.GetData()
        {
            return new List<DataSourceItem>
            {
                new DataSourceItem("float", "Float"),
                new DataSourceItem("base64", "Base64")
            };
        }
    }
}
