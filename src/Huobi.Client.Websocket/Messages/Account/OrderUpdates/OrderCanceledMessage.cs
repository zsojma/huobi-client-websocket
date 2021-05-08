using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderCanceledMessage : AccountResponseBase<OrderCanceledMessageData>
    {
        public OrderCanceledMessage(string action, string channel, OrderCanceledMessageData data)
            : base(action, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out OrderCanceledMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Cancellation.ToString().ToLower()
                },
                out response);

            return result && response?.Data?.OrderId > 0;
        }
    }
}