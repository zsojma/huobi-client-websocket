﻿using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.OrderUpdates
{
    public class ConditionalOrderCanceledMessage : AccountResponseBase<ConditionalOrderCanceledData>
    {
        public ConditionalOrderCanceledMessage(
            string action,
            int code,
            string channel,
            ConditionalOrderCanceledData data)
            : base(action, code, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out ConditionalOrderCanceledMessage response)
        {
            return serializer.TryDeserializeIfContains(
                input,
                new[]
                {
                    "\"push\"",
                    AccountSubscriptionType.Orders.ToTopicId(),
                    OrderEventType.Deletion.ToMessageValue()
                },
                out response);
        }
    }
}