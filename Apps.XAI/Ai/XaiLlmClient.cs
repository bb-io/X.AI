using Apps.XAI.Api;
using Apps.XAI.Models.Response;
using Apps.XAI.Utils;
using Blackbird.Ai.Llm.Abstract;
using Blackbird.Ai.Llm.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XAI.Ai;

public sealed class XaiLlmClient(XaiRestClient client, string model, XaiLlmSettings settings) : ILlmClient
{
    private static readonly JsonSerializerSettings BodySettings = new() { NullValueHandling = NullValueHandling.Ignore };

    public LlmCapabilities Capabilities { get; } = CapabilitiesFor(model, settings.MaxTokens);

    public async Task<LlmResponse> CompleteAsync(LlmRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var body = JsonConvert.SerializeObject(new
        {
            model,
            messages = new[]
            {
                new { role = "system", content = request.SystemPrompt },
                new { role = "user", content = request.UserPrompt }
            },
            max_tokens = Capabilities.MaxOutputTokens,
            temperature = settings.Temperature,
            top_p = settings.TopP,
            reasoning_effort = settings.ReasoningEffort
        }, BodySettings);

        var response = await client.ExecuteWithErrorHandling<CompletionResponse>(
            new RestRequest("/chat/completions", Method.Post).AddJsonBody(body));

        var choice = response.Choices?.FirstOrDefault();

        return new LlmResponse(
            choice?.Message?.Content ?? string.Empty,
            ToUsage(response.Usage),
            choice?.FinishReason == "length" ? FinishReason.Truncated : FinishReason.Stop);
    }

    private static LlmUsage ToUsage(Usage? usage) => usage is null
        ? new LlmUsage(0, 0)
        : new LlmUsage(usage.PromptTokens, Math.Max(usage.CompletionTokens, usage.TotalTokens - usage.PromptTokens));

    private static LlmCapabilities CapabilitiesFor(string model, int? requestedMaxTokens)
    {
        var limits = ModelTokenService.For(model);

        return new LlmCapabilities(
            SupportsStructuredOutput: false,
            ContextTokens: limits.ContextTokens,
            MaxOutputTokens: requestedMaxTokens is > 0 ? Math.Min(requestedMaxTokens.Value, limits.MaxOutputTokens) : limits.MaxOutputTokens);
    }
}
