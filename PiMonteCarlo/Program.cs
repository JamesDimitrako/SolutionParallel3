using System;

namespace PiMonteCarlo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            long numSteps = 10000000;
            long startTime = DateTime.Now.Millisecond;

            long count = 0;
            var random = new Random();
            for (int i = 0; i < numSteps; ++i)
            {
                double x = random.NextDouble();
                double y = random.NextDouble();
                double z = Math.Pow(x, 2) + Math.Pow(y, 2);
                if (z <= 1.0) count++;
            }

            double pi = (double) count / numSteps * 4;

            long endTime = DateTime.Now.Millisecond;
            Console.WriteLine($"sequential program results with {numSteps} \n");
            Console.WriteLine($"computed pi = {pi} \n");
            Console.WriteLine($"difference between estimated pi and Math.PI = {Math.Abs(pi - Math.PI)} \n");
            Console.WriteLine($"time to compute = {(double)(endTime - startTime) / 1000} \n");
        }
    }
}