﻿using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Serializer
{
    internal class HuobiSerializer : IHuobiSerializer
    {
        private readonly ILogger<HuobiSerializer> _logger;

        public HuobiSerializer(ILogger<HuobiSerializer> logger)
        {
            _logger = logger;
        }

        public string Serialize(object input)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(input);
                return serialized;
            }
            catch (JsonSerializationException ex)
            {
                _logger.LogError(ex, $"Unable to serialize object! Error: {ex.Message}");
                throw;
            }
        }

        public bool TryDeserializeIfContains<T>(string input, string containsValue, [MaybeNullWhen(false)] out T deserialized)
            where T : class
        {
            if (!input.Contains(containsValue))
            {
                deserialized = null;
                return false;
            }

            deserialized = Deserialize<T>(input);
            return deserialized != null;
        }

        public bool TryDeserializeIfContains<T>(string input, string[] containsValues, [MaybeNullWhen(false)] out T deserialized)
            where T : class
        {
            if (containsValues.Any(x => !input.Contains(x)))
            {
                deserialized = null;
                return false;
            }

            deserialized = Deserialize<T>(input);
            return deserialized != null;
        }

        public bool TryDeserializeIfContains<T>(
            string input,
            string[] containsValues,
            string[] notContainsValues,
            [MaybeNullWhen(false)] out T deserialized)
            where T : class
        {
            if (notContainsValues.Any(input.Contains))
            {
                deserialized = null;
                return false;
            }

            return TryDeserializeIfContains(input, containsValues, out deserialized);
        }

        [return: MaybeNull]
        private T Deserialize<T>(string input)
        {
            var deserialized = JsonConvert.DeserializeObject<T>(input);
            return deserialized;
        }
    }
}