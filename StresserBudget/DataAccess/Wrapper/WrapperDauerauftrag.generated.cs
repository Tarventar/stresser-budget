using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using DataAccess.Dto;
using DataAccess.Filter;
using DataAccess;

namespace DataAccess.Wrapper
{
    /// <summary>
    /// Allows the access to the table Dauerauftrag.
    /// </summary>
    public partial class WrapperDauerauftrag : OryxDbAWrapperBase
    {
        private Access.DbaDauerauftrag mTable;
        
        internal WrapperDauerauftrag(string conString)
        {
            mTable = new Access.DbaDauerauftrag(conString);
            base.TableName = mTable.TableName;
        }
        
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
            return mTable.InsertRow(iDBudget, bezeichnung, gueltigAb, gueltigBis, lauftag, betrag);
        }
        /// <summary>
        /// Inserts the given row and returns its id.
        /// </summary>
        /// <param name="row">The prefilled row to insert.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(DauerauftragRow row)
        {
            return mTable.InsertRow(row.IDBudget, row.Bezeichnung, row.GueltigAb, row.GueltigBis, row.Lauftag, row.Betrag);
        }
        /// <summary>
        /// Gets a single row from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wID">A ID as criteria.</param>
        /// <returns>The row fulfilling the criteria</returns>
        public DauerauftragRow GetSingleRow(int wID, bool canBeNull)
        {
            DauerauftragRow ret = mTable.GetSingleRow(wID);
            if (ret.IsNull && !canBeNull)
                UnexpectedNull(MethodBase.GetCurrentMethod(), wID);
            return ret;
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wIDBudget">A IDBudget as criteria.</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<DauerauftragRow> GetRows(int wIDBudget)
        {
            return mTable.GetRows(wIDBudget);
        }
        
        /// <summary>
        /// Gets all rows from the table.
        /// </summary>
        /// <returns>All rows from the table</returns>
        public IEnumerable<DauerauftragRow> GetTable()
        {
            return mTable.GetTable();
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wFilter">A filter to use as where condition</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<DauerauftragRow> GetRowsByFilter(DauerauftragFilter wFilter)
        {
            return mTable.GetRowsByFilter(wFilter);
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
            mTable.UpdateSingleRow(uIDBudget, uBezeichnung, uGueltigAb, uGueltigBis, uLauftag, uBetrag, wID);
        }
        
        /// <summary>
        /// Updates the given single row on the table.
        /// </summary>
        /// <param name="row">The prefilled row.</param>
        public void UpdateSingleRow(DauerauftragRow row)
        {
            mTable.UpdateSingleRow(row.IDBudget, row.Bezeichnung, row.GueltigAb, row.GueltigBis, row.Lauftag, row.Betrag, row.ID);
        }
        
        /// <summary>
        /// Deletes a single row from the table.
        /// </summary>
        /// <param name="wID">A ID to identify the row.</param>
        public void DeleteSingleRow(int wID)
        {
            mTable.DeleteSingleRow(wID);
        }
        
        /// <summary>
        /// Deletes the given row.
        /// </summary>
        /// <param name="row">The row to delete</param>
        public void DeleteSingleRow(DauerauftragRow row)
        {
            mTable.DeleteSingleRow(row.ID);
        }
        
        /// <summary>
        /// Deletes all rows fulfilling the given criteria.
        /// </summary>
        /// <param name="wIDBudget">A IDBudget as criteria.</param>
        public void DeleteRowsByBudget(int wIDBudget)
        {
            mTable.DeleteRowsByBudget(wIDBudget);
        }
        
    }
}
