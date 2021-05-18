using System;

namespace Huobi.Client.Websocket.Messages.MarketData
{
    public static class ResponseExtensions
    {
        public static string ParseSymbolFromTopic(this IResponse response)
        {
            // faster then use of regex or split

            var prefixLength = HuobiConstants.MARKET_PREFIX.Length + 1;
            if (response.Topic.Length > prefixLength)
            {
                var withoutPrefix = response.Topic.Substring(prefixLength);
                var length = withoutPrefix.IndexOf(".", StringComparison.Ordinal);
                if (length > 0)
                {
                    return withoutPrefix.Substring(0, length);
                }
            }

            throw new HuobiWebsocketClientException("Unable to parse symbol from topic: " + response.Topic);
        }
    }
}