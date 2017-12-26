using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DataException : Exception
    {
        public DataException(string aMsg) : base(aMsg)
        {
            // Nothing to do
        }

        public DataException(string aMsg, Exception aInnerException) : base(aMsg, aInnerException)
        {
            // Nothing to do
        }
    }
}
