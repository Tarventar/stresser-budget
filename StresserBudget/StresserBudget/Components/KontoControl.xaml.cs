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
using Logic;
using Logic.ViewModels;
using SdaWpfLib.EvArgs;
using StresserBudget.Dialogs;

namespace StresserBudget.Components
{
    /// <summary>
    /// Interaction logic for KontoControl.xaml
    /// </summary>
    public partial class KontoControl : UserControl
    {
        public event EventHandler<VmEventArgs> KontoSelected;

        public KontoControl()
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
            }
        }

        private void ReloadKontos()
        {
            this.mCmbKonto.ItemsSource = DataManager.Konto.GetTable();
        }

        private void mBtnAddKonto_Click(object sender, RoutedEventArgs e)
        {
            var lDlg = new RenameDialog()
            {
                Owner = Window.GetWindow(this),
                Title = "Konto erstellen",
                Bezeichnung = string.Empty
            };

            var lResult = lDlg.ShowDialog();
            if (!lResult.HasValue || !lResult.Value || string.IsNullOrEmpty(lDlg.Bezeichnung))
            {
                return;
            }

            string lBezeichnung = lDlg.Bezeichnung;
            var lKonto = DataManager.Konto.CreateKonto(lBezeichnung);
            DataManager.Konto.SaveKonto(lKonto);

            this.ReloadKontos();
        }

        private void mCmbKonto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.KontoVm = this.mCmbKonto.SelectedItem as KontoVm;
            this.KontoSelected?.Invoke(this, new VmEventArgs(this.KontoVm));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            this.ReloadKontos();
        }
    }
}
