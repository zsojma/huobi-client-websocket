using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Huobi.Client.Websocket.Messages.MarketData.Values
{
    internal class OrderBookLevelConverter : JsonConverter
    {
        private readonly OrderBookSide _side;

        public OrderBookLevelConverter(OrderBookSide side)
        {
            _side = side;
        }

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(decimal[][]);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            return JArrayToTradingTicker(JArray.Load(reader));
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private BookLevel[] JArrayToTradingTicker(JArray data)
        {
            var bookLevelList = new List<BookLevel>();

            foreach (var source in data)
            {
                var array = source.ToArray();
                if (array.Length == 2)
                {
                    var bookLevel = new BookLevel(_side, (decimal)array[0], (decimal)array[1]);
                    bookLevelList.Add(bookLevel);
                }
            }

            return bookLevelList.ToArray();
        }
    }

    internal class UnitMillisecondsToDateTimeOffsetConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset);
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
    }
}