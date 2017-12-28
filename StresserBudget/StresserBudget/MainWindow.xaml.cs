using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logic.ViewModels;

namespace StresserBudget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mKontoControl_KontoSelected(object sender, SdaWpfLib.EvArgs.VmEventArgs e)
        {
            this.mBudgetControl.KontoVm = e.GetVm<KontoVm>();
            this.mVisualizerControl.PlotRenderer = this.mBudgetControl.KontoVm;
        }
    }
}
