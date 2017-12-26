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
    /// Allows the access to the table Konto.
    /// </summary>
    public partial class WrapperKonto : OryxDbAWrapperBase
    {
        private Access.DbaKonto mTable;
        
        internal WrapperKonto(string conString)
        {
            mTable = new Access.DbaKonto(conString);
            base.TableName = mTable.TableName;
        }
        
        /// <summary>
        /// Inserts a new row and returns it.
        /// </summary>
        /// <param name="bezeichnung">The value to insert as Bezeichnung.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(string bezeichnung)
        {
            return mTable.InsertRow(bezeichnung);
        }
        /// <summary>
        /// Inserts the given row and returns its id.
        /// </summary>
        /// <param name="row">The prefilled row to insert.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(KontoRow row)
        {
            return mTable.InsertRow(row.Bezeichnung);
        }
        /// <summary>
        /// Gets a single row from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wID">A ID as criteria.</param>
        /// <returns>The row fulfilling the criteria</returns>
        public KontoRow GetSingleRow(int wID, bool canBeNull)
        {
            KontoRow ret = mTable.GetSingleRow(wID);
            if (ret.IsNull && !canBeNull)
                UnexpectedNull(MethodBase.GetCurrentMethod(), wID);
            return ret;
        }
        
        /// <summary>
        /// Gets all rows from the table.
        /// </summary>
        /// <returns>All rows from the table</returns>
        public IEnumerable<KontoRow> GetTable()
        {
            return mTable.GetTable();
        }
        
        /// <summary>
        /// Updates a single row fulfilling the criteria.
        /// </summary>
        /// <param name="uBezeichnung">The value to set for Bezeichnung.</param>
        /// <param name="wID">A ID to identify the row.</param>
        public void UpdateSingleRow(string uBezeichnung, int wID)
        {
            mTable.UpdateSingleRow(uBezeichnung, wID);
        }
        
        /// <summary>
        /// Updates the given single row on the table.
        /// </summary>
        /// <param name="row">The prefilled row.</param>
        public void UpdateSingleRow(KontoRow row)
        {
            mTable.UpdateSingleRow(row.Bezeichnung, row.ID);
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
        public void DeleteSingleRow(KontoRow row)
        {
            mTable.DeleteSingleRow(row.ID);
        }
        
    }
}
