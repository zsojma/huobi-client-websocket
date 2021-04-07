using Huobi.Client.Websocket.Messages.Account.Values;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderUpdatesSubscribeRequest : AccountSubscribeRequest
    {
        public OrderUpdatesSubscribeRequest(string symbol)
            : base(symbol, AccountSubscriptionType.Orders)
        {
        }
    }
}