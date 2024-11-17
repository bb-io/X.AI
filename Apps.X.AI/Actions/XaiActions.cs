using Apps.X.AI.Api;
using Apps.X.AI.Models.Request;
using Apps.X.AI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
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


    [Action("Create completion", Description = "Generate a completion based on a prompt")]
    public async Task<ResponseMessage> CreateCompletion([ActionParameter] CompletionRequest input)
    {
        var request = new RestRequest("/completions", Method.Post);

        request.AddJsonBody(new
        {
            model = input.Model,
            prompt = input.Prompt,
            max_tokens = input.MaxTokens ?? 4096,
            stop = input.Stop,
            temperature = input.Temperature ?? 1.0,
            top_p = input.TopP ?? 1.0,
        });

        var response = await _client.ExecuteWithErrorHandling<CompletionResponse>(request);

        var messageContent = response.Choices?.FirstOrDefault()?.Text
                             ?? "No response generated";

        return new ResponseMessage { Text = messageContent };
    }


    [Action("Create chat completion", Description = "Send a chat completion request")]
    public async Task<ChatCompletionResponse> CreateChatCompletion([ActionParameter] ChatCompletionRequest input)
    {
        var request = new RestRequest("/chat/completions", Method.Post);

        request.AddJsonBody(new
        {
            model = input.Model,
            messages = input.Messages,
            max_tokens = input.MaxTokens ?? 1024,
            stop = input.Stop,
            temperature = input.Temperature ?? 1.0,
            top_p = input.TopP ?? 1.0,
            user = input.User
        });

        var response = await _client.ExecuteWithErrorHandling<ChatCompletionResponse>(request);
        return response;
    }


    [Action("Create embeddings", Description = "Create an embedding vector for the given input text")]
    public async Task<EmbeddingResponse> CreateEmbeddings([ActionParameter] CreateEmbeddingRequest input)
    {
        var request = new RestRequest("/embeddings", Method.Post);

        request.AddJsonBody(new
        {
            input = input.Input,
            model = input.Model,
            dimensions = input.Dimensions ?? 1,
            encoding_format = input.EncodingFormat,
            user = input.User
        });

        var response = await _client.ExecuteWithErrorHandling<EmbeddingResponse>(request);

        return response;
    }

}