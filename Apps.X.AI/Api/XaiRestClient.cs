using Apps.X.AI.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
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
    }
}
