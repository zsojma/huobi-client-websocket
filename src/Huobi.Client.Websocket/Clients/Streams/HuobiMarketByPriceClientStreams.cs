using System.Reactive.Subjects;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;

namespace Huobi.Client.Websocket.Clients.Streams;

public class HuobiMarketByPriceClientStreams : HuobiClientStreamsBase
{
    public readonly Subject<SubscribeResponse> SubscribeResponseStream = new();
    public readonly Subject<UnsubscribeResponse> UnsubscribeResponseStream = new();
        
    public readonly Subject<MarketByPriceUpdateMessage> MarketByPriceUpdateStream = new();
    public readonly Subject<MarketByPricePullResponse> MarketByPricePullStream = new();
}