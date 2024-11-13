using Apps.X.AI.Api;
using Apps.X.AI.Models.Request;
using Apps.X.AI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.X.AI.Actions;

[ActionList]
public class XaiActions : BaseInvocable
{
    private readonly XaiRestClient _client;
    public XaiActions(InvocationContext invocationContext) : base(invocationContext)
    {
        _client = new XaiRestClient(invocationContext.AuthenticationCredentialsProviders);
    }

    [Action("Get info about API Key", Description = "Get information about your API key")]
    public async Task<string> GetApiKeyInfo()
    {
        var request = new RestRequest("/api-key", Method.Get);
        var response = await _client.ExecuteWithErrorHandling(request);

        return response.Content ?? "No info in content";
    }



    [Action("Get Model", Description = "Retrieve information about a specific model")]
    public async Task<string> GetModel([ActionParameter] string modelId)
    {
        var request = new RestRequest($"/models/{modelId}", Method.Get);

        var response = await _client.ExecuteWithErrorHandling(request);

        return response.Content ?? "No info in content";
    }


    [Action("Get models list", Description = "Retrieve the list of available models")]
    public async Task<List<ModelDto>> GetModelsList()
    {
        var request = new RestRequest("/models", Method.Get);

        var response = await _client.ExecuteWithErrorHandling(request);

        if (!response.IsSuccessful)
        {
            throw new Exception($"Failed to retrieve models list: {response.Content}");
        }

        var modelsResponse = JsonConvert.DeserializeObject<ModelsResponse>(response.Content);
        return modelsResponse?.Data ?? new List<ModelDto>();
    }

    [Action("Create completion", Description = "Generate a completion based on a prompt")]
    public async Task<string> CreateCompletion([ActionParameter] CompletionRequest input)
    {
        var request = new RestRequest("/completions", Method.Post);

        request.AddJsonBody(new
        {
            model = "grok-beta", //Can be modified if required. Just one model present
            prompt = input.Prompt,
            max_tokens = input.MaxTokens ?? 4096,
            best_of = input.BestOf,
            echo = input.Echo,
            frequency_penalty = input.FrequencyPenalty,
            logit_bias = input.LogitBias,
            logprobs = input.Logprobs,
            n = input.N ?? 1,
            presence_penalty = input.PresencePenalty,
            seed = input.Seed,
            stop = input.Stop,
            stream = input.Stream,
            stream_options = input.StreamOptions,
            suffix = input.Suffix,
            temperature = input.Temperature ?? 1.0,
            top_p = input.TopP ?? 1.0,
            user = input.User
        });


        var response = await _client.ExecuteAsync<CompletionResponse>(request);

        if (!response.IsSuccessful || response.Data == null)
        {
            throw new Exception($"Request failed with status: {response.StatusCode}, content: {response.Content}");
        }
        return response.Data.Choices.FirstOrDefault()?.Text ?? "No completion text found.";
    }

}