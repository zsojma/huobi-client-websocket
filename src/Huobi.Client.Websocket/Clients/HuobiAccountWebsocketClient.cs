using System.Threading.Tasks;
using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Clients.Streams;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.Account.AccountUpdates;
using Huobi.Client.Websocket.Messages.Account.OrderUpdates;
using Huobi.Client.Websocket.Messages.Account.TradeDetails;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Huobi.Client.Websocket.Clients
{
    public class HuobiAccountWebsocketClient : HuobiWebsocketClientBase<HuobiAccountClientStreams>, IHuobiAccountWebsocketClient
    {
        private readonly IOptions<HuobiAccountWebsocketClientConfig> _config;
        private readonly IHuobiAuthenticationRequestFactory _authenticationRequestFactory;

        public HuobiAccountWebsocketClient(
            IOptions<HuobiAccountWebsocketClientConfig> config,
            IHuobiAccountWebsocketCommunicator communicator,
            IHuobiSerializer serializer,
            IHuobiAuthenticationRequestFactory authenticationRequestFactory,
            ILogger<HuobiAccountWebsocketClient> logger)
            : base(communicator, serializer, logger)
        {
            _config = config;
            _authenticationRequestFactory = authenticationRequestFactory;
        }

        public override async Task Start()
        {
            await base.Start();
            Authenticate();
        }

        public void Send(AccountRequestBase request)
        {
            var serialized = Serializer.Serialize(request);
            Send(serialized);
        }

        protected override bool TryHandleMessage(string message)
        {
            return TryHandleTradeDetailsMessages(message)
                || TryHandleOrderUpdateMessages(message)
                || TryHandleAccountUpdateMessages(message)
                || TryHandleSubscribeResponses(message)
                || TryHandleAuthenticationResponses(message);
        }

        private void Authenticate()
        {
            var request = _authenticationRequestFactory.CreateRequest(_config.Value);
            Send(request);
        }

        private bool TryHandleTradeDetailsMessages(string message)
        {
            if (TradeDetailsMessage.TryParse(Serializer, message, out var tradeDetailsMessage))
            {
                Streams.TradeDetailsMessageSubject.OnNext(tradeDetailsMessage);
                return true;
            }

            return false;
        }

        private bool TryHandleOrderUpdateMessages(string message)
        {
            if (ConditionalOrderTriggerFailureMessage.TryParse(
                Serializer,
                message,
                out var conditionalOrderTriggerFailureMessage))
            {
                Streams.ConditionalOrderTriggerFailureMessageSubject.OnNext(conditionalOrderTriggerFailureMessage);
                return true;
            }

            if (ConditionalOrderCanceledMessage.TryParse(Serializer, message, out var conditionalOrderCanceledMessage))
            {
                Streams.ConditionalOrderCanceledMessageSubject.OnNext(conditionalOrderCanceledMessage);
                return true;
            }

            if (OrderSubmittedMessage.TryParse(Serializer, message, out var conditionalSubmittedMessage))
            {
                Streams.OrderSubmittedMessageSubject.OnNext(conditionalSubmittedMessage);
                return true;
            }

            if (OrderTradedMessage.TryParse(Serializer, message, out var orderTradedMessage))
            {
                Streams.OrderTradedMessageSubject.OnNext(orderTradedMessage);
                return true;
            }

            if (OrderCanceledMessage.TryParse(Serializer, message, out var orderCanceledMessage))
            {
                Streams.OrderCanceledMessageSubject.OnNext(orderCanceledMessage);
                return true;
            }

            return false;
        }
        
        private bool TryHandleAccountUpdateMessages(string message)
        {
            if (AccountUpdateMessage.TryParse(Serializer, message, out var accountUpdateMessage))
            {
                Streams.AccountUpdateMessageSubject.OnNext(accountUpdateMessage);
                return true;
            }

            return false;
        }

        private bool TryHandleSubscribeResponses(string message)
        {
            if (AccountSubscribeResponse.TryParse(Serializer, message, out var subscribeResponse))
            {
                Streams.SubscribeResponseSubject.OnNext(subscribeResponse);
                return true;
            }

            return false;
        }

        private bool TryHandleAuthenticationResponses(string message)
        {
            if (AuthenticationResponse.TryParse(Serializer, message, out var authenticationResponse))
            {
                Streams.AuthenticationResponseSubject.OnNext(authenticationResponse);
                return true;
            }

            return false;
        }
    }
}