using Huobi.Client.Websocket.Messages.Account.Values;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountSubscribeRequest : AccountRequestBase
    {
        public AccountSubscribeRequest(string symbol, AccountSubscriptionType subscriptionType)
            : base("sub", FormatChannel(symbol, subscriptionType))
        {
        }

        private static string FormatChannel(string symbol, AccountSubscriptionType subscriptionType)
        {
            if (subscriptionType == AccountSubscriptionType.Liquidations) {
                return $"public.{symbol}.liquidation_orders";
            }
            return $"{subscriptionType.ToTopicId()}#{symbol}";
        }
    }
}