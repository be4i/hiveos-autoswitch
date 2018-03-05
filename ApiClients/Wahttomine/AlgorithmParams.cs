using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HiveOsAutomation.ApiClients.Whattomine
{
    public class AlgorithmParams
    {
        public eAlgorithm Algorithm;
        public decimal HashRate;
        public decimal PowerConsumtion;
    }
}