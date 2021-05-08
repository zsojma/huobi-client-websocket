using System;
using Websocket.Client;

namespace Huobi.Client.Websocket.Communicator
{
    public class HuobiWebsocketCommunicator : WebsocketClient, IHuobiWebsocketCommunicator
    {
        public HuobiWebsocketCommunicator(Uri url, string? communicatorName = null, int? reconnectTimeoutMin = null)
            : base(url)
        {
            Name = communicatorName ?? "Huobi";

            if (reconnectTimeoutMin.HasValue)
            {
                ReconnectTimeout = TimeSpan.FromMinutes(reconnectTimeoutMin.Value);
            }
        }
    }
}