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

namespace Huobi.Client.Websocket.ComponentTests.MessagesHandling
{
    public class ClientMessagesHandlingTestsBase
    {
        internal Subject<ResponseMessage> ResponseMessageSubject { get; private set; } = null!;
        internal Mock<IHuobiWebsocketCommunicator> CommunicatorMock { get; private set; } = null!;
        internal Mock<IObserver<string>> UnhandledMessageObserverMock { get; private set; } = null!;

        internal HuobiMarketWebsocketClient InitializeMarketClient()
        {
            InitializeBase();

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);
            var client = new HuobiMarketWebsocketClient(
                CommunicatorMock.Object,
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
                CommunicatorMock.Object,
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
                CommunicatorMock.Object,
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
            ResponseMessageSubject = new Subject<ResponseMessage>();

            CommunicatorMock = new Mock<IHuobiWebsocketCommunicator>();
            CommunicatorMock.Setup(m => m.MessageReceived).Returns(ResponseMessageSubject);

            UnhandledMessageObserverMock = new Mock<IObserver<string>>();
        }
    }
}