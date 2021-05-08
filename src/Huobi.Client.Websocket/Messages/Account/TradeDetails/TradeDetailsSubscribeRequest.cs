using Huobi.Client.Websocket.Messages.Account.Values;

namespace Huobi.Client.Websocket.Messages.Account.TradeDetails
{
    public class TradeDetailsSubscribeRequest : AccountSubscribeRequest
    {
        public TradeDetailsSubscribeRequest(string symbol, bool withCancellationEvents = false)
            : base(FormatSymbol(symbol, withCancellationEvents), AccountSubscriptionType.TradeDetails)
        {
        }

        private static string FormatSymbol(string symbol, bool withCancellationEvents)
        {
            return $"{symbol}#{(withCancellationEvents ? "1" : "0")}";
        }
    }
}