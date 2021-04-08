using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderCanceledMessage : AccountResponseBase<ConditionalOrderCanceledMessageData>
    {
        public ConditionalOrderCanceledMessage(
            string action,
            string channel,
            ConditionalOrderCanceledMessageData data)
            : base(action, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out ConditionalOrderCanceledMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Deletion.ToMessageValue()
                },
                out response);

            return result && response?.Data.OrderTriggerTimeMs > 0;
        }
    }
}