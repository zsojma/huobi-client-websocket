using System;

namespace Huobi.Client.Websocket.Messages.Values
{
    public static class MarketDepthStepTypeExtensions
    {
        public static string ToStep(this MarketDepthStepType stepType)
        {
            return stepType switch
            {
                MarketDepthStepType.NoAggregation => "step0",
                MarketDepthStepType.Level1 => "step1",
                MarketDepthStepType.Level2 => "step2",
                MarketDepthStepType.Level3 => "step3",
                MarketDepthStepType.Level4 => "step4",
                MarketDepthStepType.Level5 => "step5",
                _ => throw new ArgumentOutOfRangeException(nameof(stepType), stepType, $"Unable to translate {stepType} to API step!")
            };
        }
    }
}