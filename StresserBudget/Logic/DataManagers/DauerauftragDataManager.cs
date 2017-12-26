using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Dto;
using Logic.ViewModels;

namespace Logic.DataManagers
{
    public class DauerauftragDataManager
    {
        private DbFactory mDb;

        internal DauerauftragDataManager(DbFactory aDb)
        {
            this.mDb = aDb;
        }

        public IEnumerable<DauerauftragVm> GetByBudget(BudgetVm aBudgetVm)
        {
            List<DauerauftragVm> lResult = new List<DauerauftragVm>();
            if (aBudgetVm.Id >= 0)
            {
                foreach (var lRow in this.mDb.Dauerauftrag.GetRows(aBudgetVm.Id))
                {
                    lResult.Add(new DauerauftragVm(aBudgetVm, lRow));
                }
            }

            return lResult;
        }

        internal void SaveDauerauftrag(DauerauftragVm aDauerauftragVm)
        {
            if (!aDauerauftragVm.Validate().IsValid)
            {
                throw new DataException("Es sind noch Validierungsfehler auf dem DauerauftragVm vorhanden!");
            }

            if (aDauerauftragVm.Row.Status == DtoStatus.Created)
            {
                // To create
                var lId = mDb.Dauerauftrag.InsertRow(aDauerauftragVm.Row);

                // Reload from Database
                var lRow = mDb.Dauerauftrag.GetSingleRow(lId, false);

                aDauerauftragVm.Row = lRow;
            }
            else if (aDauerauftragVm.Row.Status == DtoStatus.Updated)
            {
                // To update
                mDb.Dauerauftrag.UpdateSingleRow(aDauerauftragVm.Row);

                // Reload from Database
                aDauerauftragVm.Row = mDb.Dauerauftrag.GetSingleRow(aDauerauftragVm.Row.ID, false);
            }
            else if (aDauerauftragVm.Row.Status == DtoStatus.Deleted)
            {
                if (aDauerauftragVm.Id >= 0)
                {
                    // Dauerauftrag löschen
                    this.mDb.Dauerauftrag.DeleteSingleRow(aDauerauftragVm.Row);

                    return;
                }
            }
        }
    }
}