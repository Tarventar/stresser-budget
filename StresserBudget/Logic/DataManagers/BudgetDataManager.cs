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
    public class BudgetDataManager
    {
        private DbFactory mDb;

        internal BudgetDataManager(DbFactory aDb)
        {
            this.mDb = aDb;
        }

        public IEnumerable<BudgetVm> GetByKonto(KontoVm aKontoVm)
        {
            List<BudgetVm> lResult = new List<BudgetVm>();
            if (aKontoVm.Id >= 0)
            {
                foreach (var lBudgetRow in this.mDb.Budget.GetRows(aKontoVm.Id))
                {
                    lResult.Add(new BudgetVm(aKontoVm, lBudgetRow));
                }
            }

            return lResult;
        }

        internal void SaveBudget(BudgetVm aBudgetVm)
        {
            if (!aBudgetVm.Validate().IsValid)
            {
                throw new DataException("Es sind noch Validierungsfehler auf dem BudgetVm vorhanden!");
            }

            if (aBudgetVm.Row.Status == DtoStatus.Created)
            {
                // To create
                var lId = mDb.Budget.InsertRow(aBudgetVm.Row);

                // Reload from Database
                var lRow = mDb.Budget.GetSingleRow(lId, false);

                aBudgetVm.Row = lRow;
            }
            else if (aBudgetVm.Row.Status == DtoStatus.Updated)
            {
                // To update
                mDb.Budget.UpdateSingleRow(aBudgetVm.Row);

                // Reload from Database
                aBudgetVm.Row = mDb.Budget.GetSingleRow(aBudgetVm.Row.ID, false);
            }
            else if (aBudgetVm.Row.Status == DtoStatus.Deleted)
            {
                if (aBudgetVm.Id >= 0)
                {
                    // Alle buchungen löschen
                    this.mDb.Buchung.DeleteRowsByBudget(aBudgetVm.Id);
                    // Alle Daueraufträge löschen
                    this.mDb.Dauerauftrag.DeleteRowsByBudget(aBudgetVm.Id);
                    // Budget löschen
                    this.mDb.Budget.DeleteSingleRow(aBudgetVm.Row);

                    return;
                }
            }

            aBudgetVm.SaveDependencies();
        }
    }
}