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
    /// This class provides the lower access logic to the database table Dauerauftrag.
    /// </summary>
    public partial class DbaDauerauftrag : OryxDbATableBase
    {
        // Name of this table
        private const string SQLNAME = "[Dauerauftrag]";
        private const string NAME = "Dauerauftrag";
        
        // All Columns of this table to use in SQL
        public const string SQL_COL_ID = "[ID]";
        public const string SQL_COL_IDBudget = "[IDBudget]";
        public const string SQL_COL_Bezeichnung = "[Bezeichnung]";
        public const string SQL_COL_GueltigAb = "[GueltigAb]";
        public const string SQL_COL_GueltigBis = "[GueltigBis]";
        public const string SQL_COL_Lauftag = "[Lauftag]";
        public const string SQL_COL_Betrag = "[Betrag]";
        private const string ALL_COLUMNS = SQL_COL_ID+","+SQL_COL_IDBudget+","+SQL_COL_Bezeichnung+","+SQL_COL_GueltigAb+","+SQL_COL_GueltigBis+","+SQL_COL_Lauftag+","+SQL_COL_Betrag;
        
        // All Columns of this table to use in c#
        public const string COL_ID = "ID";
        public const string COL_IDBudget = "IDBudget";
        public const string COL_Bezeichnung = "Bezeichnung";
        public const string COL_GueltigAb = "GueltigAb";
        public const string COL_GueltigBis = "GueltigBis";
        public const string COL_Lauftag = "Lauftag";
        public const string COL_Betrag = "Betrag";
        
        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public DbaDauerauftrag(string conString) : base(conString, NAME, SQLNAME)
        {}
        
        /// <summary>
        /// Inserts a new row and returns it.
        /// </summary>
        /// <param name="iDBudget">The value to insert as IDBudget.</param>
        /// <param name="bezeichnung">The value to insert as Bezeichnung.</param>
        /// <param name="gueltigAb">The value to insert as GueltigAb.</param>
        /// <param name="gueltigBis">The value to insert as GueltigBis.</param>
        /// <param name="lauftag">The value to insert as Lauftag.</param>
        /// <param name="betrag">The value to insert as Betrag.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(int iDBudget, string bezeichnung, DateTime gueltigAb, DateTime gueltigBis, int lauftag, int betrag)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO [Dauerauftrag] ([IDBudget],[Bezeichnung],[GueltigAb],[GueltigBis],[Lauftag],[Betrag]) VALUES  (@iDBudget,@bezeichnung,@gueltigAb,@gueltigBis,@lauftag,@betrag);";
                command.Parameters.AddWithValue("@iDBudget", iDBudget);
                command.Parameters.AddWithValue("@bezeichnung", bezeichnung);
                command.Parameters.AddWithValue("@gueltigAb", this.GetAccessDateTime(gueltigAb));
                command.Parameters.AddWithValue("@gueltigBis", this.GetAccessDateTime(gueltigBis));
                command.Parameters.AddWithValue("@lauftag", lauftag);
                command.Parameters.AddWithValue("@betrag", betrag);
                int lResult = base.ExecuteInsertWithIdCommand(command);
                return lResult;
            }
        }
        /// <summary>
        /// Gets a single row from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wID">A ID as criteria.</param>
        /// <returns>The row fulfilling the criteria</returns>
        public DauerauftragRow GetSingleRow(int wID)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[Bezeichnung],[GueltigAb],[GueltigBis],[Lauftag],[Betrag] FROM [Dauerauftrag]";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@wID", wID);
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                if (resultTable.Rows.Count == 0)
                    return new DauerauftragRow() { IsNull = true };
                return RowToDto(resultTable.Rows[0]);
            }
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wIDBudget">A IDBudget as criteria.</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<DauerauftragRow> GetRows(int wIDBudget)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[Bezeichnung],[GueltigAb],[GueltigBis],[Lauftag],[Betrag] FROM [Dauerauftrag]";
                command.CommandText += " WHERE [IDBudget] = @wIDBudget";
                command.Parameters.AddWithValue("@wIDBudget", wIDBudget);
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                List<DauerauftragRow> result = new List<DauerauftragRow>(resultTable.Rows.Count);
                foreach (DataRow oneRow in resultTable.Rows)
                    result.Add(RowToDto(oneRow));
                return result;
            }
        }
        
        /// <summary>
        /// Gets all rows from the table.
        /// </summary>
        /// <returns>All rows from the table</returns>
        public IEnumerable<DauerauftragRow> GetTable()
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[Bezeichnung],[GueltigAb],[GueltigBis],[Lauftag],[Betrag] FROM [Dauerauftrag]";
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                List<DauerauftragRow> result = new List<DauerauftragRow>(resultTable.Rows.Count);
                foreach (DataRow oneRow in resultTable.Rows)
                    result.Add(RowToDto(oneRow));
                return result;
            }
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wFilter">A filter to use as where condition</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<DauerauftragRow> GetRowsByFilter(DauerauftragFilter wFilter)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[Bezeichnung],[GueltigAb],[GueltigBis],[Lauftag],[Betrag] FROM [Dauerauftrag]";
                StringBuilder sbWhere = new StringBuilder();
                StringBuilder sbColumnWhere;
                string wComboWord = (wFilter.ComboMode == OryxFilterCombinationEnum.And ? " AND " : " OR ");
                sbColumnWhere = new StringBuilder();
                if(wFilter.IDBudget != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_IDBudget + "=@wIDBudget");
                    command.Parameters.AddWithValue("wIDBudget", wFilter.IDBudget);
                }
                if(sbColumnWhere.Length > 0)
                {
                    if(sbWhere.Length > 0) sbWhere.Append(wComboWord);
                    sbWhere.Append(sbColumnWhere.ToString());
                }
                sbColumnWhere = new StringBuilder();
                if(wFilter.Bezeichnung != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Bezeichnung + "=@wBezeichnung");
                    command.Parameters.AddWithValue("wBezeichnung", wFilter.Bezeichnung);
                }
                if(wFilter.BezeichnungPattern != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Bezeichnung + " LIKE @wBezeichnungPattern");
                    command.Parameters.AddWithValue("wBezeichnungPattern", wFilter.BezeichnungPattern);
                }
                if(sbColumnWhere.Length > 0)
                {
                    if(sbWhere.Length > 0) sbWhere.Append(wComboWord);
                    sbWhere.Append(sbColumnWhere.ToString());
                }
                sbColumnWhere = new StringBuilder();
                if(wFilter.GueltigAb != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigAb + "=@wGueltigAb");
                    command.Parameters.AddWithValue("wGueltigAb", this.GetAccessDateTime(wFilter.GueltigAb.Value));
                }
                if(wFilter.GueltigAbMaxWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigAb + "<@wGueltigAbMaxWo");
                    command.Parameters.AddWithValue("wGueltigAbMaxWo", this.GetAccessDateTime(wFilter.GueltigAbMaxWo.Value));
                }
                if(wFilter.GueltigAbMinWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigAb + ">@wGueltigAbMinWo");
                    command.Parameters.AddWithValue("wGueltigAbMinWo", this.GetAccessDateTime(wFilter.GueltigAbMinWo.Value));
                }
                if(wFilter.GueltigAbMax != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigAb + "<=@wGueltigAbMax");
                    command.Parameters.AddWithValue("wGueltigAbMax", this.GetAccessDateTime(wFilter.GueltigAbMax.Value));
                }
                if(wFilter.GueltigAbMin != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigAb + ">=@wGueltigAbMin");
                    command.Parameters.AddWithValue("wGueltigAbMin", this.GetAccessDateTime(wFilter.GueltigAbMin.Value));
                }
                if(sbColumnWhere.Length > 0)
                {
                    if(sbWhere.Length > 0) sbWhere.Append(wComboWord);
                    sbWhere.Append(sbColumnWhere.ToString());
                }
                sbColumnWhere = new StringBuilder();
                if(wFilter.GueltigBis != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigBis + "=@wGueltigBis");
                    command.Parameters.AddWithValue("wGueltigBis", this.GetAccessDateTime(wFilter.GueltigBis.Value));
                }
                if(wFilter.GueltigBisMaxWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigBis + "<@wGueltigBisMaxWo");
                    command.Parameters.AddWithValue("wGueltigBisMaxWo", this.GetAccessDateTime(wFilter.GueltigBisMaxWo.Value));
                }
                if(wFilter.GueltigBisMinWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigBis + ">@wGueltigBisMinWo");
                    command.Parameters.AddWithValue("wGueltigBisMinWo", this.GetAccessDateTime(wFilter.GueltigBisMinWo.Value));
                }
                if(wFilter.GueltigBisMax != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigBis + "<=@wGueltigBisMax");
                    command.Parameters.AddWithValue("wGueltigBisMax", this.GetAccessDateTime(wFilter.GueltigBisMax.Value));
                }
                if(wFilter.GueltigBisMin != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_GueltigBis + ">=@wGueltigBisMin");
                    command.Parameters.AddWithValue("wGueltigBisMin", this.GetAccessDateTime(wFilter.GueltigBisMin.Value));
                }
                if(sbColumnWhere.Length > 0)
                {
                    if(sbWhere.Length > 0) sbWhere.Append(wComboWord);
                    sbWhere.Append(sbColumnWhere.ToString());
                }
                sbColumnWhere = new StringBuilder();
                if(wFilter.Lauftag != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Lauftag + "=@wLauftag");
                    command.Parameters.AddWithValue("wLauftag", wFilter.Lauftag);
                }
                if(wFilter.LauftagMaxWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Lauftag + "<@wLauftagMaxWo");
                    command.Parameters.AddWithValue("wLauftagMaxWo", wFilter.LauftagMaxWo);
                }
                if(wFilter.LauftagMinWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Lauftag + ">@wLauftagMinWo");
                    command.Parameters.AddWithValue("wLauftagMinWo", wFilter.LauftagMinWo);
                }
                if(wFilter.LauftagMax != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Lauftag + "<=@wLauftagMax");
                    command.Parameters.AddWithValue("wLauftagMax", wFilter.LauftagMax);
                }
                if(wFilter.LauftagMin != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Lauftag + ">=@wLauftagMin");
                    command.Parameters.AddWithValue("wLauftagMin", wFilter.LauftagMin);
                }
                if(sbColumnWhere.Length > 0)
                {
                    if(sbWhere.Length > 0) sbWhere.Append(wComboWord);
                    sbWhere.Append(sbColumnWhere.ToString());
                }
                sbColumnWhere = new StringBuilder();
                if(wFilter.Betrag != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Betrag + "=@wBetrag");
                    command.Parameters.AddWithValue("wBetrag", wFilter.Betrag);
                }
                if(wFilter.BetragMaxWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Betrag + "<@wBetragMaxWo");
                    command.Parameters.AddWithValue("wBetragMaxWo", wFilter.BetragMaxWo);
                }
                if(wFilter.BetragMinWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Betrag + ">@wBetragMinWo");
                    command.Parameters.AddWithValue("wBetragMinWo", wFilter.BetragMinWo);
                }
                if(wFilter.BetragMax != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Betrag + "<=@wBetragMax");
                    command.Parameters.AddWithValue("wBetragMax", wFilter.BetragMax);
                }
                if(wFilter.BetragMin != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Betrag + ">=@wBetragMin");
                    command.Parameters.AddWithValue("wBetragMin", wFilter.BetragMin);
                }
                if(sbColumnWhere.Length > 0)
                {
                    if(sbWhere.Length > 0) sbWhere.Append(wComboWord);
                    sbWhere.Append(sbColumnWhere.ToString());
                }
                // Add the WHERE term
                if (sbWhere.Length > 0)
                    command.CommandText += (" WHERE " + sbWhere.ToString());
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                List<DauerauftragRow> result = new List<DauerauftragRow>(resultTable.Rows.Count);
                foreach (DataRow oneRow in resultTable.Rows)
                    result.Add(RowToDto(oneRow));
                return result;
            }
        }
        
        /// <summary>
        /// Updates a single row fulfilling the criteria.
        /// </summary>
        /// <param name="uIDBudget">The value to set for IDBudget.</param>
        /// <param name="uBezeichnung">The value to set for Bezeichnung.</param>
        /// <param name="uGueltigAb">The value to set for GueltigAb.</param>
        /// <param name="uGueltigBis">The value to set for GueltigBis.</param>
        /// <param name="uLauftag">The value to set for Lauftag.</param>
        /// <param name="uBetrag">The value to set for Betrag.</param>
        /// <param name="wID">A ID to identify the row.</param>
        public void UpdateSingleRow(int uIDBudget, string uBezeichnung, DateTime uGueltigAb, DateTime uGueltigBis, int uLauftag, int uBetrag, int wID)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "UPDATE [Dauerauftrag] SET [IDBudget]=@uIDBudget,[Bezeichnung]=@uBezeichnung,[GueltigAb]=@uGueltigAb,[GueltigBis]=@uGueltigBis,[Lauftag]=@uLauftag,[Betrag]=@uBetrag";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@uIDBudget", uIDBudget);
                command.Parameters.AddWithValue("@uBezeichnung", uBezeichnung);
                command.Parameters.AddWithValue("@uGueltigAb", this.GetAccessDateTime(uGueltigAb));
                command.Parameters.AddWithValue("@uGueltigBis", this.GetAccessDateTime(uGueltigBis));
                command.Parameters.AddWithValue("@uLauftag", uLauftag);
                command.Parameters.AddWithValue("@uBetrag", uBetrag);
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
                command.CommandText = "DELETE FROM [Dauerauftrag]";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@wID", wID);
                command.CommandText += ";";
                base.ExecuteCommandNQ(command);
            }
        }
        /// <summary>
        /// Deletes all rows fulfilling the given criteria.
        /// </summary>
        /// <param name="wIDBudget">A IDBudget as criteria.</param>
        public void DeleteRowsByBudget(int wIDBudget)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "DELETE FROM [Dauerauftrag]";
                command.CommandText += " WHERE [IDBudget] = @wIDBudget";
                command.Parameters.AddWithValue("@wIDBudget", wIDBudget);
                command.CommandText += ";";
                base.ExecuteCommand(command);
            }
        }
        
        /// <summary>
        /// Converts a datarow to a DTO object.
        /// </summary>
        /// <param name="row">The datarow to convert</param>
        /// <returns>The created DTO object</returns>
        private DauerauftragRow RowToDto(DataRow row)
        {
            return new DauerauftragRow()
            {
                ID = (int)row[COL_ID],
                IDBudget = (int)row[COL_IDBudget],
                Bezeichnung = (string)row[COL_Bezeichnung],
                GueltigAb = (DateTime)row[COL_GueltigAb],
                GueltigBis = (DateTime)row[COL_GueltigBis],
                Lauftag = (int)row[COL_Lauftag],
                Betrag = (int)row[COL_Betrag]
            };
        }
    }
}
