using Huobi.Client.Websocket.Messages.Account.Values;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class AccountOrderUpdatesSubscribeRequest : AccountSubscribeRequest
    {
        public AccountOrderUpdatesSubscribeRequest(string symbol)
            : base(symbol, AccountSubscriptionType.Orders)
        {
        }
    }
}