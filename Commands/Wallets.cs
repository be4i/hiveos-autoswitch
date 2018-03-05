using System;
using McMaster.Extensions.CommandLineUtils;

namespace HiveOsAutomation.Commands
{
    public class Wallets
    {
        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            var client = new HiveOsAutomation.ApiClients.HiveOs.Client(Program.Configuration.HiveOsApiEndPoint, Program.Configuration.HiveOsApiPublicKey, Program.Configuration.HiveOsApiSecretKey);
            var wallets = client.GetWallets();

            foreach(var wallet in wallets)
            {
                console.WriteLine($"Id: {wallet.Id}, name: {wallet.Name}");
            }

            return 0;
        }
    }
}