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
using OxyPlot.Series;

namespace Logic.ViewModels
{
    public class BudgetVm : BaseStatusVm, IPlotRenderer
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

        public void Save(bool aWithDependencies)
        {
            DataManager.Budget.SaveBudget(this);

            if (aWithDependencies)
            {
                this.SaveDependencies();
            }
        }

        public BuchungVm PrepareBuchungVm()
        {
            return new BuchungVm(this);
        }

        internal void AddBuchungVm(BuchungVm aBuchungVm)
        {
            if (!this.Buchungen.Contains(aBuchungVm))
            {
                this.Buchungen.Add(aBuchungVm);
            }
        }

        public DauerauftragVm PrepareDauerauftragVm()
        {
            return new DauerauftragVm(this);
        }

        internal void AddDauerauftragVm(DauerauftragVm aDauerauftragVm)
        {
            if (!this.Dauerauftraege.Contains(aDauerauftragVm))
            {
                this.Dauerauftraege.Add(aDauerauftragVm);
            }
        }

        public PlotModel RenderPlot(string aMode, DateTime aStartDate, DateTime aEndDate)
        {
            switch (aMode.ToLowerInvariant())
            {
                case "saldoverlauf":
                    return this.CreateSaldoVerlauf(aStartDate, aEndDate);
                case "inexkasso":
                    return this.CreateInExKasso(aStartDate, aEndDate);
                default:
                    throw new ArgumentException("Nicht unterstützter Modus aufgerufen.", nameof(aMode));
            }
        }

        private PlotModel CreateSaldoVerlauf(DateTime aStartDate, DateTime aEndDate)
        {
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
                Key = "Y",
            };

            lPlotModel.Axes.Clear();
            lPlotModel.Axes.Add(lDtAxis);
            lPlotModel.Axes.Add(lYaxis);

            lPlotModel.Series.Add(this.CreateSaldoChangeStairLine(aStartDate, aEndDate, true));
            //var lPoints = this.CreateSaldoChangePoints(aStartDate, aEndDate);
            //lPlotModel.Series.Add(lPoints);

            return lPlotModel;
        }

        private PlotModel CreateInExKasso(DateTime aStartDate, DateTime aEndDate)
        {
            PlotModel lPlotModel = new PlotModel();
            CategoryAxis lCategories = new CategoryAxis()
            {
                Position = AxisPosition.Bottom,
                Angle = 90,
                Key = "XCat"
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
            lPlotModel.Axes.Add(lCategories);
            lPlotModel.Axes.Add(lYaxis);

            lPlotModel.Series.Add(this.CreateInExKassoBalken(aStartDate, aEndDate, lCategories));

            return lPlotModel;
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
