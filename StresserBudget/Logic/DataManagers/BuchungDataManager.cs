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
    public class BuchungDataManager
    {
        private DbFactory mDb;

        internal BuchungDataManager(DbFactory aDb)
        {
            this.mDb = aDb;
        }

        public IEnumerable<BuchungVm> GetByBudget(BudgetVm aBudgetVm)
        {
            List<BuchungVm> lResult = new List<BuchungVm>();
            if (aBudgetVm.Id >= 0)
            {
                foreach (var lRow in this.mDb.Buchung.GetRowsByBudget(aBudgetVm.Id))
                {
                    lResult.Add(new BuchungVm(aBudgetVm, lRow));
                }
            }

            return lResult;
        }

        internal void Reload(BuchungVm aBuchungVm)
        {
            if (aBuchungVm.Id < 0)
            {
                return;
            }

            // Reload from Database
            var lRow = mDb.Buchung.GetSingleRow(aBuchungVm.Id, false);
            aBuchungVm.Row = lRow;
        }

        internal void SaveBuchung(BuchungVm aBuchungVm)
        {
            if (!aBuchungVm.Validate().IsValid)
            {
                throw new DataException("Es sind noch Validierungsfehler auf dem BuchungVm vorhanden!");
            }

            if (aBuchungVm.Row.Status == DtoStatus.Created)
            {
                // To create
                var lId = mDb.Buchung.InsertRow(aBuchungVm.Row);

                // Reload from Database
                var lRow = mDb.Buchung.GetSingleRow(lId, false);

                aBuchungVm.Row = lRow;
            }
            else if (aBuchungVm.Row.Status == DtoStatus.Updated)
            {
                // To update
                mDb.Buchung.UpdateSingleRow(aBuchungVm.Row);

                // Reload from Database
                aBuchungVm.Row = mDb.Buchung.GetSingleRow(aBuchungVm.Row.ID, false);
            }
            else if (aBuchungVm.Row.Status == DtoStatus.Deleted)
            {
                if (aBuchungVm.Id >= 0)
                {
                    // Buchung löschen
                    this.mDb.Buchung.DeleteSingleRow(aBuchungVm.Row);

                    return;
                }
            }
        }
    }
}