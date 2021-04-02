using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class AuthSubscriptionTypeExtensions
    {
        public static string ToTopicId(this AuthSubscriptionType subscriptionType)
        {
            return subscriptionType switch
            {
                AuthSubscriptionType.AccountUpdates => "accounts.update",
                AuthSubscriptionType.Orders => "orders",
                AuthSubscriptionType.TradeDetails => "trade.clearing",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(subscriptionType),
                    subscriptionType,
                    $"Unable to translate {nameof(subscriptionType)} value '{subscriptionType}' to topic ID!")
            };
        }
    }
}