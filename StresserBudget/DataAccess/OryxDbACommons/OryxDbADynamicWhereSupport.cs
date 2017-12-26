using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public interface IWhereEmitter
    {
        string EmitWhereCondition();
    }

    public class OryxDbADynamicWhereException : Exception
    {
        public OryxDbADynamicWhereException() : base()
        {
        }

        public OryxDbADynamicWhereException(string message) 
            : base(message)
        {
        }

        public OryxDbADynamicWhereException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
