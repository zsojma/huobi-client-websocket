using Huobi.Client.Websocket.Config;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket.Communicator;

public class HuobiMarketWebsocketCommunicator : HuobiGenericWebsocketCommunicator, IHuobiMarketWebsocketCommunicator
{
    public HuobiMarketWebsocketCommunicator(IOptions<HuobiMarketWebsocketClientConfig> config)
        : base(config)
    {
    }
}