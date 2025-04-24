using Apps.XAI.Actions;
using Apps.XAI.Models.Request;
using Tests.XAI.Base;

namespace Tests.XAI
{
    [TestClass]
    public class ImageActionTests : TestBase
    {
        [TestMethod]
        public async Task GenerateImage_IsSuccess()
        {
            var action = new ImageActions(InvocationContext, FileManager);
            var request = new ImageGenerationRequest
            {
                Prompt = "A beautiful sunset over the mountains",
                N = 1,
                Model = "grok-2-image-1212"
            };
            var response = await action.GenerateImage(request);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Image);
            Assert.IsTrue(response.Image.Name.EndsWith(".png"));
        }
    }
}
