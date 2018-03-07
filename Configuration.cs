using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HiveOsAutomation
{
    public class Configuration
    {
        [JsonProperty("hiveos_api_endpoint")]
        public string HiveOsApiEndPoint { get; set; }

        [JsonProperty("hiveos_api_public_key")]
        public string HiveOsApiPublicKey { get; set; }

        [JsonProperty("hiveos_api_secret_key")]
        public string HiveOsApiSecretKey { get; set; }

        [JsonProperty("whattomine_api_endpoint")]
        public string WhattomineApiEndPoint { get; set; }

        [JsonProperty("whattomine_api_exchanges")]
        public string[] WhattomineApiExchanges {get; set; }

        public IEnumerable<RigConfiguration> Rigs { get; set; }
    }

    public class RigConfiguration
    {
        public string[] Names { get; set; }

        public IEnumerable<RigAlgorithmConfiguration> Algorithms { get; set; }
    }

    public class RigAlgorithmConfiguration
    {
        public eAlgorithm Type { get; set; }

        public decimal Hashrate { get; set; }

        [JsonProperty("power_consumption")]
        public decimal PowerConsumption { get; set; }

        [JsonProperty("overclock")]
        public string Overclock { get; set; }

        public IEnumerable<RigAlgorithmTag> Tags  { get; set; }
    }

    public class RigAlgorithmTag
    {
        public string[] Names { get; set; }

        [JsonProperty("wallet")]
        public string Wallet { get; set; }

        public eMinerSoftware Miner { get; set; }
    }
}