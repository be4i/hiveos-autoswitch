using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using RestSharp;

public class HiveOsClient
{
    private readonly string _url = "https://api.hiveos.farm/worker/eypiay.php";
    private readonly string _publicKey = "631737cb7723a076dd3e04901a5fddb0d2e73144d474b7b963457236e19e9ba6";
    private readonly string _secretKey = "ba27dec27996a20a59f61617eb89122ead71c82ae8821fb1638d9b69f290d133";

    public HiveOsClient()
    {
        
    }

    public void GetWallets()
    {
        //Request(new string[] { "" });
    }

    private void Request(Dictionary<string, string> payload)
    {
        var client = new RestClient(_url);
        var request = new RestRequest("", Method.POST);

        request.AddParameter("public_key", _publicKey);

        foreach (var item in payload)
        {
            request.AddParameter(item.Key, item.Value);
        }
    }

    // private static string SignRequest(RestRequest request)
    // {
    //     var encoding = new ASCIIEncoding();
    //     var textBytes = encoding.GetBytes(text);
    //     var keyBytes = encoding.GetBytes(key);

    //     var hash = new HMACSHA256(keyBytes);
    //     var hashBytes = hash.ComputeHash(textBytes);

    //     return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    // }
}