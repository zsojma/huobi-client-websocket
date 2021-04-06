using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderCanceledMessage : AccountResponseBase<OrderCanceledData>
    {
        public OrderCanceledMessage(string action, int code, string channel, OrderCanceledData data)
            : base(action, code, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out OrderCanceledMessage response)
        {
            return serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"push\"",
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Cancellation.ToMessageValue(),
                    OrderStatus.Canceled.ToMessageValue()
                },
                out response);
        }
    }
}