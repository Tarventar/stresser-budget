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
    /// Allows the access to the table Buchung.
    /// </summary>
    public partial class WrapperBuchung : OryxDbAWrapperBase
    {
        private Access.DbaBuchung mTable;
        
        internal WrapperBuchung(string conString)
        {
            mTable = new Access.DbaBuchung(conString);
            base.TableName = mTable.TableName;
        }
        
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
            return mTable.InsertRow(iDBudget, iDDauerauftrag, bezeichnung, datum, betrag, bemerkung);
        }
        /// <summary>
        /// Inserts the given row and returns its id.
        /// </summary>
        /// <param name="row">The prefilled row to insert.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(BuchungRow row)
        {
            return mTable.InsertRow(row.IDBudget, row.IDDauerauftrag, row.Bezeichnung, row.Datum, row.Betrag, row.Bemerkung);
        }
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wFilter">A filter to use as where condition</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<BuchungRow> GetRowsByFilter(BuchungFilter wFilter)
        {
            return mTable.GetRowsByFilter(wFilter);
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wIDBudget">A IDBudget as criteria.</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<BuchungRow> GetRowsByBudget(int wIDBudget)
        {
            return mTable.GetRowsByBudget(wIDBudget);
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wIDDauerauftrag">A IDDauerauftrag as criteria.</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<BuchungRow> GetRowsByDauerauftrag(int wIDDauerauftrag)
        {
            return mTable.GetRowsByDauerauftrag(wIDDauerauftrag);
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
            mTable.UpdateSingleRow(uIDBudget, uIDDauerauftrag, uBezeichnung, uDatum, uBetrag, uBemerkung, wID);
        }
        
        /// <summary>
        /// Updates the given single row on the table.
        /// </summary>
        /// <param name="row">The prefilled row.</param>
        public void UpdateSingleRow(BuchungRow row)
        {
            mTable.UpdateSingleRow(row.IDBudget, row.IDDauerauftrag, row.Bezeichnung, row.Datum, row.Betrag, row.Bemerkung, row.ID);
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
        public void DeleteSingleRow(BuchungRow row)
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
        
        /// <summary>
        /// Gets a single row from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wID">A ID as criteria.</param>
        /// <returns>The row fulfilling the criteria</returns>
        public BuchungRow GetSingleRow(int wID, bool canBeNull)
        {
            BuchungRow ret = mTable.GetSingleRow(wID);
            if (ret.IsNull && !canBeNull)
                UnexpectedNull(MethodBase.GetCurrentMethod(), wID);
            return ret;
        }
        
    }
}
