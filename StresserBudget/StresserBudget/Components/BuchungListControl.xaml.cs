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
    /// Interaction logic for BuchungListControl.xaml
    /// </summary>
    public partial class BuchungListControl : UserControl
    {
        public event EventHandler<VmEventArgs> SelectionChanged;

        public BuchungListControl()
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
                this.mDtgBuchungen.ItemsSource = value?.Buchungen;

                this.IsEnabled = value != null;
            }
        }

        private void EditAndSave(BuchungVm aBuchungVm, string aDialogTitle)
        {
            var lDlg = new EditBuchungDialog()
            {
                Owner = Window.GetWindow(this),
                Title = aDialogTitle,
                BuchungVm = aBuchungVm
            };

            var lResult = lDlg.ShowDialog();
            if (!lResult.HasValue || !lResult.Value)
            {
                aBuchungVm.ReloadFromDb();
                return;
            }

            aBuchungVm.Save();
            this.mDtgBuchungen.SelectedItem = aBuchungVm;
        }

        private void mDtgBuchungen_SingleSelectionChanged(object sender, EventArgs e)
        {
            this.SelectionChanged?.Invoke(this, new VmEventArgs(this.mDtgBuchungen.SelectedItem as BuchungVm));
        }

        private void mDtgBuchungen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
                var lBuchung = (dep as DataGridRow).Item as BuchungVm;
                this.EditAndSave(lBuchung, "Buchung bearbeiten");
            }
        }

        private void mBtnAddBuchung_Click(object sender, RoutedEventArgs e)
        {
            var lBuchung = this.BudgetVm.PrepareBuchungVm();
            this.EditAndSave(lBuchung, "Buchung erstellen");
        }
    }
}
