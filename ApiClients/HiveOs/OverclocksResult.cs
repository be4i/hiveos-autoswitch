using Newtonsoft.Json;

namespace HiveOsAutomation.ApiClients.HiveOs
{
    public class OverclocksResult
    {
         [JsonProperty("id_oc")]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}