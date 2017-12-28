using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace Logic.ViewModels
{
    public interface IPlotRenderer
    {
        PlotModel RenderPlot(string aMode, DateTime aStartDate, DateTime aEndDate);
    }
}
