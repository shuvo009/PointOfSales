using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace whostpos.View.Products
{
    /// <summary>
    /// Interaction logic for ProductSetupView.xaml
    /// </summary>
    public partial class ProductSetupView : UserControl
    {
        public ProductSetupView()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            InitializeComponent();
        }
    }
}
