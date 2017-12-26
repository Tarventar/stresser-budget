using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data.OleDb;
using System.Data;
using DataAccess.Dto;
using DataAccess.Filter;
using DataAccess;

namespace DataAccess.Access
{
    /// <summary>
    /// This class provides the lower access logic to the database table Konto.
    /// </summary>
    public partial class DbaKonto : OryxDbATableBase
    {
        // Name of this table
        private const string SQLNAME = "[Konto]";
        private const string NAME = "Konto";
        
        // All Columns of this table to use in SQL
        public const string SQL_COL_ID = "[ID]";
        public const string SQL_COL_Bezeichnung = "[Bezeichnung]";
        private const string ALL_COLUMNS = SQL_COL_ID+","+SQL_COL_Bezeichnung;
        
        // All Columns of this table to use in c#
        public const string COL_ID = "ID";
        public const string COL_Bezeichnung = "Bezeichnung";
        
        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public DbaKonto(string conString) : base(conString, NAME, SQLNAME)
        {}
        
        /// <summary>
        /// Inserts a new row and returns it.
        /// </summary>
        /// <param name="bezeichnung">The value to insert as Bezeichnung.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(string bezeichnung)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO [Konto] ([Bezeichnung]) VALUES  (@bezeichnung);";
                command.Parameters.AddWithValue("@bezeichnung", bezeichnung);
                int lResult = base.ExecuteInsertWithIdCommand(command);
                return lResult;
            }
        }
        /// <summary>
        /// Gets a single row from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wID">A ID as criteria.</param>
        /// <returns>The row fulfilling the criteria</returns>
        public KontoRow GetSingleRow(int wID)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[Bezeichnung] FROM [Konto]";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@wID", wID);
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                if (resultTable.Rows.Count == 0)
                    return new KontoRow() { IsNull = true };
                return RowToDto(resultTable.Rows[0]);
            }
        }
        
        /// <summary>
        /// Gets all rows from the table.
        /// </summary>
        /// <returns>All rows from the table</returns>
        public IEnumerable<KontoRow> GetTable()
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[Bezeichnung] FROM [Konto]";
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                List<KontoRow> result = new List<KontoRow>(resultTable.Rows.Count);
                foreach (DataRow oneRow in resultTable.Rows)
                    result.Add(RowToDto(oneRow));
                return result;
            }
        }
        
        /// <summary>
        /// Updates a single row fulfilling the criteria.
        /// </summary>
        /// <param name="uBezeichnung">The value to set for Bezeichnung.</param>
        /// <param name="wID">A ID to identify the row.</param>
        public void UpdateSingleRow(string uBezeichnung, int wID)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "UPDATE [Konto] SET [Bezeichnung]=@uBezeichnung";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@uBezeichnung", uBezeichnung);
                command.Parameters.AddWithValue("@wID", wID);
                command.CommandText += ";";
                base.ExecuteCommandNQ(command);
            }
        }
        /// <summary>
        /// Deletes a single row from the table.
        /// </summary>
        /// <param name="wID">A ID to identify the row.</param>
        public void DeleteSingleRow(int wID)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "DELETE FROM [Konto]";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@wID", wID);
                command.CommandText += ";";
                base.ExecuteCommandNQ(command);
            }
        }
        /// <summary>
        /// Converts a datarow to a DTO object.
        /// </summary>
        /// <param name="row">The datarow to convert</param>
        /// <returns>The created DTO object</returns>
        private KontoRow RowToDto(DataRow row)
        {
            return new KontoRow()
            {
                ID = (int)row[COL_ID],
                Bezeichnung = (string)row[COL_Bezeichnung]
            };
        }
    }
}
