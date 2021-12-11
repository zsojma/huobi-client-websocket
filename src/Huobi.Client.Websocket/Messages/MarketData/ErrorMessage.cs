using System;
using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.MarketData;

public class ErrorMessage
{
    [JsonConstructor]
    public ErrorMessage(string? status, string? errorCode, string? message, DateTimeOffset timestamp, string? reqId)
    {
        Status = status ?? string.Empty;
        ErrorCode = errorCode ?? string.Empty;
        Message = message ?? string.Empty;
        Timestamp = timestamp;
        ReqId = reqId;
    }

    public string Status { get; }

    [JsonProperty("err-code")]
    public string ErrorCode { get; }

    [JsonProperty("err-msg")]
    public string Message { get; }

    [JsonProperty("id")]
    public string? ReqId { get; }

    [JsonProperty("ts")]
    public DateTimeOffset Timestamp { get; }

    internal static bool TryParse(IHuobiSerializer serializer, string input, [MaybeNullWhen(false)] out ErrorMessage response)
    {
        var result = serializer.TryDeserializeIfContains(input, "\"err-code\"", out response);
        return result && !string.IsNullOrEmpty(response?.ErrorCode);
    }
}