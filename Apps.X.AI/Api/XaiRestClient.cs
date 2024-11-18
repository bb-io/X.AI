using Apps.X.AI.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.X.AI.Api
{
    public class XaiRestClient : BlackBirdRestClient
    {
        public XaiRestClient(IEnumerable<AuthenticationCredentialsProvider> authProviders)
            : base(new RestClientOptions { ThrowOnAnyError = false, BaseUrl = new Uri("https://api.x.ai/v1") })
        {
            this.AddDefaultHeader("Authorization", $"Bearer {authProviders.First(x => x.KeyName == "apiKey").Value}");
        }

        protected override Exception ConfigureErrorException(RestResponse response)
        {
            try
            {
                var json = response.Content;
                var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                return new(error.ToString());
            }
            catch
            {
                return new Exception($"Failed to parse response. Content: {response.Content}");
            }
        }

        public async Task<CompletionResponse> ExecuteCompletionRequest(RestRequest request)
        {
            CompletionResponse? response = null;
            try
            {
                response = await ExecuteWithErrorHandling<CompletionResponse>(request);
            }
            catch (Exception)
            {
                throw new PluginApplicationException("The xAI API is not responding as expected.");
            }

            if (response?.Choices.FirstOrDefault()?.Message?.Content == null)
            {
                throw new PluginApplicationException("The xAI API is not responding as expected.");
            }

            return response;
        }
    }
}
