using System.Globalization;
using System.Net;
using Apps.XAI.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Retry;
using RestSharp;

namespace Apps.XAI.Api
{
    public class XaiRestClient : BlackBirdRestClient
    {
        private static readonly ResiliencePipeline<RestResponse> RateLimitPipeline = new ResiliencePipelineBuilder<RestResponse>()
            .AddRetry(new RetryStrategyOptions<RestResponse>
            {
                MaxRetryAttempts = 5,
                Delay = TimeSpan.FromSeconds(2),
                BackoffType = DelayBackoffType.Exponential,
                UseJitter = true,
                ShouldHandle = new PredicateBuilder<RestResponse>()
                    .HandleResult(response => response.StatusCode == HttpStatusCode.TooManyRequests),
                DelayGenerator = args => ValueTask.FromResult(RetryAfter(args.Outcome.Result))
            })
            .Build();

        public XaiRestClient(IEnumerable<AuthenticationCredentialsProvider> authProviders)
            : base(new RestClientOptions { ThrowOnAnyError = false, BaseUrl = new Uri("https://api.x.ai/v1") })
        {
            this.AddDefaultHeader("Authorization", $"Bearer {authProviders.First(x => x.KeyName == "apiKey").Value}");
        }

        protected override Exception ConfigureErrorException(RestResponse response) =>
            new PluginApplicationException(ErrorMessage(response.Content)
                ?? $"Status code: {(int)response.StatusCode}, Content: {response.Content}");

        public override async Task<T> ExecuteWithErrorHandling<T>(RestRequest request)
        {
            string content = (await ExecuteWithErrorHandling(request)).Content;
            T val = JsonConvert.DeserializeObject<T>(content, JsonSettings);
            if (val == null)
            {
                throw new PluginApplicationException($"Could not parse {content} to {typeof(T)}");
            }

            return val;
        }

        public override async Task<RestResponse> ExecuteWithErrorHandling(RestRequest request)
        {
            var restResponse = await RateLimitPipeline.ExecuteAsync(
                async cancellationToken => await ExecuteAsync(request, cancellationToken));

            if (!restResponse.IsSuccessStatusCode)
            {
                throw ConfigureErrorException(restResponse);
            }

            return restResponse;
        }

        public async Task<CompletionResponse> ExecuteCompletionRequest(RestRequest request)
        {
            var response = await ExecuteWithErrorHandling<CompletionResponse>(request);

            if (response.Choices == null || !response.Choices.Any())
            {
                throw new PluginApplicationException("The xAI API is not responding as expected.");
            }

            var chatContent = response.Choices.FirstOrDefault()?.Message?.Content;
            var completionText = response.Choices.FirstOrDefault()?.Text;

            if (string.IsNullOrEmpty(chatContent) && string.IsNullOrEmpty(completionText))
            {
                throw new PluginApplicationException("The xAI API did not return a valid response.");
            }

            return response;
        }

        private static string? ErrorMessage(string? content)
        {
            try
            {
                var error = JObject.Parse(content ?? string.Empty)["error"];
                return error?.Type == JTokenType.Object ? error["message"]?.ToString() : error?.ToString();
            }
            catch (JsonReaderException)
            {
                return null;
            }
        }

        private static TimeSpan? RetryAfter(RestResponse? response) =>
            response?.Headers?.FirstOrDefault(header => string.Equals(header.Name, "Retry-After", StringComparison.OrdinalIgnoreCase))?.Value?.ToString() is { } value
            && double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var seconds)
                ? TimeSpan.FromSeconds(Math.Min(seconds, 60))
                : null;
    }
}
