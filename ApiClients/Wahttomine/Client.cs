using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HiveOsAutomation.ApiClients.Whattomine
{
    public class Client
    {
        private readonly string _endPoint;
        private readonly string[] _exchanges;
        
        public Client(string endPoint, string[] exchanges)
        {
            _endPoint = endPoint;
            _exchanges = exchanges;
        }

        public IEnumerable<Result> Get(IEnumerable<AlgorithmParams> algorithms)
        {
            var client = new RestClient(_endPoint);
            var request = new RestRequest("", Method.GET);

            foreach(var algo in algorithms) 
            {
                var keyValue = algo.Algorithm.GetDisplayNameAndGroupName();

                request.AddParameter($"factor[{keyValue.Key}_hr]" , algo.HashRate);
                request.AddParameter($"factor[{keyValue.Key}_p]", algo.PowerConsumtion);
                request.AddParameter(keyValue.Key, "true");
            }

            // request.AddParameter("factor[exchanges][]", "abucoins");
            // request.AddParameter("factor[exchanges][]", "bitfinex");
            // request.AddParameter("factor[exchanges][]", "bittrex");
            // request.AddParameter("factor[exchanges][]", "binance");
            // request.AddParameter("factor[exchanges][]", "cryptopia");
            // request.AddParameter("factor[exchanges][]", "hitbtc");
            // request.AddParameter("factor[exchanges][]", "poloniex");
            // request.AddParameter("factor[exchanges][]", "yobit");

            foreach(var exhcange in _exchanges)
            {
                request.AddParameter("factor[exchanges][]", exhcange);
            }

            var response = client.Execute(request);
            
            if(!response.IsSuccessful)
            {
                throw response.ErrorException;
            }

            var coins = JObject.Parse(response.Content)["coins"];

            foreach (var coin in coins)
            {
                var props = coin.First;

                yield return new Result
                {
                    Algorithm = (eAlgorithm)Enum.Parse(typeof(eAlgorithm), props["algorithm"].Value<string>()),
                    BlockReward = props["block_reward"].Value<decimal>(),
                    Id = props["id"].Value<int>(),
                    Tag = props["tag"].Value<string>(),
                    BlockTime = props["block_time"].Value<string>(),
                    BlockReward24 = props["block_reward"].Value<decimal>(),
                    LastBlock = props["last_block"].Value<long>(),
                    Difficulty = props["difficulty"].Value<decimal>(),
                    Difficulty24 = props["difficulty24"].Value<decimal>(),
                    Nethash = props["nethash"].Value<string>(),
                    ExchangeRate = props["exchange_rate"].Value<decimal>(),
                    ExchangeRate24 = props["exchange_rate24"].Value<decimal>(),
                    ExchangeRateVolume = props["exchange_rate_vol"].Value<decimal>(),
                    ExchangeRateCurrency = props["exchange_rate_curr"].Value<string>(),
                    MarketCap = props["market_cap"].Value<string>(),
                    EstimatedRewards = props["estimated_rewards"].Value<decimal>(),
                    EstimatedRewards24 = props["estimated_rewards24"].Value<decimal>(),
                    BtcRevenue = props["btc_revenue"].Value<decimal>(),
                    BtcRevenue24 = props["btc_revenue24"].Value<decimal>(),
                    Profitability = props["profitability"].Value<int>(),
                    Profitability24 = props["profitability24"].Value<int>(),
                    Lagging = props["lagging"].Value<bool>(),
                    Timestamp = DateTimeOffset.FromUnixTimeSeconds(props["timestamp"].Value<int>()).LocalDateTime,
                };
            }
        }
    }
}