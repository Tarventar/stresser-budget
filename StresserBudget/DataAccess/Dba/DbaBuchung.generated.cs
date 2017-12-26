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
    /// This class provides the lower access logic to the database table Buchung.
    /// </summary>
    public partial class DbaBuchung : OryxDbATableBase
    {
        // Name of this table
        private const string SQLNAME = "[Buchung]";
        private const string NAME = "Buchung";
        
        // All Columns of this table to use in SQL
        public const string SQL_COL_ID = "[ID]";
        public const string SQL_COL_IDBudget = "[IDBudget]";
        public const string SQL_COL_IDDauerauftrag = "[IDDauerauftrag]";
        public const string SQL_COL_Bezeichnung = "[Bezeichnung]";
        public const string SQL_COL_Datum = "[Datum]";
        public const string SQL_COL_Betrag = "[Betrag]";
        public const string SQL_COL_Bemerkung = "[Bemerkung]";
        private const string ALL_COLUMNS = SQL_COL_ID+","+SQL_COL_IDBudget+","+SQL_COL_IDDauerauftrag+","+SQL_COL_Bezeichnung+","+SQL_COL_Datum+","+SQL_COL_Betrag+","+SQL_COL_Bemerkung;
        
        // All Columns of this table to use in c#
        public const string COL_ID = "ID";
        public const string COL_IDBudget = "IDBudget";
        public const string COL_IDDauerauftrag = "IDDauerauftrag";
        public const string COL_Bezeichnung = "Bezeichnung";
        public const string COL_Datum = "Datum";
        public const string COL_Betrag = "Betrag";
        public const string COL_Bemerkung = "Bemerkung";
        
        /// <summary>
        /// Constructor of this class.
        /// </summary>
        public DbaBuchung(string conString) : base(conString, NAME, SQLNAME)
        {}
        
        /// <summary>
        /// Inserts a new row and returns it.
        /// </summary>
        /// <param name="iDBudget">The value to insert as IDBudget.</param>
        /// <param name="iDDauerauftrag">The value to insert as IDDauerauftrag.</param>
        /// <param name="bezeichnung">The value to insert as Bezeichnung.</param>
        /// <param name="datum">The value to insert as Datum.</param>
        /// <param name="betrag">The value to insert as Betrag.</param>
        /// <param name="bemerkung">The value to insert as Bemerkung. Null is possible.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(int iDBudget, int iDDauerauftrag, string bezeichnung, DateTime datum, int betrag, string bemerkung)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "INSERT INTO [Buchung] ([IDBudget],[IDDauerauftrag],[Bezeichnung],[Datum],[Betrag],[Bemerkung]) VALUES  (@iDBudget,@iDDauerauftrag,@bezeichnung,@datum,@betrag,@bemerkung);";
                command.Parameters.AddWithValue("@iDBudget", iDBudget);
                command.Parameters.AddWithValue("@iDDauerauftrag", iDDauerauftrag);
                command.Parameters.AddWithValue("@bezeichnung", bezeichnung);
                command.Parameters.AddWithValue("@datum", this.GetAccessDateTime(datum));
                command.Parameters.AddWithValue("@betrag", betrag);
                command.Parameters.AddWithValue("@bemerkung", (bemerkung == null? (object)System.DBNull.Value : bemerkung));
                int lResult = base.ExecuteInsertWithIdCommand(command);
                return lResult;
            }
        }
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wFilter">A filter to use as where condition</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<BuchungRow> GetRowsByFilter(BuchungFilter wFilter)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[IDDauerauftrag],[Bezeichnung],[Datum],[Betrag],[Bemerkung] FROM [Buchung]";
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
                if(wFilter.IDDauerauftrag != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_IDDauerauftrag + "=@wIDDauerauftrag");
                    command.Parameters.AddWithValue("wIDDauerauftrag", wFilter.IDDauerauftrag);
                }
                if(wFilter.IDDauerauftragMinWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_IDDauerauftrag + ">@wIDDauerauftragMinWo");
                    command.Parameters.AddWithValue("wIDDauerauftragMinWo", wFilter.IDDauerauftragMinWo);
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
                if(wFilter.Datum != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Datum + "=@wDatum");
                    command.Parameters.AddWithValue("wDatum", this.GetAccessDateTime(wFilter.Datum.Value));
                }
                if(wFilter.DatumMaxWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Datum + "<@wDatumMaxWo");
                    command.Parameters.AddWithValue("wDatumMaxWo", this.GetAccessDateTime(wFilter.DatumMaxWo.Value));
                }
                if(wFilter.DatumMinWo != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Datum + ">@wDatumMinWo");
                    command.Parameters.AddWithValue("wDatumMinWo", this.GetAccessDateTime(wFilter.DatumMinWo.Value));
                }
                if(wFilter.DatumMax != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Datum + "<=@wDatumMax");
                    command.Parameters.AddWithValue("wDatumMax", this.GetAccessDateTime(wFilter.DatumMax.Value));
                }
                if(wFilter.DatumMin != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Datum + ">=@wDatumMin");
                    command.Parameters.AddWithValue("wDatumMin", this.GetAccessDateTime(wFilter.DatumMin.Value));
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
                sbColumnWhere = new StringBuilder();
                if(wFilter.Bemerkung != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Bemerkung + "=@wBemerkung");
                    command.Parameters.AddWithValue("wBemerkung", wFilter.Bemerkung);
                }
                if(wFilter.BemerkungPattern != null)
                {
                    if(sbColumnWhere.Length > 0) sbColumnWhere.Append(wComboWord);
                    sbColumnWhere.Append(SQL_COL_Bemerkung + " LIKE @wBemerkungPattern");
                    command.Parameters.AddWithValue("wBemerkungPattern", wFilter.BemerkungPattern);
                }
                if(wFilter.BemerkungIsNull != null)
                {
                    if(sbColumnWhere.Length > 0)
                    {
                        sbColumnWhere.Insert(0, "((");
                        sbColumnWhere.Append(string.Format(") OR {0} IS {1}NULL)", SQL_COL_Bemerkung, (wFilter.BemerkungIsNull.HasValue && wFilter.BemerkungIsNull.Value ? "" : "NOT ")));
                    }
                    else
                    {
                        sbColumnWhere.Append(string.Format("{0} IS {1}NULL", SQL_COL_Bemerkung, (wFilter.BemerkungIsNull.HasValue && wFilter.BemerkungIsNull.Value ? "" : "NOT ")));
                    }
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
                List<BuchungRow> result = new List<BuchungRow>(resultTable.Rows.Count);
                foreach (DataRow oneRow in resultTable.Rows)
                    result.Add(RowToDto(oneRow));
                return result;
            }
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wIDBudget">A IDBudget as criteria.</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<BuchungRow> GetRowsByBudget(int wIDBudget)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[IDDauerauftrag],[Bezeichnung],[Datum],[Betrag],[Bemerkung] FROM [Buchung]";
                command.CommandText += " WHERE [IDBudget] = @wIDBudget";
                command.CommandText += " ORDER BY [Datum] DESC";
                command.Parameters.AddWithValue("@wIDBudget", wIDBudget);
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                List<BuchungRow> result = new List<BuchungRow>(resultTable.Rows.Count);
                foreach (DataRow oneRow in resultTable.Rows)
                    result.Add(RowToDto(oneRow));
                return result;
            }
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wIDDauerauftrag">A IDDauerauftrag as criteria.</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<BuchungRow> GetRowsByDauerauftrag(int wIDDauerauftrag)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[IDDauerauftrag],[Bezeichnung],[Datum],[Betrag],[Bemerkung] FROM [Buchung]";
                command.CommandText += " WHERE [IDDauerauftrag] = @wIDDauerauftrag";
                command.CommandText += " ORDER BY [Datum] DESC";
                command.Parameters.AddWithValue("@wIDDauerauftrag", wIDDauerauftrag);
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                List<BuchungRow> result = new List<BuchungRow>(resultTable.Rows.Count);
                foreach (DataRow oneRow in resultTable.Rows)
                    result.Add(RowToDto(oneRow));

                return result;
            }
        }
        
        /// <summary>
        /// Updates a single row fulfilling the criteria.
        /// </summary>
        /// <param name="uIDBudget">The value to set for IDBudget.</param>
        /// <param name="uIDDauerauftrag">The value to set for IDDauerauftrag.</param>
        /// <param name="uBezeichnung">The value to set for Bezeichnung.</param>
        /// <param name="uDatum">The value to set for Datum.</param>
        /// <param name="uBetrag">The value to set for Betrag.</param>
        /// <param name="uBemerkung">The value to set for Bemerkung. Null is possible.</param>
        /// <param name="wID">A ID to identify the row.</param>
        public void UpdateSingleRow(int uIDBudget, int uIDDauerauftrag, string uBezeichnung, DateTime uDatum, int uBetrag, string uBemerkung, int wID)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "UPDATE [Buchung] SET [IDBudget]=@uIDBudget,[IDDauerauftrag]=@uIDDauerauftrag,[Bezeichnung]=@uBezeichnung,[Datum]=@uDatum,[Betrag]=@uBetrag,[Bemerkung]=@uBemerkung";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@uIDBudget", uIDBudget);
                command.Parameters.AddWithValue("@uIDDauerauftrag", uIDDauerauftrag);
                command.Parameters.AddWithValue("@uBezeichnung", uBezeichnung);
                command.Parameters.AddWithValue("@uDatum", this.GetAccessDateTime(uDatum));
                command.Parameters.AddWithValue("@uBetrag", uBetrag);
                command.Parameters.AddWithValue("@uBemerkung", (uBemerkung == null? (object)System.DBNull.Value : uBemerkung));
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
                command.CommandText = "DELETE FROM [Buchung]";
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
                command.CommandText = "DELETE FROM [Buchung]";
                command.CommandText += " WHERE [IDBudget] = @wIDBudget";
                command.Parameters.AddWithValue("@wIDBudget", wIDBudget);
                command.CommandText += ";";
                base.ExecuteCommand(command);
            }
        }
        
        /// <summary>
        /// Gets a single row from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wID">A ID as criteria.</param>
        /// <returns>The row fulfilling the criteria</returns>
        public BuchungRow GetSingleRow(int wID)
        {
            using (OleDbCommand command = new OleDbCommand())
            {
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "SELECT [ID],[IDBudget],[IDDauerauftrag],[Bezeichnung],[Datum],[Betrag],[Bemerkung] FROM [Buchung]";
                command.CommandText += " WHERE [ID] = @wID";
                command.Parameters.AddWithValue("@wID", wID);
                command.CommandText += ";";
                DataTable resultTable = base.ExecuteCommand(command);
                if (resultTable.Rows.Count == 0)
                    return new BuchungRow() { IsNull = true };
                return RowToDto(resultTable.Rows[0]);
            }
        }
        
        /// <summary>
        /// Converts a datarow to a DTO object.
        /// </summary>
        /// <param name="row">The datarow to convert</param>
        /// <returns>The created DTO object</returns>
        private BuchungRow RowToDto(DataRow row)
        {
            return new BuchungRow()
            {
                ID = (int)row[COL_ID],
                IDBudget = (int)row[COL_IDBudget],
                IDDauerauftrag = (int)row[COL_IDDauerauftrag],
                Bezeichnung = (string)row[COL_Bezeichnung],
                Datum = (DateTime)row[COL_Datum],
                Betrag = (int)row[COL_Betrag],
                Bemerkung = (string)(row.IsNull(COL_Bemerkung) ? null : row[COL_Bemerkung])
            };
        }
    }
}
