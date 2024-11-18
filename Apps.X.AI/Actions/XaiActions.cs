﻿using Apps.X.AI.Api;
using Apps.X.AI.Models.Request;
using Apps.X.AI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Exceptions;
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
        try
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

            var response = await _client.ExecuteWithErrorHandling<CompletionResponse>(request);

            if (response == null)
            {
                throw new PluginApplicationException("The API response is null.");
            }

            var messageContent = "No response generated";
            if (response.Choices != null && response.Choices.Any())
            {
                messageContent = response.Choices.First().Text;
            }

            var usage = new Usage
            {
                PromptTokens = 0,
                CompletionTokens = 0,
                TotalTokens = 0
            };

            if (response.Usage != null)
            {
                usage = response.Usage;
            }

            return new ResponseMessage
            {
                Text = messageContent,
                Usage = usage
            };
        }
        catch (HttpRequestException ex)
        {
            throw new PluginApplicationException($"Service error occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new PluginApplicationException($"Unexpected error occurred: {ex.Message}");
        }
    }


    [Action("Chat completion", Description = "Generate a chat completion based on a conversation")]
    public async Task<ChatResponse> CreateChatCompletion([ActionParameter][Display("Message", Description = "Input the message to begin the conversation")] string input, [ActionParameter] ChatCompletionRequest session)
    {
        try
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
                temperature = session.Temperature?? 0.7,
                top_p = session.TopP ?? 1.0,
                presence_penalty = session.PresencePenalty ?? 0,
                frequency_penalty = session.FrequencyPenalty ?? 0,
                stop = session.Stop
            };

            var request = new RestRequest("/chat/completions", Method.Post);
            request.AddJsonBody(jsonBody);

            var response = await _client.ExecuteWithErrorHandling<CompletionResponse>(request);

            if (response == null || response.Choices == null || !response.Choices.Any())
            {
                throw new PluginApplicationException("No response received from the API.");
            }

            var responseText = response.Choices.First().Message?.Content ?? "No content generated";

            return new ChatResponse
            {
                Model = session.Model,
                Message = responseText,
                Usage = response.Usage ?? new Usage
                {
                    PromptTokens = 0,
                    CompletionTokens = 0,
                    TotalTokens = 0
                }
            };
        }
        catch (HttpRequestException ex)
        {
            throw new PluginApplicationException($"HTTP error occurred: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new PluginApplicationException($"Unexpected error occurred: {ex.Message}");
        }
    }

}