using MahApps.Metro.Controls;
using Telerik.Windows.Controls;
using whostpos.Windows.ViewModel;

namespace whostpos.Windows
{
    /// <summary>
    /// Interaction logic for CoustomerList.xaml
    /// </summary>
    public partial class CoustomerList : MetroWindow
    {
        public CoustomerList()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            InitializeComponent();
            var customerListViewModel = new CustomerListViewModel();
            customerListViewModel.CloseWindow += Close;
            DataContext = customerListViewModel;
        }
    }
}
