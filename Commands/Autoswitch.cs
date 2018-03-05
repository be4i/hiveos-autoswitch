using System.Collections.Generic;
using System.Linq;
using HiveOsAutomation.ApiClients.Whattomine;
using McMaster.Extensions.CommandLineUtils;

namespace HiveOsAutomation.Commands
{
    public class AutoSwitch
    {
        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            var hiveOsApiClient = new HiveOsAutomation.ApiClients.HiveOs.Client(Program.Configuration.HiveOsApiEndPoint, Program.Configuration.HiveOsApiPublicKey, Program.Configuration.HiveOsApiSecretKey);
            var whattomineApiClient = new HiveOsAutomation.ApiClients.Whattomine.Client(Program.Configuration.WhattomineApiEndPoint);

            var rigsStatus = hiveOsApiClient.GetRigs();

            foreach(var rig in Program.Configuration.Rigs)
            {
                var whattomineParams = new List<AlgorithmParams>();
                var status = rigsStatus.Single(a => a.Id == rig.Id);

                foreach(var algo in rig.Algorithms)
                {
                    whattomineParams.Add(new AlgorithmParams
                    {
                        HashRate = algo.Hashrate,
                        PowerConsumtion = algo.PowerConsumption
                    });
                }

                var profits = 
                    whattomineApiClient.Get(whattomineParams)
                    .OrderByDescending(a => a.Profitability24);

                foreach (var profit in profits)
                {
                    var tag = 
                         rig.Algorithms.Where(a => a.Type == profit.Algorithm)
                        .SelectMany(a => a.Tags)
                        .FirstOrDefault(a => a.Names.Contains(profit.Tag));

                    if(tag != null)
                    {
                        if(status.WalletId != tag.WalletId ||
                           status.Miner != tag.Miner)
                        {
                            hiveOsApiClient.MultiRocket(
                                new int[] { rig.Id },
                                tag.Miner,
                                null,
                                tag.WalletId,
                                tag.OverClockId);
                        }

                        break;
                    }
                }
            }

            return 0;
        }
    }
}