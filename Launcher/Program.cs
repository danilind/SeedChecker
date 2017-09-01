using Iota.Lib.CSharp.Api.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib;
using System.IO;

namespace Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * Usage: 
             * Dump all seed that you want to check in a file. The seeds must be seperated by a newline character ("\n").
             * First enter the path of the file.
             * Then enter the target address. This is the address that should belong to one of the seeds in the file.
             * 
             * The program will then print the seed to which the given address belongs if it is found.
             * The program can take some time to run, depending on the number of seeds. Please wait for the "Press any key to exit" message to appear before exiting.
             */

            try
            {
                DoStuff();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }

        static void DoStuff()
        {
            Console.WriteLine("Please enter the full path to your seed file: ");
            var path = Console.ReadLine();

            var seeds = File.ReadAllText(path).Split('\n').ToList();

            Console.WriteLine("Please enter the target address: ");
            var targetAddress = Console.ReadLine();

            var seed = new SeedChecker(seeds, targetAddress).CheckSeeds();

            if (seed == null)
            {
                Console.WriteLine("Seed not found");
            }
            else
            {
                Console.WriteLine($"Found seed: {seed}");
            }
        }
    }
}
