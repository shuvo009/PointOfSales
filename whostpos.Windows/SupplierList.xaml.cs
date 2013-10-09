using MahApps.Metro.Controls;
using Telerik.Windows.Controls;
using whostpos.Windows.ViewModel;

namespace whostpos.Windows
{
    /// <summary>
    /// Interaction logic for SupplierListViewModel.xaml
    /// </summary>
    public partial class SupplierList : MetroWindow
    {
        public SupplierList()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            InitializeComponent();
            var supplierListViewModel = new SupplierListViewModel();
            supplierListViewModel.CloseWindow += Close;
            DataContext = supplierListViewModel;
        }
    }
}
