using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace whostpos.View.Bank
{
    /// <summary>
    /// Interaction logic for BankAccountView.xaml
    /// </summary>
    public partial class BankAccountView : UserControl
    {
        public BankAccountView()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            InitializeComponent();
        }
    }
}
