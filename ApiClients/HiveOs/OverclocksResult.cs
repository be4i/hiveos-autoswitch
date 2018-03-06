using Newtonsoft.Json;

namespace HiveOsAutomation.ApiClients.HiveOs
{
    public class OverclocksResult
    {
         [JsonProperty("id_wal")]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}