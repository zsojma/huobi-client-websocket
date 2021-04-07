using System;

namespace Huobi.Client.Websocket.Messages.Account.Values
{
    public static class OrderTypeHelper
    {
        public static OrderType FromMessageValue(string orderType)
        {
            return orderType switch
            {
                "buy-market" => OrderType.BuyMarket,
                "sell-market" => OrderType.SellMarket,
                "buy-limit" => OrderType.BuyLimit,
                "sell-limit" => OrderType.SellLimit,
                "buy-limit-maker" => OrderType.BuyLimitMaker,
                "sell-limit-maker" => OrderType.SellLimitMaker,
                "buy-ioc" => OrderType.BuyIoc,
                "sell-ioc" => OrderType.SellIoc,
                "buy-limit-fok" => OrderType.BuyLimitFok,
                "sell-limit-fok" => OrderType.SellLimitFok,
                "buy-stop-limit" => OrderType.BuyStopLimit,
                "sell-stop-limit" => OrderType.SellStopLimit,
                "buy-stop-limit-fok" => OrderType.BuyStopLimitFok,
                "sell-stop-limit-fok" => OrderType.SellStopLimitFok,
                _ => throw new ArgumentOutOfRangeException(
                    nameof(orderType),
                    orderType,
                    $"Unable to translate {orderType} to order type!")
            };
        }

        public static string ToMessageValue(this OrderType orderType)
        {
            return orderType switch
            {
                OrderType.BuyMarket => "buy-market",
                OrderType.SellMarket => "sell-market",
                OrderType.BuyLimit => "buy-limit",
                OrderType.SellLimit => "sell-limit",
                OrderType.BuyLimitMaker => "buy-limit-maker",
                OrderType.SellLimitMaker => "sell-limit-maker",
                OrderType.BuyIoc => "buy-ioc",
                OrderType.SellIoc => "sell-ioc",
                OrderType.BuyLimitFok => "buy-limit-fok",
                OrderType.SellLimitFok => "sell-limit-fok",
                OrderType.BuyStopLimit => "buy-stop-limit",
                OrderType.SellStopLimit => "sell-stop-limit",
                OrderType.BuyStopLimitFok => "buy-stop-limit-fok",
                OrderType.SellStopLimitFok => "sell-stop-limit-fok",
                _ => orderType.ToString().ToLower()
            };
        }
    }
}