using System;
using Huobi.Client.Websocket.Authentication;
using NodaTime;
using Xunit;

namespace Huobi.Client.Websocket.Tests
{
    public class HuobiAuthenticationTests
    {
        [Fact]
        public void CreateSignature()
        {
            // Arrange
            var authentication = new HuobiAuthentication();
            var timestamp = new ZonedDateTime(
                Instant.FromDateTimeUtc(new DateTime(2021, 03, 30, 12, 13, 14, DateTimeKind.Utc)),
                DateTimeZone.Utc);

            // Act
            var signature = authentication.GenerateSignature(
                "123",
                "456",
                "api.huobi.pro",
                "/ws/v2",
                timestamp);

            // Assert
            Assert.Contains("J7Q1UdY5zVyh2xjUbPK2+n0elG+cb2DAeTU+mK4MeuE=", signature);
        }
    }
}