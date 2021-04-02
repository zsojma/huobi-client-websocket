using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket.Clients
{
    public static class HuobiWebsocketClientsFactory
    {
        public static IHuobiGenericWebsocketClient CreateGenericClient(string url, ILoggerFactory? loggerFactory = null)
        {
            var config = new HuobiWebsocketClientConfig
            {
                Url = url
            };

            return CreateGenericClient(config, loggerFactory);
        }

        public static IHuobiGenericWebsocketClient CreateGenericClient(
            HuobiWebsocketClientConfig config,
            ILoggerFactory? loggerFactory = null)
        {
            loggerFactory ??= NullLoggerFactory.Instance;

            var communicator = new HuobiWebsocketCommunicator(Options.Create(config));
            var serializer = new HuobiSerializer(loggerFactory.CreateLogger<HuobiSerializer>());

            return new HuobiGenericWebsocketClient(
                communicator,
                serializer,
                loggerFactory.CreateLogger<HuobiGenericWebsocketClient>());
        }

        public static IHuobiMarketWebsocketClient CreateMarketClient(string url, ILoggerFactory? loggerFactory = null)
        {
            var config = new HuobiWebsocketClientConfig
            {
                Url = url
            };

            return CreateMarketClient(config, loggerFactory);
        }

        public static IHuobiMarketWebsocketClient CreateMarketClient(
            HuobiWebsocketClientConfig config,
            ILoggerFactory? loggerFactory = null)
        {
            loggerFactory ??= NullLoggerFactory.Instance;

            var communicator = new HuobiWebsocketCommunicator(Options.Create(config));
            var serializer = new HuobiSerializer(loggerFactory.CreateLogger<HuobiSerializer>());

            return new HuobiMarketWebsocketClient(
                communicator,
                serializer,
                loggerFactory.CreateLogger<HuobiMarketWebsocketClient>());
        }

        public static IHuobiAccountWebsocketClient CreateAccountClient(
            string url,
            string accessKey,
            string secretKey,
            ILoggerFactory? loggerFactory = null)
        {
            var config = new HuobiAccountWebsocketClientConfig
            {
                Url = url,
                AccessKey = accessKey,
                SecretKey = secretKey
            };

            return CreateAccountClient(config, loggerFactory);
        }

        public static IHuobiAccountWebsocketClient CreateAccountClient(
            HuobiAccountWebsocketClientConfig config,
            ILoggerFactory? loggerFactory = null)
        {
            loggerFactory ??= NullLoggerFactory.Instance;

            var options = Options.Create(config);
            var communicator = new HuobiAccountWebsocketCommunicator(options);
            var serializer = new HuobiSerializer(loggerFactory.CreateLogger<HuobiSerializer>());
            var dateTimeProvider = new HuobiDateTimeProvider();
            var signature = new HuobiSignature();
            var authenticationRequestFactory = new HuobiAuthenticationRequestFactory(dateTimeProvider, signature);

            return new HuobiAccountWebsocketClient(
                options,
                communicator,
                serializer,
                authenticationRequestFactory,
                loggerFactory.CreateLogger<HuobiAccountWebsocketClient>());
        }
    }
}