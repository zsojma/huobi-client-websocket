using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public class ErrorMessage : ResponseBase
    {
        [JsonConstructor]
        public ErrorMessage(string reqId, string status, string timestamp, string errorCode, string message)
            : base(reqId)
        {
            Status = status;
            Timestamp = timestamp;
            ErrorCode = errorCode;
            Message = message;
        }

        public string Status { get; }

        [JsonProperty("ts")]
        public string Timestamp { get; }

        [JsonProperty("err-code")]
        public string ErrorCode { get; }

        [JsonProperty("err-msg")]
        public string Message { get; }

        internal static bool TryParse(IHuobiSerializer serializer, string input, [MaybeNullWhen(false)] out ErrorMessage response)
        {
            return serializer.TryDeserializeIfContains(input, "\"err-code\"", out response);
        }
    }
}