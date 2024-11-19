using Apps.XAI.Api;
using Apps.XAI.Models.Request;
using Apps.XAI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XAI.Actions;

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
            top_p = input.TopP ?? 1.0
        });

        var response = await _client.ExecuteCompletionRequest(request);

        return new ResponseMessage
        {
            Text = response.Choices.FirstOrDefault()?.Text ?? string.Empty,
            Usage = response.Usage ?? new Usage
            {
                PromptTokens = 0,
                CompletionTokens = 0,
                TotalTokens = 0
            }
        };
    }


    [Action("Chat completion", Description = "Generate a chat completion based on a conversation")]
    public async Task<ChatResponse> CreateChatCompletion([ActionParameter][Display("Message", Description = "The message to begin the conversation with")] string input, [ActionParameter] ChatCompletionRequest session)
    {
        if (session.Messages == null)
        {
            session.Messages = new List<string>();
        }
        session.Messages.Add(input);

        var jsonBody = new
        {
            model = session.Model,
            messages = session.Messages.Select(content => new { role = "user", content }).ToList(),
            max_tokens = session.MaxTokens ?? 150,
            temperature = session.Temperature ?? 0.7,
            top_p = session.TopP ?? 1.0,
            presence_penalty = session.PresencePenalty ?? 0,
            frequency_penalty = session.FrequencyPenalty ?? 0,
            stop = session.Stop
        };

        var request = new RestRequest("/chat/completions", Method.Post);
        request.AddJsonBody(jsonBody);

        var response = await _client.ExecuteCompletionRequest(request);

        return new ChatResponse
        {
            Model = session.Model,
            Message = response.Choices.FirstOrDefault()?.Message?.Content ?? string.Empty,
            Usage = response.Usage ?? new Usage
            {
                PromptTokens = 0,
                CompletionTokens = 0,
                TotalTokens = 0
            }
        };
    }

}