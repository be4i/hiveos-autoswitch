using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using RestSharp;

public class WhatToMineClient
{
    string _url = "http://whattomine.com/coins.json";
    
    public void Request(IEnumerable<AlgorithmParams> coins)
    {
        var client = new RestClient(_url);
        var request = new RestRequest("", Method.GET);
        foreach(var coin in coins) {
            var keyValue = coin.GetQueryParameter();
            request.AddParameter(keyValue.Key, coin.HashRate);
            request.AddParameter(keyValue.Value, coin.PowerConsumtion);
        }

        request.AddParameter("eth", "true");
        request.AddParameter("cost", "cost");
        request.AddParameter("sort", "Profitability24");
        request.AddParameter("revenue", "7d");
        request.AddParameter("volume", "0");
        request.AddParameter("factor[exchanges][]","");
        request.AddParameter("factor[exchanges][]", "abucoins");
        request.AddParameter("factor[exchanges][]", "bitfinex");
        request.AddParameter("factor[exchanges][]", "bittrex");
        request.AddParameter("factor[exchanges][]", "binance");
        request.AddParameter("factor[exchanges][]", "cryptopia");
        request.AddParameter("factor[exchanges][]", "hitbtc");
        request.AddParameter("factor[exchanges][]", "poloniex");
        request.AddParameter("factor[exchanges][]", "yobit");
        request.AddParameter("dataset", "Main");
        request.AddParameter("commit", "Calculate");
        var response = client.Execute(request);
        Console.Write(response.Content);
    }
}