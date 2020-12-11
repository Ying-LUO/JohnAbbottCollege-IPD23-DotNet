using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz3FinalFlights
{
    public class DataInvalidException : Exception
    {
        public DataInvalidException(string msg) : base(msg) { }
    }
}
