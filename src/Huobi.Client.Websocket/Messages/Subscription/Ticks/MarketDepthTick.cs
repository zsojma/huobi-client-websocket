using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Subscription.Ticks
{
    public class MarketDepthBidValue
    {

    }

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

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out Message<MarketDepthTick> response)
        {
            return serializer.TryDeserializeIfContains(input, SubscriptionType.MarketDepth.ToTopicId(), out response);
        }
    }
}