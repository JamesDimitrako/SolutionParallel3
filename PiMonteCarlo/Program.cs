using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PiMonteCarlo
{
    class Program
    {
        private static readonly int NumberOfSteps = 100_000_000;
        private static readonly int NumberOfCores = Environment.ProcessorCount;

        static void Main(string[] args)
        {
            Time(SerialPi, nameof(SerialPi));
            Time(ParallelForPi, nameof(ParallelForPi));
            Time(ParallelTasksPi, nameof(ParallelTasksPi));
        }
        
        // using a delegate(pointer function). Elegant solution to measure time
        static void Time(Func<double> estimatePi, string function)
        {
            var sw = Stopwatch.StartNew();
            var pi = estimatePi();
            Console.WriteLine($"{function.PadRight(22)} | {sw.Elapsed} | {pi}");
        }

        static double SerialPi()
        {
            long count = 0;
            var random = new Random();
            for (int i = 0; i < NumberOfSteps; ++i)
            {
                double x = random.NextDouble();
                double y = random.NextDouble();
                double z = Math.Pow(x, 2) + Math.Pow(y, 2);
                if (z <= 1.0) count++;
            }

            double pi = (double) count / NumberOfSteps * 4;
            
            return pi;
        }
        
        // With Parallel for.
        static double ParallelForPi()
        {
            long count = 0;
            Parallel.For(0, NumberOfCores, new ParallelOptions{ MaxDegreeOfParallelism = NumberOfCores }, i =>
            {
                int localCounterInside = 0;
                Random random = new Random(); 

                for (int j = 0; j < NumberOfSteps / NumberOfCores; ++j)
                {
                    double x = random.NextDouble();
                    double y = random.NextDouble();
                    double z = Math.Pow(x, 2) + Math.Pow(y, 2);
                    if (z <= 1.0) localCounterInside++;                                                      
                }
                
                // Adds two 64-bit integers and replaces the first integer with the sum, as an atomic operation.
                Interlocked.Add(ref count, localCounterInside); 
            }); 

            double pi = 4 * (count / (double)NumberOfSteps);

            return pi;
        }
        
        // With tasks
        static double ParallelTasksPi()
        {
            int[] localCounters = new int[NumberOfCores];
            Task[] tasks = new Task[NumberOfCores];

            for (int i = 0; i < NumberOfCores; i++)
            {
                int procIndex = i; //closure capture 
                tasks[procIndex] = Task.Factory.StartNew(() =>
                {
                    int localCounterInside = 0;
                    Random random = new Random();

                    for (int j = 0; j < NumberOfSteps / NumberOfCores; ++j)
                    {
                        double x = random.NextDouble();
                        double y = random.NextDouble();
                        double z = Math.Pow(x, 2) + Math.Pow(y, 2);
                        if (z <= 1.0) localCounterInside++;    
                    } 
                    localCounters[procIndex] = localCounterInside;
                });               
            }
            Task.WaitAll(tasks);
            long count = localCounters.Sum();
            
            double pi = 4 * (count / (double)NumberOfSteps);
            
            return pi;
        }
    }
}