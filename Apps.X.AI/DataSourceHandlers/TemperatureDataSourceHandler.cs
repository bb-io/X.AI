using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.X.AI.Extentions;

namespace Apps.X.AI.DataSourceHandlers
{
    public class TemperatureDataSourceHandler : BaseInvocable, IDataSourceHandler
    {
        public TemperatureDataSourceHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public Dictionary<string, string> GetData(DataSourceContext context)
        {
            return DataSourceHandlersExtensions.GenerateFormattedFloatArray(0.0f, 1.0f, 0.1f)
                .Where(t => context.SearchString == null || t.Contains(context.SearchString))
                .ToDictionary(t => t, t => t);
        }
    }
}
