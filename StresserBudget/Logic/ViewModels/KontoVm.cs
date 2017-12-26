using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;
using SdaWpfLib.ViewModelBase;

namespace Logic.ViewModels
{
    public class KontoVm : BaseStatusVm
    {
        private KontoRow mRow;
        private ObservableCollection<BudgetVm> mBudgetVms = null;

        internal KontoVm()
        {
            this.Row = KontoRow.InitEmpty();
        }

        internal KontoVm(KontoRow aRow)
        {
            this.Row = aRow;
        }

        public ObservableCollection<BudgetVm> Budgets
        {
            get
            {
                if (this.mBudgetVms == null)
                {
                    this.mBudgetVms = new ObservableCollection<BudgetVm>();

                    if (this.Row.ID >= 0)
                    {
                        // Load from database
                        foreach (var lVm in DataManager.Budget.GetByKonto(this))
                        {
                            this.mBudgetVms.Add(lVm);
                        }
                    }
                }

                return this.mBudgetVms;
            }
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

        protected override IEnumerable<string> ValidateYourself()
        {
            return base.ValidateYourself();
        }

        internal KontoRow Row
        {
            get
            {
                return this.mRow;
            }
            set
            {
                this.mRow = value;
                this.NotifyPropertyChanged(nameof(Bezeichnung));
            }
        }

        internal void SaveDependencies()
        {
            // Save dependencies
            // Budgets
            if (this.mBudgetVms != null)
            {
                foreach (var lBudgetVm in this.Budgets)
                {
                    lBudgetVm.IdKonto = this.Id;
                    DataManager.Budget.SaveBudget(lBudgetVm);
                }
            }
        }
    }
}
