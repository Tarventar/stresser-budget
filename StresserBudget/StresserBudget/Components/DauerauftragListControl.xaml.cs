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
using SdaWpfLib.EvArgs;
using StresserBudget.Dialogs;

namespace StresserBudget.Components
{
    /// <summary>
    /// Interaction logic for DauerauftragListControl.xaml
    /// </summary>
    public partial class DauerauftragListControl : UserControl
    {
        public event EventHandler<VmEventArgs> SelectionChanged;

        public DauerauftragListControl()
        {
            InitializeComponent();
        }

        public BudgetVm BudgetVm
        {
            get
            {
                return this.DataContext as BudgetVm;
            }
            set
            {
                this.DataContext = value;
                this.mDtgDauerauftraege.ItemsSource = value?.Dauerauftraege;

                this.IsEnabled = value != null;
            }
        }

        private void EditAndSave(DauerauftragVm aDauerauftragVm, string aDialogTitle)
        {
            var lDlg = new EditDauerauftragDialog()
            {
                Owner = Window.GetWindow(this),
                Title = aDialogTitle,
                DauerauftragVm = aDauerauftragVm
            };

            var lResult = lDlg.ShowDialog();
            if (!lResult.HasValue || !lResult.Value)
            {
                aDauerauftragVm.ReloadFromDb();
                return;
            }

            aDauerauftragVm.Save();
            this.mDtgDauerauftraege.SelectedItem = aDauerauftragVm;
        }

        private void mBtnAddDauerauftrag_Click(object sender, RoutedEventArgs e)
        {
            var lDauerauftrag = this.BudgetVm.PrepareDauerauftragVm();
            this.EditAndSave(lDauerauftrag, "Dauerauftrag erstellen");
        }

        private void mDtgDauerauftraege_SingleSelectionChanged(object sender, EventArgs e)
        {
            this.SelectionChanged?.Invoke(this, new VmEventArgs(this.mDtgDauerauftraege.SelectedItem as DauerauftragVm));
        }

        private void mDtgDauerauftraege_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            // iteratively traverse the visual tree
            while ((dep != null) &&
                    !(dep is DataGridRow))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep != null)
            {
                var lDauerauftrag = (dep as DataGridRow).Item as DauerauftragVm;
                this.EditAndSave(lDauerauftrag, "Dauerauftrag bearbeiten");
            }
        }
    }
}
