using System;

namespace Huobi.Client.Websocket.Messages.MarketData.Values
{
    public static class MarketByPriceRefreshLevelTypeExtensions
    {
        public static string ToStep(this MarketByPriceRefreshLevelType levelType)
        {
            return levelType switch
            {
                MarketByPriceRefreshLevelType.Five => "5",
                MarketByPriceRefreshLevelType.Ten => "10",
                MarketByPriceRefreshLevelType.Twenty => "20",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(levelType),
                    levelType,
                    $"Unable to translate {levelType} to API step!")
            };
        }
    }
}