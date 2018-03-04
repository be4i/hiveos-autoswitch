using System;
using System.Collections.Generic;

namespace hiveos_autoswitch
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");
            WhatToMineClient whatToMine = new WhatToMineClient();
            var coins = new List<AlgorithmParams>();
            coins.Add(new AlgorithmParams{Algo = Algorithms.Ethhash, HashRate = 170, PowerConsumtion = 1});
            whatToMine.Request(coins);
            Console.ReadLine();
            Console.ReadKey();

        }
    }
}
