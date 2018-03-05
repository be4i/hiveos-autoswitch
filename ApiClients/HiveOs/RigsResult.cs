using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HiveOsAutomation.ApiClients.HiveOs
{
    public class RigsResult
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public bool Active { get; set; }

        public long WalletId { get; set; }

        public int GpuCount { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        public eMinerSoftware Miner { get; set; }

        public eMinerSoftware Miner2 { get; set; }
    }
}