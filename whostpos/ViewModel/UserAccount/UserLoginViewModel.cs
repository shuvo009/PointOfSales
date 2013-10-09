using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using whostpos.Class;
using whostpos.Core.BaseClass;
using whostpos.Core.Classes;

namespace whostpos.ViewModel.UserAccount
{
    public class UserLoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public UserLoginViewModel()
            : base("Login")
        {
            LoginCommand = new RelayCommand(LoginCommandExecute, LoginCommandCanExecute);
            KeydownCommand = new RelayCommand<KeyEventArgs>(KeydownCommandExecute);
        }

        #region Poperty
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChanged(() => Username);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        #endregion

        #region Event
        public delegate void PasswordBoxClear();

        public delegate void GetContentPresenter();

        public event PasswordBoxClear OnPasswordBoxClear;
        public event GetContentPresenter OnGetContentPresenter;

        protected virtual void OnOnGetContentPresenter()
        {
            GetContentPresenter handler = OnGetContentPresenter;
            if (handler != null) handler();
        }

        protected virtual void OnOnPasswordBoxClear()
        {
            PasswordBoxClear handler = OnPasswordBoxClear;
            if (handler != null) handler();
        }

        #endregion

        public RelayCommand LoginCommand { get; private set; }
        public RelayCommand<KeyEventArgs> KeydownCommand { get; private set; }

        private async void LoginCommandExecute()
        {
            IsBusy = true;
            try
            {
               await Task.Factory.StartNew(() =>
                                            {
                                                if (UserAuthorize.Authorize.UserLogin(Username, Password))
                                                {
                                                    Messenger.Default.Send("Login Success");
                                                }
                                                else
                                                {
                                                    ShowMessage.ShowMessage("Wrong Username Or Password", Core.Helpers.MessageBoxImage.Error);
                                                }
                                                IsBusy = false;
                                            });
            }

            catch (Exception ex)
            {
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage("Login Failed", Core.Helpers.MessageBoxImage.Error);
            }
            finally
            {
                OnOnPasswordBoxClear();
                IsBusy = false;
            }
        }

        private bool LoginCommandCanExecute()
        {
            return !String.IsNullOrEmpty(Username) && !String.IsNullOrEmpty(Password);
        }

        private void KeydownCommandExecute(KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                LoginCommandExecute();
        }
    }
}
