using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.ViewModels;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Logic.Extensions
{
    public static class BudgetPlotExtensions
    {
        //internal static LineSeries CreateSaldoverlaufLine(this BudgetVm aBudgetVm, DateTime aStartDate, DateTime aEndDate, bool aIncludeStartAndEndSaldo)
        //{
        //    LineSeries lSerie = new LineSeries();
        //    lSerie.Title = aBudgetVm.Bezeichnung;
        //    lSerie.XAxisKey = "X";
        //    lSerie.YAxisKey = "Y";

        //    var lBuchungenRelevant = aBudgetVm.Buchungen.OrderBy(aB => aB.Datum).Where(aB => aB.Datum >= aStartDate && aB.Datum <= aEndDate);
        //    int lSaldo = aBudgetVm.Buchungen.Where(aB => aB.Datum < aStartDate).Sum(aB => aB.Betrag);

        //    if (aIncludeStartAndEndSaldo)
        //    {
        //        // Startwert
        //        lSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(aStartDate), Convert.ToDouble(MoneyManager.GetFrankenRappen(lSaldo, true))));
        //    }

        //    foreach (var lBuchung in lBuchungenRelevant)
        //    {
        //        lSaldo += lBuchung.Betrag;
        //        DataPoint lPoint = new DataPoint(
        //            DateTimeAxis.ToDouble(lBuchung.Datum),
        //            Convert.ToDouble(MoneyManager.GetFrankenRappen(lSaldo, true)));

        //        lSerie.Points.Add(lPoint);
        //    }


        //    if (aIncludeStartAndEndSaldo)
        //    {
        //        // Endwert
        //        lSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(aEndDate), Convert.ToDouble(MoneyManager.GetFrankenRappen(lSaldo, true))));
        //    }

        //    return lSerie;
        //}

        internal static StairStepSeries CreateSaldoChangeStairLine(this BudgetVm aBudgetVm, DateTime aStartDate, DateTime aEndDate, bool aIncludeStartAndEndSaldo)
        {
            StairStepSeries lSerie = new StairStepSeries();
            lSerie.Title = aBudgetVm.Bezeichnung;
            lSerie.XAxisKey = "X";
            lSerie.YAxisKey = "Y";
            lSerie.MarkerType = MarkerType.Circle;

            var lBuchungenRelevant = aBudgetVm.Buchungen.OrderBy(aB => aB.Datum).Where(aB => aB.Datum >= aStartDate && aB.Datum <= aEndDate);
            int lSaldo = aBudgetVm.Buchungen.Where(aB => aB.Datum < aStartDate).Sum(aB => aB.Betrag);

            if (aIncludeStartAndEndSaldo)
            {
                // Startwert
                lSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(aStartDate), Convert.ToDouble(MoneyManager.GetFrankenRappen(lSaldo, true))));
            }

            foreach (var lBuchung in lBuchungenRelevant)
            {
                lSaldo += lBuchung.Betrag;
                DataPoint lPoint = new DataPoint(
                    DateTimeAxis.ToDouble(lBuchung.Datum),
                    Convert.ToDouble(MoneyManager.GetFrankenRappen(lSaldo, true)));

                lSerie.Points.Add(lPoint);
            }

            if (aIncludeStartAndEndSaldo)
            {
                // Endwert
                lSerie.Points.Add(new DataPoint(DateTimeAxis.ToDouble(aEndDate), Convert.ToDouble(MoneyManager.GetFrankenRappen(lSaldo, true))));
            }

            return lSerie;
        }

        internal static StemSeries CreateSaldoChangePoints(this BudgetVm aBudgetVm, DateTime aStartDate, DateTime aEndDate)
        {
            StemSeries lSerie = new StemSeries();
            lSerie.Title = aBudgetVm.Bezeichnung;
            lSerie.XAxisKey = "X";
            lSerie.YAxisKey = "Y";
            lSerie.MarkerType = MarkerType.Circle;
            lSerie.Color = OxyColors.Transparent;
            lSerie.MarkerFill = OxyColors.Black;

            var lBuchungenRelevant = aBudgetVm.Buchungen.OrderBy(aB => aB.Datum).Where(aB => aB.Datum >= aStartDate && aB.Datum <= aEndDate);
            int lSaldo = aBudgetVm.Buchungen.Where(aB => aB.Datum < aStartDate).Sum(aB => aB.Betrag);

            foreach (var lBuchung in lBuchungenRelevant)
            {
                lSaldo += lBuchung.Betrag;
                DataPoint lPoint = new DataPoint(
                    DateTimeAxis.ToDouble(lBuchung.Datum),
                    Convert.ToDouble(lBuchung.BetragDisplay));

                lSerie.Points.Add(lPoint);
            }

            return lSerie;
        }

        internal static ColumnSeries CreateInExKassoBalken(this BudgetVm aBudgetVm, DateTime aStartDate, DateTime aEndDate, CategoryAxis aCategories)
        {
            ColumnSeries lSerie = new ColumnSeries();
            lSerie.LabelPlacement = LabelPlacement.Inside;
            lSerie.LabelFormatString = "{0:#,#.00}";
            lSerie.XAxisKey = "XCat";
            lSerie.YAxisKey = "Y";

            var lBuchungenRelevant = aBudgetVm.Buchungen.OrderBy(aB => aB.Datum).Where(aB => aB.Datum >= aStartDate && aB.Datum <= aEndDate);

            foreach (var lBuchung in lBuchungenRelevant)
            {
                var lItem = new ColumnItem(Convert.ToDouble(lBuchung.BetragDisplay), aCategories.Labels.Count);

                if (lBuchung.Betrag > 0)
                {
                    // Einzahlung
                    lItem.Color = OxyColors.YellowGreen;
                }
                else
                {
                    // Bezahlung
                    lItem.Color = OxyColors.IndianRed;
                }

                lSerie.Items.Add(lItem);
                aCategories.Labels.Add(MakeBalkenLabel(lBuchung.Bezeichnung, lBuchung.Datum));
            }

            return lSerie;
        }

        private static string MakeBalkenLabel(string aBezeichnung, DateTime aDate)
        {
            var lDate = aDate.ToString("dd.MM.yy");
            return $"{aBezeichnung}{Environment.NewLine}{lDate}";
        }
    }
}
