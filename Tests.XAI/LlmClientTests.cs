using Apps.XAI.Ai;
using Apps.XAI.Api;
using Blackbird.Ai.Llm.Models;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Tests.XAI.Base;

namespace Tests.XAI
{
    [TestClass]
    public class LlmClientTests : TestBase
    {
        [TestMethod]
        public async Task CompleteAsync_OutputLimitReached_ReportsTruncated()
        {
            var client = new XaiLlmClient(new XaiRestClient(Creds), ChatModel, new(MaxTokens: 5));

            var response = await client.CompleteAsync(
                new LlmRequest("You are terse.", "Count from 1 to 50, separated by spaces."),
                CancellationToken.None);

            Print(response);
            Assert.AreEqual(FinishReason.Truncated, response.Finish);
            Assert.AreEqual(5, client.Capabilities.MaxOutputTokens);
            Assert.IsTrue(response.Usage.InputTokens > 0 && response.Usage.OutputTokens > 0);
        }

        [TestMethod]
        public async Task CompleteAsync_ReasoningEffort_IsSentToProvider()
        {
            var client = new XaiLlmClient(new XaiRestClient(Creds), "grok-4.20-0309-non-reasoning", new(ReasoningEffort: "low"));

            var exception = await Assert.ThrowsExceptionAsync<PluginApplicationException>(() =>
                client.CompleteAsync(new LlmRequest("You are terse.", "Say hi."), CancellationToken.None));

            Console.WriteLine(exception.Message);
            StringAssert.Contains(exception.Message, "reasoningEffort");
        }
    }
}
