using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketDetails
{
    public class MarketDetailsUpdateMessage : UpdateMessage<MarketDetailsTick>
    {
        public MarketDetailsUpdateMessage(string topic, DateTimeOffset timestamp, MarketDetailsTick tick)
            : base(topic, timestamp, tick)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketDetailsUpdateMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"tick\"",
                    SubscriptionType.MarketDetails.ToTopicId()
                },
                new[]
                {
                    SubscriptionType.MarketTradeDetail.ToTopicId()
                },
                out response);

            return result && response?.Tick?.Id > 0;
        }
    }
}