using System;
using System.Windows.Controls;
using System.Windows.Media;
using whostpos.ViewModel.UserAccount;

namespace whostpos.View.UserAcount
{
    /// <summary>
    /// Interaction logic for UserLoginView.xaml
    /// </summary>
    public partial class UserLoginView : UserControl
    {
        readonly UserLoginViewModel _userLoginViewModel = new UserLoginViewModel();
        public UserLoginView()
        {
            InitializeComponent();
            _userLoginViewModel.OnPasswordBoxClear += userLoginViewModel_OnPasswordBoxClear;
            DataContext = _userLoginViewModel;
        }

        void userLoginViewModel_OnPasswordBoxClear()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                UserPasswordBox.Password = string.Empty;
            }));
        }
    }
}
