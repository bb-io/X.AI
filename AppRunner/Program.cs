using Apps.X.AI.Actions;
using Apps.X.AI.Models.Request;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace AppRunner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var apiKeyProvider = new AuthenticationCredentialsProvider(
                AuthenticationCredentialsRequestLocation.None,
                "apiKey",
                "___"
            );

            var invocationContext = new InvocationContext
            {
                AuthenticationCredentialsProviders = new List<AuthenticationCredentialsProvider> { apiKeyProvider }
            };

            var actions = new XaiActions(invocationContext);

            //Get api info
            var apiKeyInfo = await actions.GetApiKeyInfo();
            Console.WriteLine("API Key Info:");
            Console.WriteLine(apiKeyInfo);
            Console.WriteLine();

            //Get Model method
            var modelId = "grok-beta";
            var modelInfo = await actions.GetModel(modelId);
            Console.WriteLine("Model Info:");
            Console.WriteLine(modelInfo);
            Console.WriteLine();

            //Get list of models
            var models = await actions.GetModelsList();
            Console.WriteLine("Available Models:");
            foreach (var model in models)
            {
                Console.WriteLine($"ID: {model.Id}, Created: {model.Created}, Object: {model.Object}, OwnedBy: {model.OwnedBy}");
            }
            Console.WriteLine();



            //Create completion
            var request = new CompletionRequest
            {
                Model = "grok-beta",
                Prompt = "Corruption",
                MaxTokens = 50
            };

            try
            {
                var result = await actions.CreateCompletion(request);
                Console.WriteLine("Completion result:");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during completion:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
