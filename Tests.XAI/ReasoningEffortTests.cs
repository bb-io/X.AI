using Apps.XAI.Actions;
using Apps.XAI.Models.Request;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Common.Files;
using Tests.XAI.Base;

namespace Tests.XAI
{
    [TestClass]
    public class ReasoningEffortTests : TestBase
    {
        [TestMethod]
        public async Task TranslateText_SupportedReasoningEffort_IsSuccess()
        {
            var response = await new TranslationActions(InvocationContext, FileManager).TranslateText(new TranslateTextRequest
            {
                Model = ChatModel,
                ReasoningEffort = "none",
                Text = "Good morning!",
                SourceLanguage = "en",
                TargetLanguage = "es"
            });

            Print(response);
            StringAssert.Contains(response.TranslatedText, "Buenos días");
            AssertNoWarningsLogged();
        }

        [TestMethod]
        public async Task ReviewText_SupportedReasoningEffortOnAnotherModel_IsSuccess()
        {
            var response = await new ReviewActions(InvocationContext, FileManager).ReviewText(new ReviewTextRequest
            {
                Model = "grok-4.7",
                ReasoningEffort = "low",
                SourceText = "Please restart the application.",
                TargetText = "Riavvia l'applicazione.",
                SourceLanguage = "en",
                TargetLanguage = "it"
            });

            Print(response);
            Assert.IsTrue(response.Score is >= 0 and <= 1);
            AssertNoWarningsLogged();
        }

        [TestMethod]
        public async Task TranslateFile_ModelWithoutReasoningEffort_ThrowsBeforeDownload()
        {
            var exception = await Assert.ThrowsExceptionAsync<PluginMisconfigurationException>(() =>
                new TranslationActions(InvocationContext, InMemoryFiles).TranslateFile(new TranslateContentRequest
                {
                    File = new FileReference { Name = "never-downloaded.xlf" },
                    Model = "grok-4.20-0309-non-reasoning",
                    ReasoningEffort = "low",
                    TargetLanguage = "de"
                }));

            Console.WriteLine(exception.Message);
            StringAssert.Contains(exception.Message, "does not support a reasoning effort");
        }

        [TestMethod]
        public async Task ShortenText_UnsupportedReasoningEffortValue_ListsSupportedValues()
        {
            var exception = await Assert.ThrowsExceptionAsync<PluginMisconfigurationException>(() =>
                new ShortenActions(InvocationContext, FileManager).ShortenText(new ShortenTextRequest
                {
                    Model = "grok-4.7",
                    ReasoningEffort = "none",
                    Text = "A text that is long enough.",
                    MaximumLength = 10
                }));

            Console.WriteLine(exception.Message);
            StringAssert.Contains(exception.Message, "low, medium, high, xhigh");
        }
    }
}
