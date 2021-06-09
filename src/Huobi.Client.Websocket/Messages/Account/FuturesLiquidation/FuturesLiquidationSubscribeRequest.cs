using Huobi.Client.Websocket.Messages.Account.Values;

namespace Huobi.Client.Websocket.Messages.Account.FuturesLiquidation
{
    public class FuturesLiquidationsSubscribeRequest : AccountSubscribeRequest
    {
        public FuturesLiquidationsSubscribeRequest(string symbol)
            : base(symbol, AccountSubscriptionType.Liquidations)
        {
        }
    }
}