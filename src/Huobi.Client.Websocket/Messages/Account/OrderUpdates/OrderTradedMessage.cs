using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderTradedMessage : AccountResponseBase<OrderTradedMessageData>
    {
        public OrderTradedMessage(string action, string channel, OrderTradedMessageData data)
            : base(action, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out OrderTradedMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Trade.ToString().ToLower()
                },
                out response);

            return result && response?.Data?.OrderId > 0;
        }
    }
}