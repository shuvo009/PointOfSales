using MahApps.Metro.Controls;
using Telerik.Windows.Controls;
using whostpos.Windows.ViewModel;

namespace whostpos.Windows
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : MetroWindow
    {
        public ProductList()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            InitializeComponent();
            var productListViewModel = new ProductListViewModel();
            productListViewModel.CloseWindow += Close;
            DataContext = productListViewModel;
        }
    }
}
