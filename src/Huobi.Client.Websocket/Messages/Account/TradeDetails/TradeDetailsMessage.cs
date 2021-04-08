using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.TradeDetails
{
    public class TradeDetailsMessage
    {
        [JsonConstructor]
        public TradeDetailsMessage(string channel, TradeDetailsMessageData data)
        {
            Validations.ValidateInput(channel, nameof(channel));
            Validations.ValidateInput(data, nameof(data));

            Channel = channel;
            Data = data;
        }

        [JsonProperty("ch")]
        public string Channel { get; }

        public TradeDetailsMessageData Data { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out TradeDetailsMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                AccountSubscriptionType.TradeDetails.ToTopicId(), 
                "\"message\"",
                out response);

            return result && response?.Data.TradeTimeMs > 0;
        }
    }
}