using System.Windows.Controls;
using whostpos.ViewModel.UserAccount;

namespace whostpos.View.UserAcount
{
    /// <summary>
    /// Interaction logic for UserAccountView.xaml
    /// </summary>
    public partial class UserAccountView : UserControl
    {
        public UserAccountView()
        {
            InitializeComponent();
            var userAcountViewModel = new UserAccountViewModel();
            userAcountViewModel.OnPasswordBoxClear += userAcountViewModel_OnPasswordBoxClear;
            DataContext = userAcountViewModel;
        }

        void userAcountViewModel_OnPasswordBoxClear()
        {
            UserPasswordBox.Password = string.Empty;
        }
    }
}
