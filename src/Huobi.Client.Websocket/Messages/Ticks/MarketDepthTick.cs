using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Ticks
{
    public class MarketDepthTick
    {
        [JsonConstructor]
        public MarketDepthTick(decimal[][] bids, decimal[][] asks, long version, long timestamp)
        {
            Bids = bids;
            Asks = asks;
            Version = version;
            Timestamp = timestamp;
        }

        public decimal[][] Bids { get; }
        public decimal[][] Asks { get; }
        public long Version { get; }

        [JsonProperty("ts")]
        public long Timestamp { get; }
    }
}