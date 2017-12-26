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
    public class KontoDataManager
    {
        private DbFactory mDb;

        internal KontoDataManager(DbFactory aDb)
        {
            this.mDb = aDb;
        }

        public IEnumerable<KontoVm> GetTable()
        {
            List<KontoVm> lResult = new List<KontoVm>();
            foreach (var lKontoRow in this.mDb.Konto.GetTable())
            {
                lResult.Add(new KontoVm(lKontoRow));
            }

            return lResult;
        }

        internal void SaveKonto(KontoVm aKontoVm)
        {
            if (!aKontoVm.Validate().IsValid)
            {
                throw new DataException("Es sind noch Validierungsfehler auf dem KontoVm vorhanden!");
            }

            if (aKontoVm.Row.Status == DtoStatus.Created)
            {
                // To create
                var lKontoId = mDb.Konto.InsertRow(aKontoVm.Row);

                // Reload from Database
                var lRow = mDb.Konto.GetSingleRow(lKontoId, false);

                aKontoVm.Row = lRow;
            }
            else if (aKontoVm.Row.Status == DtoStatus.Updated)
            {
                // To update
                mDb.Konto.UpdateSingleRow(aKontoVm.Row);

                // Reload from Database
                aKontoVm.Row = mDb.Konto.GetSingleRow(aKontoVm.Row.ID, false);
            }
            else if (aKontoVm.Row.Status == DtoStatus.Deleted)
            {
                if (aKontoVm.Id >= 0)
                {
                    throw new DataException("Aktuell wird ein Löschen des Kontos nicht unterstützt.");
                }
            }

            aKontoVm.SaveDependencies();
        }
    }
}