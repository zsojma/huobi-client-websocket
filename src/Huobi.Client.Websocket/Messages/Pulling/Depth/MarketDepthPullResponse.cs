using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Ticks;
using Huobi.Client.Websocket.Messages.Values;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Pulling.Depth
{
    public class MarketDepthPullResponse : PullResponse<MarketDepthTick>
    {
        [JsonConstructor]
        public MarketDepthPullResponse(
            string reqId,
            string status,
            string topic,
            long timestamp,
            MarketDepthTick data)
            : base(reqId, status, topic, timestamp, data)
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

            return result && response?.Data.Timestamp > 0;
        }
    }
}