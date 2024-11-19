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
        var client = new XaiRestClient(authenticationCredentialsProviders);
        var request = new RestRequest("/api-key", Method.Get);

        try
        {
            await client.ExecuteWithErrorHandling(request);
            return new ConnectionValidationResponse { IsValid = true };
        }
        catch (Exception ex)
        {
            return new ConnectionValidationResponse { IsValid = false, Message = ex.Message };
        }
    }
}