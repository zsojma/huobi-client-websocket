using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Ticks
{
    public class MarketDetailsTick
    {
        [JsonConstructor]
        public MarketDetailsTick(
            long id,
            decimal amount,
            int count,
            decimal open,
            decimal close,
            decimal low,
            decimal high,
            decimal vol)
        {
            Id = id;
            Amount = amount;
            Count = count;
            Open = open;
            Close = close;
            Low = low;
            High = high;
            Vol = vol;
        }

        public long Id { get; }
        public decimal Amount { get; }
        public int Count { get; }
        public decimal Open { get; }
        public decimal Close { get; }
        public decimal Low { get; }
        public decimal High { get; }
        public decimal Vol { get; }
    }
}