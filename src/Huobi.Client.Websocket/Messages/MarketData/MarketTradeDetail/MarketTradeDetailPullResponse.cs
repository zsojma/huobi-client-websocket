using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketTradeDetail
{
    public class MarketTradeDetailPullResponse : PullResponse<MarketTradeDetailTickDataItem[]>
    {
        [JsonConstructor]
        public MarketTradeDetailPullResponse(
            string reqId,
            string status,
            string topic,
            DateTimeOffset timestamp,
            MarketTradeDetailTickDataItem[] data)
            : base(reqId, status, topic, timestamp, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketTradeDetailPullResponse response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"rep\"",
                    SubscriptionType.MarketTradeDetail.ToTopicId()
                },
                out response);

            return result && response?.Data.FirstOrDefault()?.Timestamp.Ticks > 0;
        }
    }
}