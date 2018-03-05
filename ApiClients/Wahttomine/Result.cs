using System;

namespace HiveOsAutomation.ApiClients.Whattomine
{
    public class Result
    {
        public int Id { get; set; }

        public string Tag { get; set; }

        public eAlgorithm Algorithm { get; set; }

        public string BlockTime { get; set; }

        public decimal BlockReward { get; set; }

        public decimal BlockReward24 { get; set; }

        public long LastBlock { get; set; }

        public decimal Difficulty { get; set; }

        public decimal Difficulty24 { get; set; }

        public string Nethash { get; set; }

        public decimal ExchangeRate { get; set; }

        public decimal ExchangeRate24 { get; set; }

        public decimal ExchangeRateVolume { get; set; }

        public string ExchangeRateCurrency { get; set; }

        public string MarketCap { get; set; }

        public decimal EstimatedRewards { get; set; }

        public decimal EstimatedRewards24 { get; set; }

        public decimal BtcRevenue { get; set; }

        public decimal BtcRevenue24 { get; set; }

        public int Profitability { get; set; }

        public int Profitability24 { get; set; }

        public bool Lagging { get; set; }

        public DateTime Timestamp { get; set; }
    }
}