using System;
using System.IO;
using System.IO.Compression;
using System.Reactive.Subjects;
using System.Text;
using Huobi.Client.Websocket.Authentication;
using Huobi.Client.Websocket.Clients;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Config;
using Huobi.Client.Websocket.Serializer;
using Huobi.Client.Websocket.Serializer.Converters;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using Websocket.Client;
using Websocket.Client.Models;

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class ClientMessagesHandlingTestsBase
    {
        internal Subject<ResponseMessage> ResponseMessageSubject { get; private set; } = null!;
        internal Mock<IHuobiGenericWebsocketCommunicator> GenericCommunicatorMock { get; private set; } = null!;
        internal Mock<IHuobiMarketWebsocketCommunicator> MarketCommunicatorMock { get; private set; } = null!;
        internal Mock<IHuobiMarketByPriceWebsocketCommunicator> MarketByPriceCommunicatorMock { get; private set; } = null!;
        internal Mock<IHuobiAccountWebsocketCommunicator> AccountCommunicatorMock { get; private set; } = null!;
        internal Mock<IObserver<string>> UnhandledMessageObserverMock { get; private set; } = null!;

        internal HuobiGenericWebsocketClient InitializeGenericClient()
        {
            InitializeBase();

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var client = new HuobiGenericWebsocketClient(
                GenericCommunicatorMock.Object,
                serializer,
                NullLogger<HuobiGenericWebsocketClient>.Instance);

            client.Streams.UnhandledMessageStream.Subscribe(UnhandledMessageObserverMock.Object);
            return client;
        }

        internal HuobiMarketWebsocketClient InitializeMarketClient()
        {
            InitializeBase();

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var client = new HuobiMarketWebsocketClient(
                MarketCommunicatorMock.Object,
                serializer,
                NullLogger<HuobiMarketWebsocketClient>.Instance);

            client.Streams.UnhandledMessageStream.Subscribe(UnhandledMessageObserverMock.Object);
            return client;
        }

        internal HuobiMarketByPriceWebsocketClient InitializeMarketByPriceClient()
        {
            InitializeBase();

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var client = new HuobiMarketByPriceWebsocketClient(
                MarketByPriceCommunicatorMock.Object,
                serializer,
                NullLogger<HuobiMarketByPriceWebsocketClient>.Instance);

            client.Streams.UnhandledMessageStream.Subscribe(UnhandledMessageObserverMock.Object);
            return client;
        }

        internal HuobiAccountWebsocketClient InitializeAccountClient()
        {
            InitializeBase();

            var config = Options.Create(new HuobiAccountWebsocketClientConfig());
            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var authFactoryMock = new Mock<IHuobiAuthenticationRequestFactory>();
            var client = new HuobiAccountWebsocketClient(
                config,
                AccountCommunicatorMock.Object,
                serializer,
                authFactoryMock.Object,
                NullLogger<HuobiAccountWebsocketClient>.Instance);

            client.Streams.UnhandledMessageStream.Subscribe(UnhandledMessageObserverMock.Object);
            return client;
        }

        internal void TriggerMessageReceive(object message)
        {
            var compressed = Compress(message);
            var binary = ResponseMessage.BinaryMessage(compressed);
            ResponseMessageSubject.OnNext(binary);
        }

        internal void TriggerMessageReceive(string message)
        {
            var compressed = Compress(message);
            var binary = ResponseMessage.BinaryMessage(compressed);
            ResponseMessageSubject.OnNext(binary);
        }

        internal void VerifyMessageNotUnhandled()
        {
            UnhandledMessageObserverMock.Verify(m => m.OnNext(It.IsAny<string>()), Times.Never);
        }

        internal static byte[] Compress(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var inputStream = new MemoryStream(bytes);

            using var outputStream = new MemoryStream(bytes.Length);
            using var gZipStream = new GZipStream(outputStream, CompressionMode.Compress);

            inputStream.CopyTo(gZipStream);
            gZipStream.Flush();

            outputStream.Position = 0;
            var output = outputStream.ToArray();
            return output;
        }

        private static byte[] Compress(object input)
        {
            var serialized = JsonConvert.SerializeObject(
                input,
                new HuobiEnumJsonConverter(),
                new UnixMillisecondsToDateTimeOffsetConverter());
            return Compress(serialized);
        }

        private void InitializeBase()
        {
            GenericCommunicatorMock = new Mock<IHuobiGenericWebsocketCommunicator>();
            MarketCommunicatorMock = new Mock<IHuobiMarketWebsocketCommunicator>();
            MarketByPriceCommunicatorMock = new Mock<IHuobiMarketByPriceWebsocketCommunicator>();
            AccountCommunicatorMock = new Mock<IHuobiAccountWebsocketCommunicator>();
            
            ResponseMessageSubject = new Subject<ResponseMessage>();
            var reconnectionHappenedSubject = new Subject<ReconnectionInfo>();
            var disconnectionHappenedSubject = new Subject<DisconnectionInfo>();

            GenericCommunicatorMock.Setup(m => m.MessageReceived).Returns(ResponseMessageSubject);
            GenericCommunicatorMock.Setup(m => m.ReconnectionHappened).Returns(reconnectionHappenedSubject);
            GenericCommunicatorMock.Setup(m => m.DisconnectionHappened).Returns(disconnectionHappenedSubject);

            MarketCommunicatorMock.Setup(m => m.MessageReceived).Returns(ResponseMessageSubject);
            MarketCommunicatorMock.Setup(m => m.ReconnectionHappened).Returns(reconnectionHappenedSubject);
            MarketCommunicatorMock.Setup(m => m.DisconnectionHappened).Returns(disconnectionHappenedSubject);

            MarketByPriceCommunicatorMock.Setup(m => m.MessageReceived).Returns(ResponseMessageSubject);
            MarketByPriceCommunicatorMock.Setup(m => m.ReconnectionHappened).Returns(reconnectionHappenedSubject);
            MarketByPriceCommunicatorMock.Setup(m => m.DisconnectionHappened).Returns(disconnectionHappenedSubject);

            AccountCommunicatorMock.Setup(m => m.MessageReceived).Returns(ResponseMessageSubject);
            AccountCommunicatorMock.Setup(m => m.ReconnectionHappened).Returns(reconnectionHappenedSubject);
            AccountCommunicatorMock.Setup(m => m.DisconnectionHappened).Returns(disconnectionHappenedSubject);

            UnhandledMessageObserverMock = new Mock<IObserver<string>>();
        }
    }
}