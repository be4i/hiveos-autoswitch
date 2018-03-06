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
            var wallets = hiveOsApiClient.GetWallets();
            var overclocks = hiveOsApiClient.GetOverclocks();

            foreach(var rig in Program.Configuration.Rigs)
            {
                var whattomineParams = new List<AlgorithmParams>();

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
                    .OrderByDescending(a => a.Profitability);

                foreach (var profit in profits)
                {
                    var algo = 
                         rig.Algorithms.Where(a => 
                            a.Type == profit.Algorithm &&
                            a.Tags.Any(b => b.Names.Contains(profit.Tag)))
                        .FirstOrDefault();

                    var tag = algo?.Tags.Single(a => a.Names.Contains(profit.Tag));

                    if(algo != null)
                    { 
                        var wallet = wallets.Single(a => a.Name == tag.Wallet);
                        var overclock = overclocks.SingleOrDefault(a => a.Name == algo.Overclock);
                        
                        foreach(var status in rigsStatus.Where(a => rig.Names.Contains(a.Name)))
                        {
                            if(status.WalletId != wallet.Id ||
                               status.Miner != tag.Miner)
                            {
                                hiveOsApiClient.MultiRocket(
                                    new [] { status.Id },
                                    tag.Miner,
                                    null,
                                    wallet.Id,
                                    overclock?.Id);
                            }
                        }

                        break;
                    }
                }
            }

            return 0;
        }
    }
}