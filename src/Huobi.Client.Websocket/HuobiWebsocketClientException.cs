using System;

namespace Huobi.Client.Websocket
{
    public class HuobiWebsocketClientException : Exception
    {
        public HuobiWebsocketClientException(string message)
            : base(message)
        {
        }
    }
}