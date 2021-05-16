using Huobi.Client.Websocket.Config;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket.Communicator
{
    public class HuobiAccountWebsocketCommunicator : HuobiGenericWebsocketCommunicator, IHuobiAccountWebsocketCommunicator
    {
        public HuobiAccountWebsocketCommunicator(IOptions<HuobiAccountWebsocketClientConfig> config)
            : base(config)
        {
        }
    }
}