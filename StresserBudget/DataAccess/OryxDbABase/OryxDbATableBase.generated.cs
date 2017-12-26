using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Reflection;

namespace DataAccess
{
    public partial class OryxDbATableBase
    {
        protected string mConString = null;
        
        /// <summary>
        /// Gets or sets the table name.
        /// </summary>
        public string TableName
        {
            get;
            protected set;
        }

        public string SqlTableName
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets the predifined ConnectionString.
        /// </summary>
        public string ConString
        {
            get { return mConString; }
            protected set { mConString = value; }
        }

        protected OryxDbATableBase(string conString, string tableName, string sqlTableName)
        {
            mConString = conString;
            TableName = tableName;
            SqlTableName = sqlTableName;
        }
              
        /// <summary>
        /// Creates a SqlParameter to use as inline statement.
        /// </summary>
        protected OleDbParameter MakeSqlParameter(string parameterName, OleDbType dbType, object value)
        {
            OleDbParameter ret = new OleDbParameter(parameterName, dbType);
            if (value == null)
                ret.Value = System.Data.SqlTypes.SqlInt32.Null;
            else
                ret.Value = value;
            return ret;
        }
        
        /// <summary>
        /// Executes the given command using the predefined connectionstring without result value.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        protected void ExecuteCommandNQ(OleDbCommand command)
        {
            if (mConString == null)
                throw new Exception("ConnectionString is not available.");
            ExecuteCommandNQ(command, mConString);
        }
        
        /// <summary>
        /// Executes the given command using the given connectionstring without result value.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        protected void ExecuteCommandNQ(OleDbCommand command, string conString)
        {
            using (OleDbConnection connection = new OleDbConnection(conString))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        
        /// <summary>
        /// Executes the given command using the predefined connectionstring and returns the result as datatable.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Datatable containing the data.</returns>
        protected DataTable ExecuteCommand(OleDbCommand command)
        {
            if (mConString == null)
                throw new Exception("ConnectionString is not available.");
            return ExecuteCommand(command, mConString);
        }

        /// <summary>
        /// Only to use for insert statements with autovalue. The Value of the inserted id will be returned.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Datatable containing the data.</returns>
        protected int ExecuteInsertWithIdCommand(OleDbCommand command)
        {
            if (mConString == null)
                throw new Exception("ConnectionString is not available.");
            using (OleDbConnection connection = new OleDbConnection(mConString))
            {
                command.Connection = connection;
                connection.Open();
                command.ExecuteNonQuery();
                using (var lCmd = new OleDbCommand("SELECT @@Identity;", connection))
                {
                    object result = lCmd.ExecuteScalar();
                    connection.Close();
                    return Convert.ToInt32(result);
                }
            }
        }

        /// <summary>
        /// Executes the given command using the given connectionstring and returns the result as datatable.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Datatable containing the data.</returns>
        protected DataTable ExecuteCommand(OleDbCommand command, string conString)
        {
            using (OleDbConnection connection = new OleDbConnection(conString))
            {
                command.Connection = connection;
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    adapter.SelectCommand = command;
                    DataTable table = new DataTable();
                    connection.Open();
                    adapter.Fill(table);
                    connection.Close();
                    return table;
                }
            }
        }
        
        /// <summary>
        /// Executes the given command using the predefined connectionstring and returns the scalar result as object.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Datatable containing the data.</returns>
        protected object ExecuteCommandScalar(OleDbCommand command)
        {
            if (mConString == null)
                throw new Exception("ConnectionString is not available.");
            return ExecuteCommandScalar(command, mConString);
        }
        
        /// <summary>
        /// Executes the given command using the given connectionstring and returns the scalar result as object.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Datatable containing the data.</returns>
        protected object ExecuteCommandScalar(OleDbCommand command, string conString)
        {
            using (OleDbConnection connection = new OleDbConnection(conString))
            {
                command.Connection = connection;
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// Gets the given DateTime in MS Access format.
        /// </summary>
        protected DateTime GetAccessDateTime(DateTime aDateTime)
        {
            if (aDateTime.Millisecond > 0)
            {
                return new DateTime(aDateTime.Year, aDateTime.Month, aDateTime.Day, aDateTime.Hour, aDateTime.Minute, aDateTime.Second);
            }
            else
            {
                return aDateTime;
            }
        }
    }
}
