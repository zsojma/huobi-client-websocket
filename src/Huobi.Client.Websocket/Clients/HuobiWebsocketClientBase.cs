using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Huobi.Client.Websocket.Clients.Streams;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Messages.Account;
using Huobi.Client.Websocket.Messages.MarketData;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging;
using Websocket.Client;

namespace Huobi.Client.Websocket.Clients
{
    public abstract class HuobiWebsocketClientBase<TStreams> : IDisposable
        where TStreams : HuobiClientStreamsBase, new()
    {
        private readonly IHuobiWebsocketCommunicator _communicator;
        private readonly ILogger<HuobiWebsocketClientBase<TStreams>> _logger;

        private readonly IDisposable _messageReceivedSubscription;

        protected HuobiWebsocketClientBase(
            IHuobiWebsocketCommunicator communicator,
            IHuobiSerializer serializer,
            ILogger<HuobiWebsocketClientBase<TStreams>> logger)
        {
            _communicator = communicator;
            Serializer = serializer;
            _logger = logger;

            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);
        }

        protected IHuobiSerializer Serializer { get; }

        [NotNull]
        public TStreams Streams { get; } = new();

        public void Dispose()
        {
            _messageReceivedSubscription.Dispose();
        }

        public virtual Task Start()
        {
            return _communicator.Start();
        }

        public void Send(string request)
        {
            try
            {
                _logger.LogDebug($"Sending client request: {request}");
                _communicator.Send(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception while sending client request: '{request}'. Error: {ex.Message}");
                throw;
            }
        }

        protected abstract bool TryHandleMessage(string message);

        private void HandleMessage(ResponseMessage responseMessage)
        {
            var message = ParseMessage(responseMessage);

            try
            {
                var processed = false;
                if (message.StartsWith("{"))
                {
                    processed = TryHandleServerPingRequest(message)
                             || TryHandleMessage(message)
                             || TryHandleErrorMessage(message);
                }

                if (!processed)
                {
                    Streams.UnhandledMessageSubject.OnNext(message);
                    _logger.LogError($"Unhandled message received: {message}");
                }
            }
            catch (Exception e)
            {
                Streams.UnhandledMessageSubject.OnNext(message);
                _logger.LogError(e, "Exception while processing of response message");
            }
        }

        private bool TryHandleServerPingRequest(string message)
        {
            if (PingRequest.TryParse(Serializer, message, out var pingRequest))
            {
                Streams.PingMessageSubject.OnNext(pingRequest);
                RespondWithPong(pingRequest);
                return true;
            }

            if (AuthPingRequest.TryParse(Serializer, message, out var pingAuthRequest))
            {
                Streams.PingAuthMessageSubject.OnNext(pingAuthRequest);
                RespondWithPong(pingAuthRequest);
                return true;
            }

            return false;
        }

        private bool TryHandleErrorMessage(string message)
        {
            if (ErrorMessage.TryParse(Serializer, message, out var errorMessage))
            {
                Streams.ErrorMessageSubject.OnNext(errorMessage);
                return true;
            }

            if (AuthErrorMessage.TryParse(Serializer, message, out var authErrorMessage))
            {
                Streams.AuthErrorMessageSubject.OnNext(authErrorMessage);
                return true;
            }

            return false;
        }

        private void RespondWithPong(PingRequest pingRequest)
        {
            var clientResponse = new PongResponse(pingRequest.Value);
            var serialized = Serializer.Serialize(clientResponse);
            Send(serialized);
        }

        private void RespondWithPong(AuthPingRequest authPingRequest)
        {
            var clientResponse = new AuthPongResponse(authPingRequest.Data.Timestamp);
            var serialized = Serializer.Serialize(clientResponse);
            Send(serialized);
        }

        private string ParseMessage(ResponseMessage message)
        {
            try
            {
                if (message.MessageType == WebSocketMessageType.Binary)
                {
                    return Decompress(message.Binary);
                }

                return message.Text?.Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while parsing response message. Error: " + ex.Message);
                return string.Empty;
            }
        }

        private static string Decompress(byte[] input)
        {
            using var inputStream = new MemoryStream(input);
            using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);

            using var outputStream = new MemoryStream();
            gZipStream.CopyTo(outputStream);

            var decompressed = Encoding.UTF8.GetString(outputStream.ToArray());
            return decompressed;
        }
    }
}