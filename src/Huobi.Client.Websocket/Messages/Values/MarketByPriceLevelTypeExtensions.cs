using System;

namespace Huobi.Client.Websocket.Messages.Values
{
    public static class MarketByPriceLevelTypeExtensions
    {
        public static string ToStep(this MarketByPriceLevelType levelType)
        {
            return levelType switch
            {
                MarketByPriceLevelType.Five => "5",
                MarketByPriceLevelType.Twenty => "20",
                MarketByPriceLevelType.OneHundredAndFifty => "150",
                _ => throw new ArgumentOutOfRangeException(
                    nameof(levelType),
                    levelType,
                    $"Unable to translate {levelType} to API step!")
            };
        }
    }
}