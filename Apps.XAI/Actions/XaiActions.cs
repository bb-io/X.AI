using Apps.XAI.Models.Request;
using Apps.XAI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.XAI.Actions;

[ActionList]
public class XaiActions : BaseActions
{
    public XaiActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext, fileManagementClient)
    {
    }


    [Action("Chat completion", Description = "Generate a chat completion based on a conversation")]
    public async Task<ChatResponse> CreateChatCompletion(
        [ActionParameter][Display("Message", Description = "The message to begin the conversation with")] string input,
        [ActionParameter] ChatCompletionRequest session)
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

        var response = await Client.ExecuteCompletionRequest(request);

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