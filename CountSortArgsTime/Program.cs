using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CountSortArgsTime
{
    class Program
    {
        private static readonly int NumberOfSteps = 20_000;
        private static readonly int NumberOfCores = Environment.ProcessorCount;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Time(()=>SerialCountSortArgsTime(NumberOfSteps), 
                nameof(SerialCountSortArgsTime));
            Time(()=>SerialCountSortArgsTimeQuickSortGeneric(NumberOfSteps), 
                nameof(SerialCountSortArgsTimeQuickSortGeneric));     
            Time(()=>ParallelCountSortArgsTime(NumberOfSteps), 
                nameof(ParallelCountSortArgsTime));
            

        }
        
        // Delegates: Function: Pointer function that returns something(not Void)
        // Delegates: Action: Pointer function that didn't return anything(Void)
        static void Time(Action countSort, string function)
        {
            var sw = Stopwatch.StartNew();
            countSort();
            Console.WriteLine($"{function.PadRight(22)} | {sw.Elapsed}");
        }

        static void SerialCountSortArgsTime(long size)
        {
            int[] a = new int[size];
            int[] b = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = i;
                b[i] = 0;
            }
        
            // for debugging
            /*
            for (int i = 0; i < size; i++)
                Console.WriteLine(a[i]);
                */
        
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
            /*for (int j = 0; j < size; j++)
                Console.WriteLine(b[j]);*/
        }
        
        static void SerialCountSortArgsTimeQuickSortGeneric(int size)
        {
            int[] a = new int[size];
            int[] b = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = i;
                b[i] = 0;
            }
        
            // for debugging
            /*
            for (int i = 0; i < size; i++)
                Console.WriteLine(a[i]);
                */
        
            // get current time
            int[] c = (int[]) QuickSort.QuickySort(a);

            // for debugging
            /*if (b.Length > 0)
                for (int j = 0; j < size; j++)
                    Console.WriteLine(b[j]);*/
        }
        
        static void SerialCountSortArgsTimeQuickSortArray(long size)
        {
            int[] a = new int[size];
            int[] b = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = i;
                b[i] = 0;
            }
        
            // for debugging
            /*
            for (int i = 0; i < size; i++)
                Console.WriteLine(a[i]);
                */
        
            // get current time
            QuickSortArray.Program();

            // for debugging
            /*if (b.Length > 0)
                for (int j = 0; j < size; j++)
                    Console.WriteLine(b[j]);*/
        }
        
        static void ParallelCountSortArgsTime(int size)
        {
            int[] a = new int[size];
            int[] b = new int[size];
            for (int i = 0; i < size; i++)
            {
                a[i] = i;
                b[i] = 0;
            }
        
            // for debugging
            /*for (int i = 0; i < size; i++)
                Console.WriteLine(a[i]);*/
        
            // get current time
            QuickSort.ParallelQuickySort(a);

            // for debugging
            /*for (int j = 0; j < size; j++)
                Console.WriteLine(b[j]);*/
        }
    }
}