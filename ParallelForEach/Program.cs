/*
 * Bill Nicholson
 * nicholdw@ucmail.uc.edu
 * https://www.kaggle.com/adhok93/presidentialaddress/data
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForEach {
    class Program {
        static void Main(string[] args) {
            // First set of tests, without and with parallelization
            //            Demo01WithNoParallelization();
            //            Demo01();

            //            Demo02();
            //            Demo02WithNoParallelization();

            Demo03();
            Demo03WithNoParallelization();
        }
        /// <summary>
        /// No parllelization to process each element in an integer list.
        /// The amount of time this will take to run is computable
        /// </summary>
        private static void Demo01WithNoParallelization() {
            Console.WriteLine("Demo01WithNoParallelization...");
            List<int> items = Enumerable.Range(0, 100).ToList();
            Stopwatch sw = Stopwatch.StartNew();
            foreach(int item in items)  {
                Thread.Sleep(50);   // For each element in the list we will sleep for .050 seconds
                Console.WriteLine(item);
            };
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} milliseconds.");
        }
        /// <summary>
        /// A simple use of the Parallel.ForEach construct to process each element in an integer list.
        /// The amount of time this will take to run is NOT computable
        /// </summary>
        private static void Demo01() {
            Console.WriteLine("Demo 01...");
            List<int> items = Enumerable.Range(0, 100).ToList();
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.ForEach(items, (item) => {
                Thread.Sleep(50);   // For each element in the list we will sleep for .050 seconds
                Console.WriteLine(item);
            });
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} milliseconds...");
        }
        /// <summary>
        /// Count the words in each Presidential Innaugural Address, in parallel
        /// </summary>
        private static void Demo02() {
            Console.WriteLine("Demo 02...");
            //https://www.kaggle.com/adhok93/presidentialaddress/data
            List<PresidentialAddress> presidentialAddresses = new List<PresidentialAddress>();
            String[] lines = System.IO.File.ReadAllLines(@"../../Data/inaug_speeches.csv");
            int lineNumber = 0;
            foreach(String line in lines) {
                if (lineNumber != 0) {
                    presidentialAddresses.Add(new PresidentialAddress(line));
                }
                lineNumber++;
            }
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.ForEach(presidentialAddresses, presidentialAddress => {
                Console.WriteLine(presidentialAddress.president + " : " + presidentialAddress.CountWords());
            });
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} milliseconds...");
        }
        /// <summary>
        /// Count the words in each Presidential Innaugural Address, not in parallel
        /// </summary>
        private static void Demo02WithNoParallelization() {
            Console.WriteLine("Demo02WithNoParallelization...");
            //https://www.kaggle.com/adhok93/presidentialaddress/data
            List<PresidentialAddress> presidentialAddresses = new List<PresidentialAddress>();
            String[] lines = System.IO.File.ReadAllLines(@"../../Data/inaug_speeches.csv");
            int lineNumber = 0;
            foreach (String line in lines) {
                if (lineNumber != 0) {
                    presidentialAddresses.Add(new PresidentialAddress(line));
                }
                lineNumber++;
            }
            Stopwatch sw = Stopwatch.StartNew();
            foreach (PresidentialAddress presidentialAddress in presidentialAddresses) {
                Console.WriteLine(presidentialAddress.president + " : " + presidentialAddress.CountWords());
            };
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} milliseconds...");
        }
        /// <summary>
        /// No parllelization to process each element in an integer list. We just count from 1 to 100, serially.
        /// The amount of time this will take to run is computable
        /// </summary>
        private static void Demo03WithNoParallelization()
        {
            int sum = 0;
            Console.WriteLine("Demo03WithNoParallelization...");
            List<int> items = Enumerable.Range(0, 100).ToList();
            Stopwatch sw = Stopwatch.StartNew();
            foreach (int item in items)
            {
                Thread.Sleep(50);   // For each element in the list we will sleep for .050 seconds
                //Console.WriteLine(item);
                sum++;
            };
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} milliseconds.");
            Console.WriteLine("sum = " + sum);
        }
        /// <summary>
        /// A simple use of the Parallel.ForEach construct to process each element in an integer list.
        /// We count from 1 to 100, paralley. We use a non-thread-safe data type (int)
        /// We then use an int in a thread-safe manner.
        /// The amount of time this will take to run is NOT computable
        /// </summary>
        private static void Demo03()
        {
            int sum1 = 0;
            int sum2 = 0;
            Console.WriteLine("Demo 03...");
            List<int> items = Enumerable.Range(0, 100).ToList();
            Stopwatch sw = Stopwatch.StartNew();
            Parallel.ForEach(items, (item) => {
                Thread.Sleep(50);   // For each element in the list we will sleep for .050 seconds
                //Console.WriteLine(item);
                sum1++;                                 // This does not work!
                Interlocked.Increment(ref sum2);        // This works!
            });
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} milliseconds...");
            Console.WriteLine("sum1 incremented NOT thread-safe = " + sum1);
            Console.WriteLine("sum2 incremented thread-safe = " + sum2);
        }
    }
}
