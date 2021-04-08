using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDepth
{
    public class MarketDepthPullResponse : PullResponse<MarketDepthTick>
    {
        [JsonConstructor]
        public MarketDepthPullResponse(
            string reqId,
            string status,
            string topic,
            long timestampMs,
            MarketDepthTick data)
            : base(reqId, status, topic, timestampMs, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketDepthPullResponse response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"rep\"",
                    SubscriptionType.MarketDepth.ToTopicId()
                },
                out response);

            return result && response?.Data.TimestampMs > 0;
        }
    }
}