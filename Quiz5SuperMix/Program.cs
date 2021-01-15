using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz5SuperMix
{
    public class Program
    {
        static void Main(string[] args)
        {
            SuperFibs sf = new SuperFibs();

            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine("{0}:{1}, steps {2}", i, sf[i], sf.StepsCount);
            }
            Console.WriteLine("{0}:{1}, steps {2}", 5, sf[5], sf.StepsCount);

            Console.WriteLine("{0}:{1}, steps {2}", 4, sf[4], sf.StepsCount);

            Console.WriteLine("{0}:{1}, steps {2}", 12, sf[12], sf.StepsCount);

            Console.WriteLine("Please enter the number of seconds(floating point number) to compute the 'Super Finbonacci': ");
            double seconds;
            var watch = new System.Diagnostics.Stopwatch();

            try
            {
                double.TryParse(Console.ReadLine(), out seconds);
                int index = 1;

                watch.Start();

                while (watch.Elapsed.TotalSeconds < seconds)
                {
                    Console.WriteLine("{0}:{1}", index, sf[index]);
                    index++;
                }
                    watch.Stop();
                    Console.WriteLine("{0} super-fib numbers generated in under {1} seconds", index, seconds);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Please input correct number" + ex.Message);
            }

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
