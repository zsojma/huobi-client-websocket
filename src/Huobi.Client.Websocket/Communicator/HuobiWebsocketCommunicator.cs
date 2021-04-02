using System;
using Huobi.Client.Websocket.Config;
using Microsoft.Extensions.Options;
using Websocket.Client;

namespace Huobi.Client.Websocket.Communicator
{
    public class HuobiWebsocketCommunicator : WebsocketClient, IHuobiWebsocketCommunicator
    {
        public HuobiWebsocketCommunicator(IOptions<HuobiWebsocketClientConfig> config)
            : this(GetUrl(config))
        {
            Name = config.Value.CommunicatorName ?? "Huobi";

            if (config.Value.ReconnectTimeoutMin.HasValue)
            {
                ReconnectTimeout = TimeSpan.FromMinutes(config.Value.ReconnectTimeoutMin.Value);
            }
        }

        public HuobiWebsocketCommunicator(Uri url)
            : base(url)
        {
        }

        private static Uri GetUrl(IOptions<HuobiWebsocketClientConfig> config)
        {
            return new(
                config.Value.Url
             ?? throw new ArgumentNullException(nameof(config.Value.Url), "Huobi websocket url cannot be null"));
        }
    }
}