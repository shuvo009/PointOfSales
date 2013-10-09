using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace whostpos.View
{
    /// <summary>
    /// Interaction logic for CompanyInformationView.xaml
    /// </summary>
    public partial class CompanyInformationView : UserControl
    {
        public CompanyInformationView()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            InitializeComponent();
        }
    }
}
