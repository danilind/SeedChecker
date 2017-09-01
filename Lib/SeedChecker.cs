using Iota.Lib.CSharp.Api;
using Iota.Lib.CSharp.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib
{
    public class SeedChecker
    {
        private IotaApi iotaApi = new IotaApi("eugene.iotasupport.com", 14999);

        private int count = 5;

        private List<string> seeds;

        private string targetAddress;

        public SeedChecker(List<string> seeds, string targetAddress)
        {
            this.seeds = seeds;
            this.targetAddress = new string(targetAddress.Take(81).ToArray());
        }

        public string CheckSeeds()
        {
            // The result will be read in parallel. We can assume that only one seed matches, so that no race conditions occurres for writing.
            // To make sure that no collisions will occur, we first need to remove all duplicates
            this.seeds = this.seeds.Distinct().ToList();

            string result = null;
            for (var i = 0; i < this.count; i++)
            {
                Parallel.ForEach(this.seeds, (seed) =>
                {
                    if (result == null)
                    {
                        var address = IotaApiUtils.NewAddress(seed, i, false, new Curl());
                        if (address == this.targetAddress)
                        {
                            result = seed;
                        }
                    }
                });

                if (result != null)
                {
                    return result;
                }

                Console.WriteLine($"Nothing found at index {i}");
            }

            return result;
        }
    }
}
