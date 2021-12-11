using Huobi.Client.Websocket.Clients.Streams;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Messages.MarketData.MarketByPrice;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging;

namespace Huobi.Client.Websocket.Clients;

public class HuobiMarketByPriceWebsocketClient : HuobiWebsocketClientBase<HuobiMarketByPriceClientStreams>,
    IHuobiMarketByPriceWebsocketClient
{
    public HuobiMarketByPriceWebsocketClient(
        IHuobiMarketByPriceWebsocketCommunicator communicator,
        IHuobiSerializer serializer,
        ILogger<HuobiMarketByPriceWebsocketClient> logger)
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
        if (MarketByPricePullResponse.TryParse(Serializer, message, out var marketByPrice))
        {
            Streams.MarketByPricePullSubject.OnNext(marketByPrice);
            return true;
        }

        return false;
    }

    private bool TryHandleUpdateMessages(string message)
    {
        if (MarketByPriceUpdateMessage.TryParse(Serializer, message, out var marketByPrice))
        {
            Streams.MarketByPriceUpdateSubject.OnNext(marketByPrice);
            return true;
        }

        return false;
    }

    private bool TryHandleSubscribeResponses(string message)
    {
        if (SubscribeResponse.TryParse(Serializer, message, out var subscribeResponse))
        {
            Streams.SubscribeResponseSubject.OnNext(subscribeResponse);
            return true;
        }

        if (UnsubscribeResponse.TryParse(Serializer, message, out var unsubscribeResponse))
        {
            Streams.UnsubscribeResponseSubject.OnNext(unsubscribeResponse);
            return true;
        }

        return false;
    }
}