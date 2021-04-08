using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class OrderSubmittedMessage : AccountResponseBase<OrderSubmittedMessageData>
    {
        public OrderSubmittedMessage(
            string action,
            string channel,
            OrderSubmittedMessageData data)
            : base(action, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out OrderSubmittedMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Creation.ToMessageValue(),
                    OrderStatus.Submitted.ToMessageValue()
                },
                out response);

            return result && response?.Data.OrderId > 0;
        }
    }
}