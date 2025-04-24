using Apps.XAI.Actions;
using Apps.XAI.Models.Request;
using Newtonsoft.Json;
using Tests.XAI.Base;

namespace Tests.XAI
{
    [TestClass]
    public class ChatActionTests :TestBase
    {
        [TestMethod]
        public async Task ChatCompletions_IsSuccess()
        {
            var action = new XaiActions(InvocationContext, FileManager);

            var request = new ChatCompletionRequest
            {
                Model = "grok-2-1212",
                Messages = new List<string> { "Hello, how are you?" },
                MaxTokens = 50,
                Temperature = 0.7,
                TopP = 1.0,
                PresencePenalty = 0,
                FrequencyPenalty = 0,
                Stop = null
            };

            var response = await action.CreateChatCompletion("Hello how are", request);
            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
            Assert.IsNotNull(request.Messages);
        }
    }
}
