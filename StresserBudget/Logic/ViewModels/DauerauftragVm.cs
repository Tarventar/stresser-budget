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
            }
        }

        public int Lauftag
        {
            get
            {
                return this.Row.Lauftag;
            }
            internal set
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
            }
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
                this.NotifyPropertyChanged(nameof(GueltigBis));
                this.NotifyPropertyChanged(nameof(Lauftag));
                this.NotifyPropertyChanged(nameof(Betrag));
            }
        }
    }
}
