using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;

namespace Logic.ViewModels
{
    public class DauerauftragVm : BaseStatusVm
    {
        private DauerauftragRow mRow;
        private BudgetVm mBudgetVm;

        internal DauerauftragVm(BudgetVm aBudgetVm)
        {
            this.Row = DauerauftragRow.InitEmpty();
            this.mBudgetVm = aBudgetVm;
            this.Row.IDBudget = this.mBudgetVm.Id;
        }

        internal DauerauftragVm(BudgetVm aBudgetVm, DauerauftragRow aRow)
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

        public DateTime GueltigAb
        {
            get
            {
                return this.Row.GueltigAb;
            }
            set
            {
                this.Row.GueltigAb = value;
                this.ChangingRow(nameof(GueltigAb));
                this.NotifyPropertyChanged(nameof(GueltigAbDisplay));
            }
        }

        public DateTime GueltigBis
        {
            get
            {
                return this.Row.GueltigBis;
            }
            set
            {
                this.Row.GueltigBis = value;
                this.ChangingRow(nameof(GueltigBis));
                this.NotifyPropertyChanged(nameof(GueltigBisDisplay));
            }
        }

        public string GueltigAbDisplay
        {
            get
            {
                return this.GueltigAb.ToString("dd.MM.yyyy");
            }
        }

        public string GueltigBisDisplay
        {
            get
            {
                return this.GueltigBis.ToString("dd.MM.yyyy");
            }
        }

        public int Lauftag
        {
            get
            {
                return this.Row.Lauftag;
            }
            set
            {
                this.Row.Lauftag = value;
                this.ChangingRow(nameof(Lauftag));
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
                this.NotifyPropertyChanged(nameof(BetragDisplay));
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

        public void Save()
        {
            if (this.RowStatus == DtoStatus.Created)
            {
                this.mBudgetVm.AddDauerauftragVm(this);
            }
            else if (this.RowStatus == DtoStatus.Deleted)
            {
                this.mBudgetVm.Dauerauftraege.Remove(this);
            }

            if (this.RowStatus != DtoStatus.Unchanged)
            {
                DataManager.Dauerauftrag.SaveDauerauftrag(this);
            }
        }

        public void ReloadFromDb()
        {
            if (this.RowStatus == DtoStatus.Created)
            {
                return;
            }

            DataManager.Dauerauftrag.Reload(this);
        }

        protected override IEnumerable<string> ValidateYourself()
        {
            return base.ValidateYourself();
        }

        internal DauerauftragRow Row
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
                this.NotifyPropertyChanged(nameof(GueltigAb));
                this.NotifyPropertyChanged(nameof(GueltigAbDisplay));
                this.NotifyPropertyChanged(nameof(GueltigBis));
                this.NotifyPropertyChanged(nameof(GueltigBisDisplay));
                this.NotifyPropertyChanged(nameof(Lauftag));
                this.NotifyPropertyChanged(nameof(Betrag));
                this.NotifyPropertyChanged(nameof(BetragDisplay));
            }
        }
    }
}
