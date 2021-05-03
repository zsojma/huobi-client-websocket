using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Huobi.Client.Websocket.Messages.Account.Values;
using Huobi.Client.Websocket.Serializer.Converters;
using Newtonsoft.Json;
using Xunit;

namespace Huobi.Client.Websocket.Tests
{
    public class HuobiEnumJsonConverterTests
    {
        [Theory]
        [ClassData(typeof(OrderEventTypeTestData))]
        public void OrderEventType_ValidInput_Parsed(string input)
        {
            // Arrange
            var converter = new HuobiEnumJsonConverter();
            var jsonReader = CreateAndPrepareJsonReader(input);

            // Act
            var output = converter.ReadJson(jsonReader, typeof(OrderEventType), null, null!);

            // Assert
            Assert.NotNull(output);
            Assert.NotEqual(OrderEventType.Unknown, output);
        }

        [Theory]
        [ClassData(typeof(OrderSideTestData))]
        public void OrderSide_ValidInput_Parsed(string input)
        {
            // Arrange
            var converter = new HuobiEnumJsonConverter();
            var jsonReader = CreateAndPrepareJsonReader(input);

            // Act
            var output = converter.ReadJson(jsonReader, typeof(OrderSide), null, null!);

            // Assert
            Assert.NotNull(output);
            Assert.NotEqual(OrderSide.Unknown, output);
        }

        [Theory]
        [ClassData(typeof(OrderStatusTestData))]
        public void OrderStatus_ValidInput_Parsed(string input)
        {
            // Arrange
            var converter = new HuobiEnumJsonConverter();
            var jsonReader = CreateAndPrepareJsonReader(input);

            // Act
            var output = converter.ReadJson(jsonReader, typeof(OrderStatus), null, null!);

            // Assert
            Assert.NotNull(output);
            Assert.NotEqual(OrderStatus.Unknown, output);
        }

        [Theory]
        [ClassData(typeof(OrderTypeTestData))]
        public void OrderType_ValidInput_Parsed(string input)
        {
            // Arrange
            var converter = new HuobiEnumJsonConverter();
            var jsonReader = CreateAndPrepareJsonReader(input);

            // Act
            var output = converter.ReadJson(jsonReader, typeof(OrderType), null, null!);

            // Assert
            Assert.NotNull(output);
            Assert.NotEqual(OrderType.Unknown, output);
        }

        [Theory]
        [ClassData(typeof(TradeEventTypeTestData))]
        public void TradeEventType_ValidInput_Parsed(string input)
        {
            // Arrange
            var converter = new HuobiEnumJsonConverter();
            var jsonReader = CreateAndPrepareJsonReader(input);

            // Act
            var output = converter.ReadJson(jsonReader, typeof(TradeEventType), null, null!);

            // Assert
            Assert.NotNull(output);
            Assert.NotEqual(TradeEventType.Unknown, output);
        }

        [Theory]
        [ClassData(typeof(AccountChangeTypeTestData))]
        public void AccountChangeType_ValidInput_Parsed(string input)
        {
            // Arrange
            var converter = new HuobiEnumJsonConverter();
            var jsonReader = CreateAndPrepareJsonReader(input);

            // Act
            var output = converter.ReadJson(jsonReader, typeof(AccountChangeType), null, null!);

            // Assert
            Assert.NotNull(output);
            Assert.NotEqual(AccountChangeType.Unknown, output);
        }

        [Theory]
        [ClassData(typeof(AccountTypeTestData))]
        public void AccountType_ValidInput_Parsed(string input)
        {
            // Arrange
            var converter = new HuobiEnumJsonConverter();
            var jsonReader = CreateAndPrepareJsonReader(input);

            // Act
            var output = converter.ReadJson(jsonReader, typeof(AccountType), null, null!);

            // Assert
            Assert.NotNull(output);
            Assert.NotEqual(AccountType.Unknown, output);
        }

        private static JsonTextReader CreateAndPrepareJsonReader(string input)
        {
            var jsonReader = new JsonTextReader(new StringReader($@"{{ ""value"": ""{input}"" }}"));
            jsonReader.Read(); // start object
            jsonReader.Read(); // property name
            jsonReader.Read(); // value
            return jsonReader;
        }
    }

    public abstract class EnumTestDataBase : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            return GetValues()
                .Select(
                    item => new object[]
                    {
                        item
                    })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static bool EqualsWithString<T>(string input, T type)
            where T : Enum
        {
            return string.Equals(input.Replace("-", ""), type.ToString(), StringComparison.OrdinalIgnoreCase);
        }

        public abstract IEnumerable<string> GetValues();
    }

    public class OrderEventTypeTestData : EnumTestDataBase
    {
        public override IEnumerable<string> GetValues()
        {
            yield return "trigger";
            yield return "deletion";
            yield return "creation";
            yield return "trade";
            yield return "cancellation";
        }
    }

    public class OrderSideTestData : EnumTestDataBase
    {
        public override IEnumerable<string> GetValues()
        {
            yield return "buy";
            yield return "sell";
        }
    }

    public class OrderStatusTestData : EnumTestDataBase
    {
        public override IEnumerable<string> GetValues()
        {
            yield return "rejected";
            yield return "partial-canceled";
            yield return "canceled";
            yield return "submitted";
            yield return "partial-filled";
            yield return "filled";
        }
    }

    public class OrderTypeTestData : EnumTestDataBase
    {
        public override IEnumerable<string> GetValues()
        {
            yield return "buy-market";
            yield return "sell-market";
            yield return "buy-limit";
            yield return "sell-limit";
            yield return "buy-limit-maker";
            yield return "sell-limit-maker";
            yield return "buy-ioc";
            yield return "sell-ioc";
            yield return "buy-limit-fok";
            yield return "sell-limit-fok";
            yield return "buy-stop-limit";
            yield return "sell-stop-limit";
            yield return "buy-stop-limit-fok";
            yield return "sell-stop-limit-fok";
        }
    }

    public class TradeEventTypeTestData : EnumTestDataBase
    {
        public override IEnumerable<string> GetValues()
        {
            yield return "trade";
            yield return "cancellation";
        }
    }

    public class AccountChangeTypeTestData : EnumTestDataBase
    {
        public override IEnumerable<string> GetValues()
        {
            yield return "order-place";
            yield return "order-match";
            yield return "order-refund";
            yield return "order-cancel";
            yield return "order-fee-refund";
            yield return "margin-transfer";
            yield return "margin-loan";
            yield return "margin-interest";
            yield return "margin-repay";
            yield return "deposit";
            yield return "withdraw";
            yield return "other";
        }
    }

    public class AccountTypeTestData : EnumTestDataBase
    {
        public override IEnumerable<string> GetValues()
        {
            yield return "trade";
            yield return "loan";
            yield return "interest";
        }
    }
}