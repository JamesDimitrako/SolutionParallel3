using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Histogram
{
    class Program
    {        
        private static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Time(SerialHistogram, nameof(SerialHistogram));
            Time(ParallelHistogram, nameof(ParallelHistogram));
        }

        static void SerialHistogram()
        {
            String fileString = RandomString(100000000);//, StandardCharsets.UTF_8);
            char[] text = new char[fileString.Length]; 
            int n = fileString.Length;
            for (int i = 0; i < n; i++) { 
                text[i] = fileString[i]; 
            } 
 
            int alphabetSize = 256;
            int[] histogram = new int[alphabetSize]; 
            for (int i = 0; i < alphabetSize; i++) { 
                histogram[i] = 0; 
            }
        
            for (int i = 0; i < n; i++) {
                histogram[text[i]] ++;
            }

            for (int i = 0; i < alphabetSize; i++) {
                Console.WriteLine(histogram[i]);
            }
        }
        
        static void ParallelHistogram()
        {
            String fileString = RandomString(100000000);//, StandardCharsets.UTF_8);
            char[] text = new char[fileString.Length]; 
            int n = fileString.Length;
            for (int i = 0; i < n; i++) { 
                text[i] = fileString[i]; 
            } 
 
            int alphabetSize = 256;
            int[] histogram = new int[alphabetSize]; 
            for (int i = 0; i < alphabetSize; i++) { 
                histogram[i] = 0; 
            }

/*            Parallel.For<int>(0, alphabetSize, () => 0, (j, loop, local) =>
            {
                local = text[j];
                return local;
            }, x => Interlocked.Add(ref histogram[x], x));
  */      
           // for (int i = 0; i < n; i++) {
           //     histogram[text[i]] ++;
           // }

            for (int i = 0; i < alphabetSize; i++) {
                Console.WriteLine(histogram[i]);
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        
        // TODO: Explain diff between Action and Func
        static void Time(Action function, string functionName)
        {
            var sw = Stopwatch.StartNew();
            function();
            Console.WriteLine($"{functionName.PadRight(22)} | {sw.Elapsed}");
        }
    }
}