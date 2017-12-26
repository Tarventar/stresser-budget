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
    /// Allows the access to the table Budget.
    /// </summary>
    public partial class WrapperBudget : OryxDbAWrapperBase
    {
        private Access.DbaBudget mTable;
        
        internal WrapperBudget(string conString)
        {
            mTable = new Access.DbaBudget(conString);
            base.TableName = mTable.TableName;
        }
        
        /// <summary>
        /// Inserts a new row and returns it.
        /// </summary>
        /// <param name="iDKonto">The value to insert as IDKonto.</param>
        /// <param name="bezeichnung">The value to insert as Bezeichnung.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(int iDKonto, string bezeichnung)
        {
            return mTable.InsertRow(iDKonto, bezeichnung);
        }
        /// <summary>
        /// Inserts the given row and returns its id.
        /// </summary>
        /// <param name="row">The prefilled row to insert.</param>
        /// <returns>The inserted row</returns>
        public int InsertRow(BudgetRow row)
        {
            return mTable.InsertRow(row.IDKonto, row.Bezeichnung);
        }
        /// <summary>
        /// Gets a single row from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wID">A ID as criteria.</param>
        /// <returns>The row fulfilling the criteria</returns>
        public BudgetRow GetSingleRow(int wID, bool canBeNull)
        {
            BudgetRow ret = mTable.GetSingleRow(wID);
            if (ret.IsNull && !canBeNull)
                UnexpectedNull(MethodBase.GetCurrentMethod(), wID);
            return ret;
        }
        
        /// <summary>
        /// Gets all rows from the table.
        /// </summary>
        /// <returns>All rows from the table</returns>
        public IEnumerable<BudgetRow> GetTable()
        {
            return mTable.GetTable();
        }
        
        /// <summary>
        /// Gets rows from the table fulfilling the criteria.
        /// </summary>
        /// <param name="wIDKonto">A IDKonto as criteria.</param>
        /// <returns>All rows fulfilling the criteria</returns>
        public IEnumerable<BudgetRow> GetRows(int wIDKonto)
        {
            return mTable.GetRows(wIDKonto);
        }
        
        /// <summary>
        /// Updates a single row fulfilling the criteria.
        /// </summary>
        /// <param name="uIDKonto">The value to set for IDKonto.</param>
        /// <param name="uBezeichnung">The value to set for Bezeichnung.</param>
        /// <param name="wID">A ID to identify the row.</param>
        public void UpdateSingleRow(int uIDKonto, string uBezeichnung, int wID)
        {
            mTable.UpdateSingleRow(uIDKonto, uBezeichnung, wID);
        }
        
        /// <summary>
        /// Updates the given single row on the table.
        /// </summary>
        /// <param name="row">The prefilled row.</param>
        public void UpdateSingleRow(BudgetRow row)
        {
            mTable.UpdateSingleRow(row.IDKonto, row.Bezeichnung, row.ID);
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
        public void DeleteSingleRow(BudgetRow row)
        {
            mTable.DeleteSingleRow(row.ID);
        }
        
    }
}
