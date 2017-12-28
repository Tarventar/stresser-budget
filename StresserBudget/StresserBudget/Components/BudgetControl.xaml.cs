using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace StresserBudget.Components
{
    /// <summary>
    /// Interaction logic for BudgetControl.xaml
    /// </summary>
    public partial class BudgetControl : UserControl
    {
        public BudgetControl()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            this.KontoVm = null;
        }

        public KontoVm KontoVm
        {
            get
            {
                return this.DataContext as KontoVm;
            }
            set
            {
                this.DataContext = value;
                this.mBudgetListControl.KontoVm = value;
                this.mDauerauftragListControl.BudgetVm = null;
                this.mBuchungListControl.BudgetVm = null;

                this.IsEnabled = value != null;
            }
        }

        private void mBudgetListControl_SelectionChanged(object sender, SdaWpfLib.EvArgs.VmEventArgs e)
        {
            var lBudget = e.GetVm<BudgetVm>();
            this.mDauerauftragListControl.BudgetVm = lBudget;
            this.mBuchungListControl.BudgetVm = lBudget;
            this.mBudgetVisualizerControl.PlotRenderer = lBudget;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
