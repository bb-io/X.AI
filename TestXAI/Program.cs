using Apps.App.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using System;
using System.Threading.Tasks;

namespace AppRunner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var actions = new Actions(new InvocationContext());
            await TestApiKeyInfo(actions);
        }

        static async Task TestApiKeyInfo(Actions actions)
        {
            try
            {
                var response = await actions.GetApiKeyInfo();
                Console.WriteLine("API Key Info Response:");
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
