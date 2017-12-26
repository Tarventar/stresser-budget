using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace DataAccess
{
    public partial class OryxDbAWrapperBase
    {
        protected string mTableName;
        
        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string TableName
        {
            get { return mTableName; }
            protected set { mTableName = value; }
        }
        
        /// <summary>
        /// Throws a new detailed exception if a select statement returned an unexpected null result.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        protected void UnexpectedNull(MethodBase method, params object[] parameters)
        {
            StringBuilder paraText = new StringBuilder();
            foreach (object oneParam in parameters)
                paraText.Append(string.Format("[{0}], ", oneParam));
            if (paraText.Length > 1)
                paraText.Remove(paraText.Length - 2, 2);
            
            StringBuilder sb = new StringBuilder();
            sb.Append("Unexpected NULL result detected:");
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("{0}[{1}].{2}({3})",
                method.ReflectedType.Name,
                mTableName,
                method.Name,
                paraText.ToString()));

                throw new UnexcpectedNullException(sb.ToString(), this.TableName, method);
            }
        }
    }
