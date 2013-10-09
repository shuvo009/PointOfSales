using MahApps.Metro.Controls;
using Telerik.Windows.Controls;
using whostpos.ViewModel;

namespace whostpos.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly MainViewModel _mainViewModel;
        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Windows8TouchTheme();
            InitializeComponent();
            _mainViewModel = new MainViewModel { ContentPresenter = MainContentPresenter };
            DataContext = _mainViewModel;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !_mainViewModel.ShowMessage.ShowYesNoMessage("Are you Sure Are You Want To Exit ?");
            base.OnClosing(e);
        }
    }
}
