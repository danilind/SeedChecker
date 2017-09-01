using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib;
using Iota.Lib.CSharp.Api.Utils;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // Say that the target address is on the 4th index to test performance.
            var targetAddress = IotaApiUtils.NewAddress("OFEICXMADRKUCEYMFMLNQLUHNNPCJQBDRNAVT9RZJ9AJIMW9WBVHBXT9SCVTXNZBXVNXCVJDIFLRMRYPO", 4, false, new Curl());

            var seedsToCheck = new List<string>
            {
                "OFEICXMADRKUCEYMFMLNQLUHNNPCJQBDRNAVT9RZJ9AJIMW9WBVHBXT9SCVTXNZBXVNXCVJDIFLRMRYPO"
            };

            // Add more seeds to test performance
            seedsToCheck.AddRange(Enumerable.Range(0, 100).Select(o => GenerateRandomSeed()).ToList());

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var seed = new SeedChecker(seedsToCheck, targetAddress).CheckSeeds();

            Console.WriteLine($"Done in {stopwatch.ElapsedMilliseconds} ms");

            if (seed == null)
            {
                Console.WriteLine("Seed not found");
            }
            else
            {
                Console.WriteLine($"Found seed: {seed}");
            }

            Console.ReadKey();
        }

        static string GenerateRandomSeed()
        {
            // Need a short sleep because the random module is based on the system time.
            Thread.Sleep(20);

            // NOTE: This is not a secure way of generating seeds. Only for testing purposes
            var rnd = new Random();
            var alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ9";
            return new string(Enumerable.Range(0, 81).Select(o => alph[rnd.Next(alph.Length)]).ToArray());
        }
    }
}
