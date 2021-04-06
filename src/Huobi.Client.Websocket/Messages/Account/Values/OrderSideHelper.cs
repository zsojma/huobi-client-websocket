using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class OrderSideHelper
    {
        public static OrderSide FromMessageValue(string orderSide)
        {
            return Enum.TryParse(orderSide, true, out OrderSide value)
                ? value
                : throw new ArgumentOutOfRangeException(
                    nameof(orderSide),
                    orderSide,
                    $"Unable to translate {orderSide} to order side!");
        }

        public static string ToMessageValue(this OrderSide orderSide)
        {
            return orderSide.ToString().ToLower();
        }
    }
}