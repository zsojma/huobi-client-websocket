using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderTriggerFailureMessage : AccountResponseBase<ConditionalOrderTriggerFailureData>
    {
        public ConditionalOrderTriggerFailureMessage(
            string action,
            int code,
            string channel,
            ConditionalOrderTriggerFailureData data)
            : base(action, code, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out ConditionalOrderTriggerFailureMessage response)
        {
            return serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"push\"",
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Trigger.ToMessageValue()
                },
                out response);
        }
    }
}