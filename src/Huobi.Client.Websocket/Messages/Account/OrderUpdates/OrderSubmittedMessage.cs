using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderSubmittedMessage : AccountResponseBase<OrderSubmittedData>
    {
        public OrderSubmittedMessage(
            string action,
            int code,
            string channel,
            OrderSubmittedData data)
            : base(action, code, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out OrderSubmittedMessage response)
        {
            return serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"push\"",
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Creation.ToMessageValue(),
                    OrderStatus.Submitted.ToMessageValue()
                },
                out response);
        }
    }
}