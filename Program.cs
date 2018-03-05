using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HiveOsAutomation;
using HiveOsAutomation.ApiClients.HiveOs;
using HiveOsAutomation.ApiClients.Whattomine;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HiveOsAutomation
{
    [Subcommand("wallets", typeof(Commands.Wallets))]
    [Subcommand("autoswitch", typeof(Commands.AutoSwitch))]
    class Program
    {
        public static Configuration Configuration {get; set;}

        static void Main(string[] args)
        {
            Configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json")));

            CommandLineApplication.Execute<Program>(args);
        }

        public int OnExecute(CommandLineApplication app, IConsole console)
        {
            app.ShowHelp();
            
            return 0;
        }
    }
}
