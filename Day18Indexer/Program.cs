using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18Indexer
{
    class PrimeArray
    {
        private bool isPrime(int n)
        {
            int m = n / 2;
            for (int i=2; i<=m; i++)
            {
                if (n% i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool this[int n]
        {
            get
            {
                if (n <= 1) return false;
                return isPrime(n);
            }
        }
    }

    class PrimeArray2
    {
        private bool isPrime(long n)
        {
            long m = n / 2;
            for (long i = 2; i <= m; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public long this[int n]
        {
            get
            {
                if (n <= 0) throw new IndexOutOfRangeException("Index must be 1 or greater");
                int count = 0; // number of primes found
                long val = 2; // value tested for being prime or not
                while (true)
                {
                    if (isPrime(val))
                    {
                        count++;
                    }
                    if(count == n)
                    {
                        // found the Nth prime number I wanted
                        return val;
                    }
                    val++; // go check the next prime number
                    if(val < 0)
                    {
                        // integer overflow
                        throw new IndexOutOfRangeException();
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*
            PrimeArray pa = new PrimeArray();
            for(int i = 1; i <10; i++)
            {
                Console.WriteLine(i+" : " + pa[i]);
            }
            */

            PrimeArray2 pa2 = new PrimeArray2();
            for (int i = 1; i < 100; i++)
            {
                Console.Write("{0}:{1}, ", i, pa2[i]);
            }

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
