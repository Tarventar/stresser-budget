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
                this.NotifyPropertyChanged(nameof(Betrag));
            }
        }
    }
}
