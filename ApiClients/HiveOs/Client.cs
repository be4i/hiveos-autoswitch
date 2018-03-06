using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HiveOsAutomation.ApiClients.HiveOs;

namespace HiveOsAutomation.ApiClients.HiveOs
{
    public class Client
    {
        private readonly string _endPoint;
        private readonly string _publicKey;
        private readonly string _secretKey;

        public Client(string endPoint, string publicKey, string secretKey)
        {
            _endPoint = endPoint;
            _publicKey = publicKey;
            _secretKey = secretKey;
        }

        public IEnumerable<RigsResult> GetRigs()
        {
            var payload = new Dictionary<string, string>();

            payload.Add("method", "getRigs");

            var response = Request(payload);

            if(!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }

            var result = JObject.Parse(response.Content)["result"];

            foreach (var item in result)
            {
                var props = item.First();

                var rig = new RigsResult
                {
                    Id = props["id_rig"].Value<long>(),
                    Name = props["name"].Value<string>(),
                    Active = props["active"].Value<bool>(),
                    Description = props["description"].Value<string>(),
                    GpuCount = props["gpu_count"].Value<int>(),
                    WalletId = props["id_wal"].Value<int>(),
                    Password = props["passwd"].Value<string>(),
                    Miner = props["miner"].Value<string>().GetMinerSoftware(),
                    Miner2 = props["miner2"].Value<string>().GetMinerSoftware(),
                };
                
                yield return rig;
            }
        }

        public IEnumerable<WalletsResult> GetWallets()
        {
            var payload = new Dictionary<string, string>();

            payload.Add("method", "getWallets");

            var response = Request(payload);

            if(!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            var result = JObject.Parse(response.Content)["result"];

            foreach(var item in result)
            {
                var wallet = item.First;

                yield return wallet.ToObject<WalletsResult>();
            }
        }

        public IEnumerable<OverclocksResult> GetOverclocks()
        {
            var payload = new Dictionary<string, string>();

            payload.Add("method", "getOC");

            var response = Request(payload);

            if(!response.IsSuccessful)
            {
                throw new Exception(response.Content);
            }

            var result = JObject.Parse(response.Content)["result"];

            foreach(var item in result)
            {
                var wallet = item.First;

                yield return wallet.ToObject<OverclocksResult>();
            }
        }

        public bool MultiRocket(IEnumerable<long> rigIds, eMinerSoftware? miner = null, eMinerSoftware? miner2 = null, int? walletId = null, int? overClockId = null)
        {
            var payload = new Dictionary<string, string>();
            
            payload.Add("method", "multiRocket");
            payload.Add("rig_ids_str", string.Join(",", rigIds.Select(a => a.ToString())));

            if(miner.HasValue)
            {
                payload.Add("miner", miner.GetDisplayNameAndGroupName().Key);
            }

            if(walletId.HasValue)
            {
                payload.Add("id_wal", walletId.ToString());
            }

            if(overClockId.HasValue)
            {
                payload.Add("id_oc", overClockId.ToString());
            }

            if(miner2.HasValue)
            {
                payload.Add("miner2", miner2.GetDisplayNameAndGroupName().Key);
            }

            var response = Request(payload);

            return response.IsSuccessful;
        }

        private IRestResponse Request(Dictionary<string, string> payload)
        {
            var client = new RestClient(_endPoint);
            var request = new RestRequest("", Method.POST);

            request.AddParameter("public_key", _publicKey);

            foreach (var item in payload)
            {
                request.AddParameter(item.Key, item.Value);
            }

            request.AddHeader("HMAC", SignRequest(request));

            return client.Execute(request);
        }

        private string SignRequest(RestRequest request)
        {
            var builder = new StringBuilder();

            foreach (var param in request.Parameters)
            {
                builder.AppendFormat("{2}{0}={1}", WebUtility.UrlEncode(param.Name), WebUtility.UrlEncode(param.Value.ToString()), builder.Length > 0 ? "&" : "");
            }

            var payload = builder.ToString();
            var encoding = new UTF8Encoding();
            var textBytes = encoding.GetBytes(payload);
            var keyBytes = encoding.GetBytes(_secretKey);

            var hash = new HMACSHA256(keyBytes);
            var hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}