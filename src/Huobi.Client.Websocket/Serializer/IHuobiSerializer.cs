using System.Diagnostics.CodeAnalysis;

namespace Huobi.Client.Websocket.Serializer
{
    public interface IHuobiSerializer
    {
        string Serialize(object input);
        T Deserialize<T>(string input);
        bool TryDeserializeIfContains<T>(string input, string containsValue, [MaybeNullWhen(false)] out T deserialized)
            where T : class;
    }
}