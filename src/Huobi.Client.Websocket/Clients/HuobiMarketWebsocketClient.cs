using Huobi.Client.Websocket.Clients.Streams;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketBestBidOffer;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;
using Huobi.Client.Websocket.Messages.MarketData.MarketCandlestick;
using Huobi.Client.Websocket.Messages.MarketData.MarketDepth;
using Huobi.Client.Websocket.Messages.MarketData.MarketDetails;
using Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Clients;

public class HuobiMarketWebsocketClient : HuobiWebsocketClientBase<HuobiMarketClientStreams>, IHuobiMarketWebsocketClient
{
    public HuobiMarketWebsocketClient(
        IHuobiMarketWebsocketCommunicator communicator,
        IHuobiSerializer serializer,
        ILogger<HuobiMarketWebsocketClient> logger)
        : base(communicator, serializer, logger)
    {
    }

    public void Send(RequestBase request)
    {
        var serialized = Serializer.Serialize(request);
        Send(serialized);
    }

    protected override bool TryHandleMessage(string message)
    {
        return TryHandlePullResponses(message)
               || TryHandleUpdateMessages(message)
               || TryHandleSubscribeResponses(message);
    }

    private bool TryHandlePullResponses(string message)
    {
        if (MarketCandlestickPullResponse.TryParse(Serializer, message, out var marketCandlestick))
        {
            Streams.CandlestickPullStream.OnNext(marketCandlestick);
            return true;
        }

        if (MarketDepthPullResponse.TryParse(Serializer, message, out var marketDepth))
        {
            Streams.DepthPullStream.OnNext(marketDepth);
            return true;
        }

        if (MarketTradeDetailPullResponse.TryParse(Serializer, message, out var marketTradeDetail))
        {
            Streams.TradeDetailPullStream.OnNext(marketTradeDetail);
            return true;
        }

        if (MarketDetailsPullResponse.TryParse(Serializer, message, out var marketDetails))
        {
            Streams.MarketDetailsPullStream.OnNext(marketDetails);
            return true;
        }

        return false;
    }

    private bool TryHandleUpdateMessages(string message)
    {
        if (MarketCandlestickUpdateMessage.TryParse(Serializer, message, out var marketCandlestick))
        {
            Streams.CandlestickUpdateStream.OnNext(marketCandlestick);
            return true;
        }

        if (MarketDepthUpdateMessage.TryParse(Serializer, message, out var marketDepth))
        {
            Streams.DepthUpdateStream.OnNext(marketDepth);
            return true;
        }

        if (MarketByPriceRefreshUpdateMessage.TryParse(Serializer, message, out var marketByPriceRefresh))
        {
            Streams.MarketByPriceRefreshUpdateStream.OnNext(marketByPriceRefresh);
            return true;
        }

        if (MarketBestBidOfferUpdateMessage.TryParse(Serializer, message, out var marketBestBidOffer))
        {
            Streams.BestBidOfferUpdateStream.OnNext(marketBestBidOffer);
            return true;
        }

        if (MarketTradeDetailUpdateMessage.TryParse(Serializer, message, out var marketTradeDetail))
        {
            Streams.TradeDetailUpdateStream.OnNext(marketTradeDetail);
            return true;
        }

        if (MarketDetailsUpdateMessage.TryParse(Serializer, message, out var marketDetails))
        {
            Streams.MarketDetailsUpdateStream.OnNext(marketDetails);
            return true;
        }

        return false;
    }

    private bool TryHandleSubscribeResponses(string message)
    {
        if (SubscribeResponse.TryParse(Serializer, message, out var subscribeResponse))
        {
            Streams.SubscribeResponseStream.OnNext(subscribeResponse);
            return true;
        }

        if (UnsubscribeResponse.TryParse(Serializer, message, out var unsubscribeResponse))
        {
            Streams.UnsubscribeResponseStream.OnNext(unsubscribeResponse);
            return true;
        }

        return false;
    }
}