using Apps.XAI.Api;
using Apps.XAI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XAI.DataSourceHandlers
{
    public class ModelDataSourceHandler : BaseInvocable, IAsyncDataSourceHandler
    {
        private readonly XaiRestClient Client;

        public ModelDataSourceHandler(InvocationContext invocationContext)
            : base(invocationContext)
        {
            Client = new XaiRestClient(invocationContext.AuthenticationCredentialsProviders);
        }
        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context,
             CancellationToken cancellationToken)
        {
            var request = new RestRequest("/language-models", Method.Get);
            var response = await Client.ExecuteWithErrorHandling<ModelsResponse>(request);
            return response.Models.ToDictionary(m => m.Id, m => m.Id);
        }
    }
}
