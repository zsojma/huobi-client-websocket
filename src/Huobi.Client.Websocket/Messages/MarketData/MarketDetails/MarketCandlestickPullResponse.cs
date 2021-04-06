using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDetails
{
    public class MarketDetailsPullResponse : PullResponse<MarketDetailsTick>
    {
        [JsonConstructor]
        public MarketDetailsPullResponse(
            string reqId,
            string status,
            string topic,
            long timestamp,
            MarketDetailsTick data)
            : base(reqId, status, topic, timestamp, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketDetailsPullResponse response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"rep\"",
                    SubscriptionType.MarketDetails.ToTopicId()
                },
                new[]
                {
                    SubscriptionType.MarketTradeDetail.ToTopicId()
                },
                out response);

            return result && response?.Data.Id > 0;
        }
    }
}