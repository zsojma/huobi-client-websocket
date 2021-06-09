using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;
using System;

namespace Huobi.Client.Websocket.Messages.Account
{
    public class AccountSubscribeResponse : AccountResponseBase<object>
    {
        [JsonConstructor]
        public AccountSubscribeResponse(string op, int code, string topic, object data)
            : base(op, topic, data)
        {
            Code = code;
        }

        public int Code { get; }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out AccountSubscribeResponse response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"sub\""
                },
                out response);

            Console.WriteLine($"tryParseSubResp: {result}");
            return result;
        }
    }
}