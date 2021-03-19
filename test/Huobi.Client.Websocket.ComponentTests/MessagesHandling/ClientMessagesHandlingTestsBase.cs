﻿using System;
using System.IO;
using System.IO.Compression;
using System.Reactive.Subjects;
using System.Text;
using Huobi.Client.Websocket.Client;
using Huobi.Client.Websocket.Communicator;
using Huobi.Client.Websocket.Serializer;
using Microsoft.Extensions.Logging.Abstractions;
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

        internal HuobiWebsocketClient Initialize()
        {
            ResponseMessageSubject = new Subject<ResponseMessage>();

            CommunicatorMock = new Mock<IHuobiWebsocketCommunicator>();
            CommunicatorMock.Setup(m => m.MessageReceived).Returns(ResponseMessageSubject);

            UnhandledMessageObserverMock = new Mock<IObserver<string>>();

            var serializer = new HuobiSerializer(NullLogger<HuobiSerializer>.Instance);

            var client = new HuobiWebsocketClient(
                CommunicatorMock.Object,
                serializer,
                NullLogger<HuobiWebsocketClient>.Instance);

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

        internal static byte[] Compress(object input)
        {
            var serialized = JsonConvert.SerializeObject(input);
            return Compress(serialized);
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
    }
}