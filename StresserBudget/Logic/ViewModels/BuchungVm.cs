using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;

namespace Logic.ViewModels
{
    public class BuchungVm : BaseStatusVm
    {
        private BuchungRow mRow;
        private BudgetVm mBudgetVm;

        internal BuchungVm(BudgetVm aBudgetVm)
        {
            this.Row = BuchungRow.InitEmpty();
            this.mBudgetVm = aBudgetVm;
            this.Row.IDBudget = this.mBudgetVm.Id;
        }

        internal BuchungVm(BudgetVm aBudgetVm, BuchungRow aRow)
        {
            this.Row = aRow;
            this.mBudgetVm = aBudgetVm;
        }

        internal override DtoStatus RowStatus
        {
            get
            {
                return this.Row.Status;
            }
            set
            {
                this.Row.Status = value;
            }
        }

        public int Id
        {
            get
            {
                return this.Row.ID;
            }
        }

        public int IdBudget
        {
            get
            {
                return this.Row.IDBudget;
            }
            internal set
            {
                if (this.Row.IDBudget == value)
                {
                    return;
                }

                this.Row.IDBudget = value;
                this.ChangingRow(nameof(IdBudget));
            }
        }

        public string Bezeichnung
        {
            get
            {
                return this.Row.Bezeichnung;
            }
            set
            {
                this.Row.Bezeichnung = value;
                this.ChangingRow(nameof(Bezeichnung));
            }
        }

        public DateTime Datum
        {
            get
            {
                return this.Row.Datum;
            }
            set
            {
                this.Row.Datum = value;
                this.ChangingRow(nameof(Datum));
            }
        }

        public string DatumDisplay
        {
            get
            {
                return this.Datum.ToString("dd.MM.yyyy");
            }
        }

        public int Betrag
        {
            get
            {
                return this.Row.Betrag;
            }
            internal set
            {
                this.Row.Betrag = value;
                this.ChangingRow(nameof(Betrag));
            }
        }

        public decimal BetragDisplay
        {
            get
            {
                return MoneyManager.GetFrankenRappen(this.Row.Betrag, true);
            }
            set
            {
                this.Betrag = Convert.ToInt32(value * 100);
            }
        }

        public string Bemerkung
        {
            get
            {
                return this.Row.Bemerkung;
            }
            set
            {
                this.Row.Bemerkung = value;
                this.ChangingRow(nameof(Bemerkung));
            }
        }

        public string IstDauerauftrag
        {
            get
            {
                return this.Row.IDDauerauftrag > -1 ? "Ja" : "Nein";
            }
        }

        public void Save()
        {
            if (this.RowStatus == DtoStatus.Created)
            {
                this.mBudgetVm.AddBuchungVm(this);
            }
            else if (this.RowStatus == DtoStatus.Deleted)
            {
                this.mBudgetVm.Buchungen.Remove(this);
            }

            if (this.RowStatus != DtoStatus.Unchanged)
            {
                DataManager.Buchung.SaveBuchung(this);
            }
        }

        public void ReloadFromDb()
        {
            if (this.RowStatus == DtoStatus.Created)
            {
                return;
            }

            DataManager.Buchung.Reload(this);
        }

        protected override IEnumerable<string> ValidateYourself()
        {
            return base.ValidateYourself();
        }

        internal BuchungRow Row
        {
            get
            {
                return this.mRow;
            }
            set
            {
                this.mRow = value;
                this.NotifyPropertyChanged(nameof(IdBudget));
                this.NotifyPropertyChanged(nameof(Bezeichnung));
                this.NotifyPropertyChanged(nameof(Datum));
                this.NotifyPropertyChanged(nameof(DatumDisplay));
                this.NotifyPropertyChanged(nameof(Betrag));
                this.NotifyPropertyChanged(nameof(BetragDisplay));
                this.NotifyPropertyChanged(nameof(Bemerkung));
                this.NotifyPropertyChanged(nameof(IstDauerauftrag));
            }
        }
    }
}
