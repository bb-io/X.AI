using Apps.XAI.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Tests.XAI.Base;

namespace Tests.XAI
{
    [TestClass]
    public class DataSources : TestBase
    {
        [TestMethod]
        public async Task ModelsHandlerReturnsValues()
        {
            var handler = new ModelDataSourceHandler(InvocationContext);

            var response = await handler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

            foreach (var model in response)
            {
                Console.WriteLine($"Key: {model.Key}, Value: {model.Value}");
            }
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task ImageModelsHandlerReturnsValues()
        {
            var handler = new ImageModelDataSourceHandler(InvocationContext);

            var response = await handler.GetDataAsync(new DataSourceContext(), CancellationToken.None);

            foreach (var model in response)
            {
                Console.WriteLine($"Key: {model.Key}, Value: {model.Value}");
            }
            Assert.IsNotNull(response);
        }

    }
}