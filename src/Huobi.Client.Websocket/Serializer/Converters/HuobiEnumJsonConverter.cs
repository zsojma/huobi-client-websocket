using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Serializer.Converters
{
    public class HuobiEnumJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsEnum;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == objectType)
            {
                return reader.Value;
            }

            if (reader.Value is string str)
            {
                return TryConvertFromString(objectType, str);
            }

            return null;
        }

        private static object? TryConvertFromString(Type objectType, string input)
        {
            var sanitized = input.Replace("-", "");
            return Enum.TryParse(objectType, sanitized, true, out var type)
                ? type
                : null;
        }
    }
}