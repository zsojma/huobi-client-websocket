using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Serializer;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account;

public class AuthenticationResponse : AccountResponseBase<object>
{
    [JsonConstructor]
    public AuthenticationResponse(string action, int code, string channel, object data)
        : base(action, channel, data)
    {
        Code = code;
    }

    public int Code { get; }

    internal static bool TryParse(
        IHuobiSerializer serializer,
        string input,
        [MaybeNullWhen(false)] out AuthenticationResponse response)
    {
        var result = serializer.TryDeserializeIfContains(
            input,
            new[]
            {
                "\"req\"",
                "\"auth\""
            },
            out response);

        return result;
    }
}