using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz02Traveller
{
    class DataInvalidException : Exception
    {
        public DataInvalidException(string msg) : base(msg) { }
    }
}
