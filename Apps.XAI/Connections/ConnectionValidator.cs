using Apps.XAI.Api;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.XAI.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = new XaiRestClient(authenticationCredentialsProviders);
            await client.ExecuteWithErrorHandling(new RestRequest("/api-key", Method.Get));
            return new ConnectionValidationResponse { IsValid = true };
        }
        catch (Exception ex)
        {
            return new ConnectionValidationResponse { IsValid = false, Message = ex.Message };
        }
    }
}