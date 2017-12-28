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
using System.Windows.Shapes;
using SdaWpfLib.General;

namespace StresserBudget.Dialogs
{
    /// <summary>
    /// Interaction logic for RenameDialog.xaml
    /// </summary>
    public partial class RenameDialog : Window
    {
        public RenameDialog()
        {
            InitializeComponent();
        }

        public string Bezeichnung
        {
            get
            {
                return this.mTxtBezeichnung.Text;
            }
            set
            {
                this.mTxtBezeichnung.Text = value;
            }
        }

        private void mBtnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RealFocusManager.SetFocus(this.mTxtBezeichnung);
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(this.Bezeichnung))
                {
                    e.Handled = true;
                    this.DialogResult = true;
                    this.Close();
                }
            }
        }
    }
}
