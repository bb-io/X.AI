using Apps.XAI.Models.Request;
using Apps.XAI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using RestSharp;
using Apps.XAI.Utils;

namespace Apps.XAI.Actions;

[ActionList("Chat completions")]
public class XaiActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : BaseActions(invocationContext, fileManagementClient)
{
    private const int MaxCompletionRetries = 3;


    [Action("Chat completion", Description = "Generate a chat completion based on a conversation")]
    public async Task<ChatResponse> CreateChatCompletion(
        [ActionParameter][Display("Message", Description = "The message to begin the conversation with")] string input,
        [ActionParameter] ChatCompletionRequest session)
    {
        var messages = new List<object>();

        if (!string.IsNullOrEmpty(session.SystemPrompt))
        {
            messages.Add(new { role = "system", content = session.SystemPrompt });
        }
        if (session.Messages != null)
        {
            messages.AddRange(session.Messages.Select(m => new { role = "user", content = m }));
        }

        if (session.Parameters != null)
        {
            messages.AddRange(session.Parameters.Select(p => new { role = "user", content = p }));
        }

        messages.Add(new { role = "user", content = input });

        var completeMessage = string.Empty;
        var usage = new Usage { PromptTokens = 0, CompletionTokens = 0, TotalTokens = 0 };
        var counter = 0;

        while (counter < MaxCompletionRetries)
        {
            var response = await ExecuteChatCompletion(messages, session);
            completeMessage += response.Choices.FirstOrDefault()?.Message?.Content ?? string.Empty;

            if (response.Usage != null)
            {
                usage.PromptTokens += response.Usage.PromptTokens;
                usage.CompletionTokens += response.Usage.CompletionTokens;
                usage.TotalTokens += response.Usage.TotalTokens;
            }

            if (response.Choices.FirstOrDefault()?.FinishReason != "length")
            {
                break;
            }

            messages.Add(new { role = "assistant", content = response.Choices.FirstOrDefault()?.Message?.Content });
            messages.Add(new { role = "user", content = "Continue your latest message, it was too long." });
            counter++;
        }

        return new ChatResponse
        {
            Model = session.Model,
            Message = completeMessage,
            SystemPrompt = session.SystemPrompt ?? string.Empty,
            Usage = usage
        };
    }


    private async Task<CompletionResponse> ExecuteChatCompletion(List<object> messages, ChatCompletionRequest session)
    {
        var jsonBody = new
        {
            model = session.Model,
            messages,
            max_tokens = session.MaxTokens ?? ModelTokenService.GetMaxTokensForModel(session.Model),
            temperature = session.Temperature ?? 1.0,
            top_p = session.TopP ?? 1.0,
            presence_penalty = session.PresencePenalty,
            frequency_penalty = session.FrequencyPenalty,
            stop = session.Stop
        };

        var jsonBodySerialized = JsonConvert.SerializeObject(jsonBody, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        });

        var request = new RestRequest("/chat/completions", Method.Post);
        request.AddJsonBody(jsonBodySerialized);

        return await Client.ExecuteCompletionRequest(request);
    }
}