using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class OrderStatusHelper
    {
        public static OrderStatus FromMessageValue(string orderStatus)
        {
            return orderStatus switch
            {
                "partial-filled" => OrderStatus.PartialFilled,
                _ => Enum.TryParse(orderStatus, true, out OrderStatus value)
                    ? value
                    : throw new ArgumentOutOfRangeException(
                        nameof(orderStatus),
                        orderStatus,
                        $"Unable to translate {orderStatus} to order status!")
            };
        }

        public static string ToMessageValue(this OrderStatus orderStatus)
        {
            return orderStatus switch
            {
                OrderStatus.PartialFilled => "partial-filled",
                _ => orderStatus.ToString().ToLower()
            };
        }
    }
}