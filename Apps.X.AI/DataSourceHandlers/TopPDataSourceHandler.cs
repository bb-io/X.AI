﻿using Apps.X.AI.Extentions;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.DataSourceHandlers
{
    public class TopPDataSourceHandler : BaseInvocable, IDataSourceHandler
    {
        public TopPDataSourceHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public Dictionary<string, string> GetData(DataSourceContext context)
        { 
            return DataSourceHandlersExtensions.GenerateFormattedFloatArray(0.0f, 1.0f, 0.1f)
                .Where(p => context.SearchString == null || p.Contains(context.SearchString))
                .ToDictionary(p => p, p => p);
        }
    }
}
