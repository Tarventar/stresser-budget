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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace StresserBudget.Components
{
    /// <summary>
    /// Interaction logic for BudgetVisualizerControl.xaml
    /// </summary>
    public partial class BudgetVisualizerControl : UserControl
    {
        public BudgetVisualizerControl()
        {
            InitializeComponent();

            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            this.mDtpEnde.SelectedDate = DateTime.Today;
            this.mDtpStart.SelectedDate = DateTime.Today.Subtract(new TimeSpan(90, 0,0,0));
        }

        public bool AutoRender { get; set; } = false;

        public string[] SupportedModes
        {
            get
            {
                return this.mCmbModus.ItemsSource as string[];
            }
            set
            {
                this.mCmbModus.ItemsSource = value;
                if (value != null && value.Length > 0)
                {
                    this.mCmbModus.SelectedIndex = 0;
                }
            }
        }

        public IPlotRenderer PlotRenderer
        {
            get
            {
                return this.DataContext as IPlotRenderer;
            }
            set
            {
                this.DataContext = value;
                this.mPlotView.Model = null;
                this.IsEnabled = value != null;

                if (this.AutoRender && value != null)
                {
                    this.RenderView();
                }
            }
        }

        private void RenderView()
        {
            var lMode = this.mCmbModus.SelectedItem as string;
            if (string.IsNullOrEmpty(lMode))
            {
                MessageBox.Show("Ungültiger Modus.");
            }

            this.mPlotView.Model = this.PlotRenderer?.RenderPlot(lMode, this.mDtpStart.SelectedDate.Value, this.mDtpEnde.SelectedDate.Value);
        }

        private void mBtnAnzeigen_Click(object sender, RoutedEventArgs e)
        {
            this.RenderView();
        }
    }
}
