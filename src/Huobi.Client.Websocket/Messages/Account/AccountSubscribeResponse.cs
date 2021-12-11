using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account;

public class AccountSubscribeResponse : AccountResponseBase<object>
{
    [JsonConstructor]
    public AccountSubscribeResponse(string action, int code, string channel, object data)
        : base(action, channel, data)
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
                "\"sub\"",
                "\"code\""
            },
            new[]
            {
                "\"message\""
            },
            out response);

        return result;
    }
}