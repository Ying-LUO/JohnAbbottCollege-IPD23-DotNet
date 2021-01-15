using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz5SuperMix
{
    public class SuperFibs
    {
        private IDictionary<int, long> cache = new Dictionary<int, long>();
        private int _stepsCount = 0;

        public int StepsCount
        {
            get
            {
                return _stepsCount;
            }
        }

        private long getNthFib(int n)
        {
            if (n == 1)
            {
                return 0;
            }
            else if (n == 2)
            {
                return 1;
            }
            else if (n == 3)
            {
                return 1;
            }
            else
            {
                return getNthFib(n - 1) + getNthFib(n - 2) + getNthFib(n - 3);
            }
        }

        public long this[int n]
        {
            get
            {
                while (true)
                {
                    if (n < 1)
                    {
                        throw new IndexOutOfRangeException("Index must be 1 or greater");
                    }
                    else
                    {
                        if (cache.Keys.Contains(n))
                        {
                            _stepsCount = 0;
                            return cache[n];
                        }
                        else
                        {
                            if (n>3)
                            {
                                _stepsCount = n - 3;
                            }
                            cache.Add(n, getNthFib(n));
                            return getNthFib(n);
                        }
                    }
                }
            }
        }
    }
}
