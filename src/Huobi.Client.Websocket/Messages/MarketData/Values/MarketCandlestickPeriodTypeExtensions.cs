using System;

namespace Huobi.Client.Websocket.Messages.MarketData.Values;

public static class MarketCandlestickPeriodTypeExtensions
{
    public static string ToStep(this MarketCandlestickPeriodType periodType)
    {
        return periodType switch
        {
            MarketCandlestickPeriodType.OneMinute => "1min",
            MarketCandlestickPeriodType.FiveMinutes => "5min",
            MarketCandlestickPeriodType.FifteenMinutes => "15min",
            MarketCandlestickPeriodType.ThirtyMinutes => "30min",
            MarketCandlestickPeriodType.SixtyMinutes => "60min",
            MarketCandlestickPeriodType.FourHours => "4hour",
            MarketCandlestickPeriodType.OneDay => "1day",
            MarketCandlestickPeriodType.OneWeek => "1week",
            MarketCandlestickPeriodType.OneMonth => "1mon",
            MarketCandlestickPeriodType.OneYear => "1year",
            _ => throw new ArgumentOutOfRangeException(
                nameof(periodType),
                periodType,
                $"Unable to translate {periodType} to API step!")
        };
    }
}