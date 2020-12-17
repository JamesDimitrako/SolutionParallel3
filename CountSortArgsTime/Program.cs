using System;
using System.Diagnostics;

namespace CountSortArgsTime
{
    class Program
    {
        private static readonly int NumberOfSteps = 100_000_000;
        private static readonly int NumberOfCores = Environment.ProcessorCount;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Time(()=>CountSortArgsTime(10000), nameof(CountSortArgsTime));
        }
        
        // TODO: Explain diff between Action and Func
        static void Time(Action countSort, string function)
        {
            var sw = Stopwatch.StartNew();
            countSort();
            Console.WriteLine($"{function.PadRight(22)} | {sw.Elapsed}");
        }

        static void CountSortArgsTime(long size)
        {
            int[] a = new int[size];
            int[] b = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = i;
                b[i] = 0;
            }
        
            // for debugging
            for (int i = 0; i < size; i++)
                Console.WriteLine(a[i]);
        
            // get current time
            for (int i = 0; i < size; i++) {
                int mynum = a[i];
                int myplace = 0;
                for (int j = 0; j < size; j++)
                    if (mynum < a[j] || (mynum == a[j] && j < i))
                        myplace++;
                b[myplace] = mynum;
            }
            
            // for debugging
            for (int j = 0; j < size; j++)
                Console.WriteLine(b[j]);
        }
        
        static void CountSortArgsTimeParallel(long size)
        {
            int[] a = new int[size];
            int[] b = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = i;
                b[i] = 0;
            }
        
            // for debugging
            for (int i = 0; i < size; i++)
                Console.WriteLine(a[i]);
        
            // get current time
            for (int i = 0; i < size; i++) {
                int mynum = a[i];
                int myplace = 0;
                for (int j = 0; j < size; j++)
                    if (mynum < a[j] || (mynum == a[j] && j < i))
                        myplace++;
                b[myplace] = mynum;
            }
            
            // for debugging
            for (int j = 0; j < size; j++)
                Console.WriteLine(b[j]);
        }
    }
}