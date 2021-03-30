using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AuthenticationResponse
    {
        [JsonConstructor]
        public AuthenticationResponse(string action, int code, string topic, object data)
        {
            Action = action;
            Code = code;
            Topic = topic;
            Data = data;
        }


        public string Action { get; }
        public int Code { get; }

        [JsonProperty("ch")]
        public string Topic { get; }

        public object Data { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out AuthenticationResponse response)
        {
            return serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"req\"",
                    "\"auth\""
                },
                out response);
        }
    }
}