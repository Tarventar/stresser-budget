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
using System.Windows.Shapes;
using Logic.ViewModels;
using SdaWpfLib.General;
using SdaWpfLib.Validation;

namespace StresserBudget.Dialogs
{
    /// <summary>
    /// Interaction logic for EditBuchungDialog.xaml
    /// </summary>
    public partial class EditBuchungDialog : Window
    {
        public EditBuchungDialog()
        {
            InitializeComponent();
        }

        public BuchungVm BuchungVm
        {
            get
            {
                return this.DataContext as BuchungVm;
            }

            set
            {
                this.DataContext = value;
            }
        }

        private void mBtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.TryClose();
        }

        private void TryClose()
        {
            if (!WpfValidator.IsValid(this))
            {
                MessageBox.Show("Es sind noch Validierungsfehler vorhanden.", "Ungültige Eingaben", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (!this.BuchungVm.Validate().ValidOrMessageBox())
            {
                return;
            }

            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }

            RealFocusManager.SetFocus(this.mTxtBezeichnung);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Return) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                e.Handled = true;
                this.TryClose();
            }
        }
    }
}
