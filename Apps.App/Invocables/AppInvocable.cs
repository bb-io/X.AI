//using Apps.App.Api;
//using Apps.App.Constants;
//using Blackbird.Applications.Sdk.Common;
//using Blackbird.Applications.Sdk.Common.Authentication;
//using Blackbird.Applications.Sdk.Common.Invocation;

//namespace Apps.App.Invocables;

//public class AppInvocable : BaseInvocable
//{
//    protected AuthenticationCredentialsProvider[] Creds =>
//        InvocationContext.AuthenticationCredentialsProviders.ToArray();

//    protected AppClient Client { get; }
//    public AppInvocable(InvocationContext invocationContext) : base(invocationContext)
//    {
//        var apiKey = Creds.FirstOrDefault(c=>c.KeyName == CredsNames.ApiKey)?.Value;
//        Client = new AppClient();
//    }
//}