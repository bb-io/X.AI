using Apps.XAI.Ai;
using Apps.XAI.Api;
using Apps.XAI.Models.Response;
using Blackbird.Ai.Batching.Models;
using Blackbird.Ai.Content.Models;
using Blackbird.Ai.Files;
using Blackbird.Ai.Files.Models;
using Blackbird.Ai.Llm.Abstract;
using Blackbird.Ai.Llm.Models;
using Blackbird.Ai.Prompts.Models;
using Blackbird.Ai.Text;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.XAI.Actions
{
    public abstract class BaseActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
        : BaseInvocable(invocationContext)
    {
        protected XaiRestClient Client { get; } = new(invocationContext.AuthenticationCredentialsProviders);

        protected IFileManagementClient FileManagementClient { get; } = fileManagementClient;

        protected virtual ILlmClient CreateLlmClient(string model, XaiLlmSettings settings) =>
            new XaiLlmClient(Client, model, settings);

        protected async Task<ILlmClient> CreateLlmClientAsync(string model, XaiLlmSettings settings)
        {
            if (!string.IsNullOrWhiteSpace(settings.ReasoningEffort))
                await ThrowIfReasoningEffortUnsupportedAsync(model, settings.ReasoningEffort);

            return CreateLlmClient(model, settings);
        }

        protected ContentStreamProcessor CreateContentProcessor(ILlmClient client, string model, int? bucketSize = null) =>
            new(client, CreateEngineOptions(model, bucketSize));

        protected TextProcessor CreateTextProcessor(ILlmClient client, string model) =>
            new(client, CreateEngineOptions(model));

        protected async Task<Stream?> DownloadOptionalAsync(FileReference? file) =>
            file is null ? null : await FileManagementClient.DownloadAsync(file);

        protected async Task<FileReference> UploadAsync(ContentStream file)
        {
            await using var content = file.Content;
            return await FileManagementClient.UploadAsync(content, file.MediaType, file.FileName);
        }

        protected static Usage ToUsage(LlmUsage usage) => new()
        {
            PromptTokens = usage.InputTokens,
            CompletionTokens = usage.OutputTokens,
            TotalTokens = usage.TotalTokens
        };

        protected static string UserPrompt(ContentResult content) =>
            string.Join(Environment.NewLine, content.Traces.Select(trace => trace.UserPrompt));

        protected static string? Instructions(string? targetAudience, string? additionalInstructions)
        {
            var audience = string.IsNullOrWhiteSpace(targetAudience)
                ? null
                : $"The target audience is {targetAudience.Trim()}.";

            var instructions = new[] { audience, additionalInstructions }
                .Where(part => !string.IsNullOrWhiteSpace(part))
                .ToList();

            return instructions.Count > 0 ? string.Join(Environment.NewLine, instructions) : null;
        }

        protected static void ThrowIfUnresolved(ContentResult content)
        {
            if (content.UnresolvedSegments > 0)
            {
                throw new PluginApplicationException(string.Join(Environment.NewLine, content.Warnings));
            }
        }

        private async Task ThrowIfReasoningEffortUnsupportedAsync(string model, string reasoningEffort)
        {
            var models = await Client.ExecuteWithErrorHandling<ModelsResponse>(new RestRequest("/language-models", Method.Get));
            var supported = models.Models
                .FirstOrDefault(entry => entry.Id == model || entry.Aliases.Contains(model))?
                .Capabilities?.ReasoningEffort ?? [];

            if (!supported.Contains(reasoningEffort))
                throw new PluginMisconfigurationException(supported.Count == 0
                    ? $"The model {model} does not support a reasoning effort. Leave the input empty or choose another model."
                    : $"The model {model} does not support the reasoning effort '{reasoningEffort}'. Supported values: {string.Join(", ", supported)}.");
        }

        private EngineOptions CreateEngineOptions(string model, int? bucketSize = null) => new()
        {
            Logger = AiAdapters.Logger(InvocationContext),
            LogTag = "[xAI]",
            ProvenanceTool = $"xAI/{model}",
            Bucketing = BucketingOptions.FromInputs(bucketSize, null),
            TraceLevel = PromptTraceLevel.Full
        };
    }
}
