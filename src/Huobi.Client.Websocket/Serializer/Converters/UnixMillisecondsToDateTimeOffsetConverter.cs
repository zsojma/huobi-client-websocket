using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Serializer.Converters
{
    internal class UnixMillisecondsToDateTimeOffsetConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                var ms = dateTimeOffset.ToUnixTimeMilliseconds();
                writer.WriteRawValue(ms.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteRawValue(value?.ToString());
            }
        }

        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value is long num
             || reader.Value is string str && long.TryParse(str, out num))
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(num);
            }

            return null;
        }
    }
}