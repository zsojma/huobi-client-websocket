using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPricePullResponse : PullResponse<MarketByPricePullTick>
    {
        [JsonConstructor]
        public MarketByPricePullResponse(
            string reqId,
            string status,
            string topic,
            long timestampMs,
            MarketByPricePullTick data)
            : base(reqId, status, topic, timestampMs, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketByPricePullResponse response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"rep\"",
                    SubscriptionType.MarketByPrice.ToTopicId()
                },
                out response);

            return result && !string.IsNullOrEmpty(response?.Data.SeqNum);
        }
    }
}