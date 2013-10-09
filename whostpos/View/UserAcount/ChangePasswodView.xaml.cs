using System;
using System.Windows.Controls;
using whostpos.ViewModel.UserAccount;

namespace whostpos.View.UserAcount
{
    /// <summary>
    /// Interaction logic for ChangePasswodView.xaml
    /// </summary>
    public partial class ChangePasswodView : UserControl
    {
        public ChangePasswodView()
        {
            InitializeComponent();
            var changePasswordViewModel = new ChangePasswodViewModel();
            changePasswordViewModel.OnPasswordBoxClear += changePasswordViewModel_OnPasswordBoxClear;
            DataContext = changePasswordViewModel;
        }

        void changePasswordViewModel_OnPasswordBoxClear()
        {
            CurrentPasswordBox.Password =
                NewPasswordBox.Password =
                    ConformPasswordBox.Password = String.Empty;
        }
    }
}
