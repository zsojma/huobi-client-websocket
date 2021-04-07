using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class TradeEventTypeHelper
    {
        public static TradeEventType FromMessageValue(string tradeEventType)
        {
            return Enum.TryParse(tradeEventType, true, out TradeEventType value)
                ? value
                : throw new ArgumentOutOfRangeException(
                    nameof(tradeEventType),
                    tradeEventType,
                    $"Unable to translate {tradeEventType} to trade event type!");
        }

        public static string ToMessageValue(this TradeEventType eventType)
        {
            return eventType.ToString().ToLower();
        }
    }
}