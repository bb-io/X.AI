using Apps.XAI.Api;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;


namespace Apps.XAI.Actions
{
    public abstract class BaseActions : BaseInvocable
    {
        protected readonly XaiRestClient Client;
        protected readonly IFileManagementClient FileManagementClient;
        protected BaseActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
        {
            Client = new XaiRestClient(invocationContext.AuthenticationCredentialsProviders);
            FileManagementClient = fileManagementClient;
        }
    }
}
