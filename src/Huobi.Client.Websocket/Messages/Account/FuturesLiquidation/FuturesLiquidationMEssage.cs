using System.Diagnostics.CodeAnalysis;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer;

namespace Huobi.Client.Websocket.Messages.Account.FuturesLiquidation
{
    public class FuturesLiquidationMessage : AccountResponseBase<FuturesLiquidationMessageData>
    {
        public FuturesLiquidationMessage(string action, string channel, FuturesLiquidationMessageData data)
            : base(action, channel, data)
        {
        }

        internal static bool TryParse(
            IHuobiSerializer serializer,
            string input,
            [MaybeNullWhen(false)] out FuturesLiquidationMessage response)
        {
            var result = serializer.TryDeserializeIfContains(
                input,
                AccountSubscriptionType.Liquidations.ToTopicId(),
                "\"code\"",
                out response);

            return result;
        }
    }
}