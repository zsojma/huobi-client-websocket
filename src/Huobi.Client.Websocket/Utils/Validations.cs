using System;

namespace Huobi.Client.Websocket.Utils
{
    public static class Validations
    {
        public static void ValidateInput<T>(T? value, string name)
            where T : class
        {
            if (value is default(T))
            {
                throw new ArgumentNullException(
                    name,
                    $"{name} value cannot be null! Value: {value}");
            }
        }

        public static void ValidateInput(string? value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(
                    name,
                    $"{name} value cannot be null or empty! Value: {value}");
            }
        }
    }
}