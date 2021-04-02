using System;
using Huobi.Client.Websocket.Config;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket.Communicator
{
    public class HuobiAccountWebsocketCommunicator : HuobiWebsocketCommunicator, IHuobiAccountWebsocketCommunicator
    {
        public HuobiAccountWebsocketCommunicator(IOptions<HuobiAccountWebsocketClientConfig> config)
            : base(config)
        {
        }

        public HuobiAccountWebsocketCommunicator(Uri url)
            : base(url)
        {
        }
    }
}