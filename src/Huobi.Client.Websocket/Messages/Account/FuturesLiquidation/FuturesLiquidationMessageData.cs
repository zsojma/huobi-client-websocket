using System;
using Newtonsoft.Json;

namespace Huobi.Client.Websocket.Messages.Account.FuturesLiquidation
{
    public class FuturesLiquidationMessageData
    {
        [JsonConstructor]
        public FuturesLiquidationMessageData(
            string symbol,
            string contract_code,
            string direction,
            string offset,
            decimal volume,
            decimal price,
            long created_at,
            decimal amount
        )
        {
            Symbol = symbol;
            Contract_code = contract_code;
            Direction = direction;
            Offset = offset;
            Volume = volume;
            Price = price;
            Created_at = created_at;
            Amount = amount;
        }

        public string Symbol { get; }
        public string Contract_code { get; }
        public string Direction { get; }
        public string Offset { get; }
        public decimal Volume { get; }
        public decimal Price { get; }
        public long Created_at { get; }
        public decimal Amount { get; }
    }
}