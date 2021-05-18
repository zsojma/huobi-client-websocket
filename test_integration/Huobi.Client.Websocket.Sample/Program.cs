using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Sample.Examples;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Sample
{
    public static class Program
    {
        private const string SYMBOL = "btcusdt";

        public static async Task Main()
        {
            var examples = GetExamples();
            var logger = Setup.Logger;

            logger.LogInformation("Starting application...");
            await Task.WhenAll(examples.Select(x => x.Start(SYMBOL)));

            logger.LogInformation("Press ctrl+c to exit...");
            Setup.ExitEvent.WaitOne();

            logger.LogInformation("Stopping application...");
            await Task.WhenAll(examples.Select(x => x.Stop(SYMBOL)));

            // wait until unsubscribe requests are send
            await Task.Delay(1000);
        }

        private static IExample[] GetExamples()
        {
            // running examples can be configured in Setup.cs file
            var examples = Setup.ServiceProvider.GetRequiredService<IEnumerable<IExample>>().ToArray();
            return examples;
        }
    }
}