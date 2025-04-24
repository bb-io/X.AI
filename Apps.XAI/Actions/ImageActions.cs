using Apps.XAI.Api;
using Apps.XAI.Models.Request;
using Apps.XAI.Models.Response;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Xml.Serialization;

namespace Apps.XAI.Actions
{
    [ActionList]
    public class ImageActions : BaseActions
    {
        public ImageActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext, fileManagementClient)
        {
        }

        [Action("Generate image", Description = "Generates an image based on a prompt")]
        public async Task<ImageResponse> GenerateImage([ActionParameter] ImageGenerationRequest input)
        {
            var model = input.Model ?? "grok-2-image-1212";
            var request = new RestRequest("/images/generations", Method.Post);

            request.AddJsonBody(new
            {
                model,
                prompt = input.Prompt,
                response_format = "b64_json",
                n = input.N ?? 1,
            });

            var response = await Client.ExecuteWithErrorHandling<ImageGenerationResponse>(request);
            var bytes = Convert.FromBase64String(response.Data.First().B64Json);

            using var stream = new MemoryStream(bytes);
            var filename = ("generated_image") + ".png";
            var file = await FileManagementClient.UploadAsync(stream, "image/png", filename);
            return new() { Image = file };
        }
    }
}
