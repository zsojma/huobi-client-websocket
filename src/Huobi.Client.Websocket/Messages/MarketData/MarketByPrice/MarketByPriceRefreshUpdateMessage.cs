using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.MarketData.MarketByPrice
{
    public class MarketByPriceRefreshUpdateMessage : UpdateMessage<MarketByPriceTick>
    {
        public MarketByPriceRefreshUpdateMessage(string topic, long timestampMs, MarketByPriceTick tick)
            : base(topic, timestampMs, tick)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out MarketByPriceRefreshUpdateMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"tick\"",
                    SubscriptionType.MarketByPriceRefresh.ToTopicId()
                },
                out response);

            return result && !string.IsNullOrEmpty(response?.Tick.SeqNum);
        }
    }
}