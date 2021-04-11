using System;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class UpdateMessage<TTick>
        where TTick : class
    {
        [JsonConstructor]
        public UpdateMessage(string topic, long timestampMs, TTick tick)
        {
            Validations.ValidateInput(topic, nameof(topic));
            Validations.ValidateInput(tick, nameof(tick));

            Topic = topic;
            TimestampMs = timestampMs;
            Tick = tick;
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp => DateTimeOffset.FromUnixTimeMilliseconds(TimestampMs);

        [JsonProperty("ch")]
        public string Topic { get; }

        [JsonProperty("ts")]
        internal long TimestampMs { get; }

        public TTick Tick { get; }

        public string ParseSymbolFromTopic()
        {
            // faster then use of regex or split

            var prefixLength = HuobiConstants.MARKET_PREFIX.Length + 1;
            if (Topic.Length > prefixLength)
            {
                var withoutPrefix = Topic[prefixLength..];
                var length = withoutPrefix.IndexOf(".", StringComparison.Ordinal);
                if (length > 0)
                {
                    return withoutPrefix.Substring(0, length);
                }
            }

            throw new HuobiWebsocketClientException("Unable to parse symbol from topic: " + Topic);
        }
    }
}