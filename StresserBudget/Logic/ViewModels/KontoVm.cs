using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;
using Logic.Extensions;
using OxyPlot;
using OxyPlot.Axes;
using SdaWpfLib.ViewModelBase;

namespace Logic.ViewModels
{
    public class KontoVm : BaseStatusVm, IPlotRenderer
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

        public BudgetVm CreateBudget(string aBezeichnung)
        {
            var lBudget = new BudgetVm(this)
            {
                Bezeichnung = aBezeichnung
            };

            this.Budgets.Add(lBudget);

            if (this.Id >= 0)
            {
                // Direkt speichern
                DataManager.Budget.SaveBudget(lBudget);
            }

            return lBudget;
        }

        public PlotModel RenderPlot(string aMode, DateTime aStartDate, DateTime aEndDate)
        {
            if (aMode.ToLowerInvariant() != "saldoverlauf")
            {
                throw new ArgumentException("Konto unterstützt nur Modus 'SaldoVerlauf'.", nameof(aMode));
            }

            PlotModel lPlotModel = new PlotModel();

            var lDtAxis = new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                StringFormat = "dd.MM.yy",
                Key = "X",
            };

            var lYaxis = new LinearAxis()
            {
                Position = AxisPosition.Left,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineColor = OxyColors.DarkRed,
                ExtraGridlineStyle = LineStyle.LongDash,
                Key = "Y"
            };

            lPlotModel.Axes.Clear();
            lPlotModel.Axes.Add(lDtAxis);
            lPlotModel.Axes.Add(lYaxis);

            foreach (var lBudget in this.Budgets)
            {
                lPlotModel.Series.Add(lBudget.CreateSaldoChangeStairLine(aStartDate, aEndDate, true));
            }

            return lPlotModel;
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
