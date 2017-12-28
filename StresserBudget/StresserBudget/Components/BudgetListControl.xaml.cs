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
using Logic;
using Logic.ViewModels;
using SdaWpfLib.EvArgs;
using StresserBudget.Dialogs;

namespace StresserBudget.Components
{
    /// <summary>
    /// Interaction logic for BudgetListControl.xaml
    /// </summary>
    public partial class BudgetListControl : UserControl
    {
        public event EventHandler<VmEventArgs> SelectionChanged;

        public BudgetListControl()
        {
            InitializeComponent();
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
                this.mDtgBudgets.ItemsSource = value?.Budgets;
            }
        }

        private void EditAndSave(BudgetVm aBudget, string aDialogText)
        {
            var lDlg = new RenameDialog()
            {
                Owner = Window.GetWindow(this),
                Title = aDialogText,
                Bezeichnung = aBudget.Bezeichnung
            };

            var lResult = lDlg.ShowDialog();
            if (!lResult.HasValue || !lResult.Value || string.IsNullOrEmpty(lDlg.Bezeichnung))
            {
                return;
            }

            aBudget.Bezeichnung = lDlg.Bezeichnung;
            aBudget.Save(false);
        }

        private void mDtgBudgets_SingleSelectionChanged(object sender, EventArgs e)
        {
            this.SelectionChanged?.Invoke(this, new VmEventArgs(this.mDtgBudgets.SelectedItem as BudgetVm));
        }

        private void mBtnAddBudget_Click(object sender, RoutedEventArgs e)
        {
            var lDlg = new RenameDialog()
            {
                Owner = Window.GetWindow(this),
                Title = "Budget erstellen",
                Bezeichnung = string.Empty
            };

            var lResult = lDlg.ShowDialog();
            if (!lResult.HasValue || !lResult.Value || string.IsNullOrEmpty(lDlg.Bezeichnung))
            {
                return;
            }

            string lBezeichnung = lDlg.Bezeichnung;
            var lBudget = this.KontoVm.CreateBudget(lBezeichnung);

            this.mDtgBudgets.SelectedItem = lBudget;
        }

        private void mDtgBudgets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
                var lBudget = (dep as DataGridRow).Item as BudgetVm;
                this.EditAndSave(lBudget, "Budget bearbeiten");
            }
        }
    }
}
