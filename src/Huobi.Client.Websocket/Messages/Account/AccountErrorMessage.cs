using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Utils;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountErrorMessage
    {
        [JsonConstructor]
        public AccountErrorMessage(int code, string topic, string message)
        {
            Validations.ValidateInput(topic, nameof(topic));
            Validations.ValidateInput(message, nameof(message));

            Code = code;
            Topic = topic;
            Message = message;
        }

        public int Code { get; }

        [JsonProperty("ch")]
        public string Topic { get; }

        public string Message { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out AccountErrorMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"code\"",
                    "\"message\""
                },
                out response);

            return result && response?.Code != 200;
        }
    }
}