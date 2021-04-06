using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class OrderEventTypeHelper
    {
        public static OrderEventType FromMessageValue(string orderEventType)
        {
            return Enum.TryParse(orderEventType, true, out OrderEventType value)
                ? value
                : throw new ArgumentOutOfRangeException(
                    nameof(orderEventType),
                    orderEventType,
                    $"Unable to translate {orderEventType} to order event type!");
        }

        public static string ToMessageValue(this OrderEventType eventType)
        {
            return eventType.ToString().ToLower();
        }
    }
}