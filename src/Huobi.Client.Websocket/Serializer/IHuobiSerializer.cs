using System.Diagnostics.CodeAnalysis;

namespace Huobi.Client.Websocket.Serializer
{
    public interface IHuobiSerializer
    {
        string Serialize(object input);

        bool TryDeserializeIfContains<T>(
            string input,
            string containsValue,
            [MaybeNullWhen(false)] out T deserialized)
            where T : class;

        bool TryDeserializeIfContains<T>(
            string input,
            string[] containsValues,
            [MaybeNullWhen(false)] out T deserialized)
            where T : class;

        bool TryDeserializeIfContains<T>(
            string input,
            string containsValue,
            string notContainsValue,
            [MaybeNullWhen(false)] out T deserialized)
            where T : class;

        bool TryDeserializeIfContains<T>(
            string input,
            string[] containsValues,
            string[] notContainsValues,
            [MaybeNullWhen(false)] out T deserialized)
            where T : class;
    }
}