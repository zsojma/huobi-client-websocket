using System;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Utils;
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
            Validations.ValidateInput(config.Value.Url, nameof(Url));
            return new Uri(config.Value.Url!);
        }
    }
}