using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class AccountSubscriptionTypeExtensions
    {
        public static string ToTopicId(this AccountSubscriptionType subscriptionType)
        {
            return subscriptionType switch
            {
                AccountSubscriptionType.Orders => "orders",
                AccountSubscriptionType.TradeDetails => "trade.clearing",
                AccountSubscriptionType.AccountUpdates => "accounts.update",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(subscriptionType),
                    subscriptionType,
                    $"Unable to translate {nameof(subscriptionType)} value '{subscriptionType}' to topic ID!")
            };
        }
    }
}