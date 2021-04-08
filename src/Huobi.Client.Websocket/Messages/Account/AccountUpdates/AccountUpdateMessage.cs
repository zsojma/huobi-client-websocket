using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.AccountUpdates
{
    public class AccountUpdateMessage : AccountResponseBase<AccountUpdateMessageData>
    {
        public AccountUpdateMessage(string action, string channel, AccountUpdateMessageData data)
            : base(action, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out AccountUpdateMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                AccountSubscriptionType.AccountUpdates.ToTopicId(),
                "\"code\"",
                out response);

            return result && response?.Data.ChangeTimeMs > 0;
        }
    }
}