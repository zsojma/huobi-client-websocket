using Huobi.Client.Websocket.Config;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket.Communicator
{
    public class HuobiMarketByPriceWebsocketCommunicator : HuobiGenericWebsocketCommunicator, IHuobiMarketByPriceWebsocketCommunicator
    {
        public HuobiMarketByPriceWebsocketCommunicator(IOptions<HuobiMarketByPriceWebsocketClientConfig> config)
            : base(config)
        {
        }
    }
}