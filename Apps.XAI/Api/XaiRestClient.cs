﻿using Apps.XAI.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XAI.Api
{
    public class XaiRestClient : BlackBirdRestClient
    {
        public XaiRestClient(IEnumerable<AuthenticationCredentialsProvider> authProviders)
            : base(new RestClientOptions { ThrowOnAnyError = false, BaseUrl = new Uri("https://api.X.AI/v1") })
        {
            this.AddDefaultHeader("Authorization", $"Bearer {authProviders.First(x => x.KeyName == "apiKey").Value}");
        }

        protected override Exception ConfigureErrorException(RestResponse response)
        {
            try
            {
                var json = response.Content;
                var error = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                return new(error.ToString());
            }
            catch
            {
                return new PluginApplicationException($"Failed to parse response. Content: {response.Content}");
            }
        }

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
            RestResponse restResponse = await ExecuteAsync(request);
            if (!restResponse.IsSuccessStatusCode)
            {
                throw ConfigureErrorException(restResponse);
            }

            return restResponse;
        }

        public async Task<CompletionResponse> ExecuteCompletionRequest(RestRequest request)
        {
            CompletionResponse? response = null;
            try
            {
                response = await ExecuteWithErrorHandling<CompletionResponse>(request);
            }
            catch (Exception e)
            {
                throw new PluginApplicationException(e.Message);
            }

            if (response?.Choices == null || !response.Choices.Any())
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
    }
}
