using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;

namespace Logic.ViewModels
{
    public class BudgetVm : BaseStatusVm
    {
        private BudgetRow mRow;
        private KontoVm mKontoVm;
        private ObservableCollection<BuchungVm> mBuchungVms = null;
        private ObservableCollection<DauerauftragVm> mDauerauftragVms = null;

        internal BudgetVm(KontoVm aKontoVm)
        {
            this.Row = BudgetRow.InitEmpty();
            this.mKontoVm = aKontoVm;
            this.Row.IDKonto = this.mKontoVm.Id;
        }

        internal BudgetVm(KontoVm aKontoVm, BudgetRow aRow)
        {
            this.Row = aRow;
            this.mKontoVm = aKontoVm;
        }

        public ObservableCollection<BuchungVm> Buchungen
        {
            get
            {
                if (this.mBuchungVms == null)
                {
                    this.mBuchungVms = new ObservableCollection<BuchungVm>();

                    if (this.Row.ID >= 0)
                    {
                        // Load from database
                        foreach (var lVm in DataManager.Buchung.GetByBudget(this))
                        {
                            this.mBuchungVms.Add(lVm);
                        }
                    }
                }

                return this.mBuchungVms;
            }
        }

        public ObservableCollection<DauerauftragVm> Dauerauftraege
        {
            get
            {
                if (this.mDauerauftragVms == null)
                {
                    this.mDauerauftragVms = new ObservableCollection<DauerauftragVm>();

                    if (this.Row.ID >= 0)
                    {
                        // Load from database
                        foreach (var lVm in DataManager.Dauerauftrag.GetByBudget(this))
                        {
                            this.mDauerauftragVms.Add(lVm);
                        }
                    }
                }

                return this.mDauerauftragVms;
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

        public int IdKonto
        {
            get
            {
                return this.Row.IDKonto;
            }
            internal set
            {
                if (this.Row.IDKonto == value)
                {
                    return;
                }

                this.Row.IDKonto = value;
                this.ChangingRow(nameof(IdKonto));
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

        internal BudgetRow Row
        {
            get
            {
                return this.mRow;
            }
            set
            {
                this.mRow = value;
                this.NotifyPropertyChanged(nameof(IdKonto));
                this.NotifyPropertyChanged(nameof(Bezeichnung));
            }
        }

        internal void SaveDependencies()
        {
            // Save dependencies
            // Buchungen
            if (this.mBuchungVms != null)
            {
                foreach (var lBuchungVm in this.Buchungen)
                {
                    lBuchungVm.IdBudget = this.Id;
                    DataManager.Buchung.SaveBuchung(lBuchungVm);
                }
            }

            // Dauerauftrag
            if (this.mDauerauftragVms != null)
            {
                foreach (var lDauerauftragVm in this.Dauerauftraege)
                {
                    lDauerauftragVm.IdBudget = this.Id;
                    DataManager.Dauerauftrag.SaveDauerauftrag(lDauerauftragVm);
                }
            }
        }
    }
}
