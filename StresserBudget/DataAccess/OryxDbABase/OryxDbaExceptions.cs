using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace DataAccess
{
    public class OryxDbaException : Exception
    {
        public string TableName { get; private set; }
        public string ClassName { get; private set; }
        public string MethodName { get; private set; }

        public OryxDbaException(string msg, string tableName, MethodBase method)
            : base(msg)
        {
            TableName = tableName;
            ClassName = method.DeclaringType.Name;
            MethodName = method.Name;
        }
    }

    public class NoRowReturnedException : OryxDbaException
    {
        public NoRowReturnedException(string msg, string tableName, MethodBase method)
            : base(msg, tableName, method)
        {
        }
    }

    public class ToManyRowsReturnedException : OryxDbaException
    {
        public ToManyRowsReturnedException(string msg, string tableName, MethodBase method)
            : base(msg, tableName, method)
        {
        }
    }

    public class MissingParameterException : OryxDbaException
    {
        public MissingParameterException(string msg, string tableName, MethodBase method)
            : base(msg, tableName, method)
        {
        }
    }

    public class UnexcpectedNullException : OryxDbaException
    {
        public UnexcpectedNullException(string msg, string tableName, MethodBase method)
            : base(msg, tableName, method)
        {
        }
    }
}
