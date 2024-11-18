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


    [Action("Chat completion", Description = "Generate a chat completion based on a conversation")]
    public async Task<ChatResponse> CreateChatCompletion([ActionParameter] string input, [ActionParameter] ChatCompletionRequest session)
    {
        session.Messages ??= new List<Message>();

        session.Messages.Add(new Message
        {
            Role = "user",
            Content = input
        });

        var messages = session.Messages.Select(message => new
        {
            role = message.Role,
            content = message.Content
        }).ToList();

        var jsonBody = new
        {
            model = session.Model,
            messages = messages,
            max_tokens = session.MaxTokens ?? 150,
            temperature = session.Temperature ?? 0.7,
            top_p = session.TopP ?? 1.0,
            presence_penalty = session.PresencePenalty ?? 0,
            frequency_penalty = session.FrequencyPenalty ?? 0,
            stop = session.Stop
        };

        var request = new RestRequest("/chat/completions", Method.Post);
        request.AddJsonBody(jsonBody);

        var response = await _client.ExecuteWithErrorHandling<CompletionResponse>(request);

        if (response.Choices == null || !response.Choices.Any())
        {
            throw new Exception("No choices were returned from the API.");
        }

        var firstChoice = response.Choices.First();

        session.Messages.Add(new Message
        {
            Role = "assistant",
            Content = firstChoice.Message?.Content ?? "No content generated"
        });

        return new ChatResponse
        {
            Model = session.Model,
            Choices = response.Choices.Select(choice => new Choice
            {
                Index = choice.Index,
                Message = new Message
                {
                    Role = "assistant",
                    Content = choice.Message?.Content ?? "No content generated",
                    Refusal = null
                },
                FinishReason = choice.FinishReason
            }).ToList(),
            Usage = response.Usage
        };
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