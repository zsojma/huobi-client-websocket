using System;
using System.Collections.Generic;
using System.Linq;
using Huobi.Client.Websocket.Messages.MarketData.Values;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Huobi.Client.Websocket.Serializer.Converters
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

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            var bookLevelList = new List<BookLevel>();
            var data = JArray.Load(reader);

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
}