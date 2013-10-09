using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using whostpos.Class;
using whostpos.Core.BaseClass;
using whostpos.Core.Classes;
using whost.License.Validator.Activitor;
using whostpos.Domain.Context;

namespace whostpos.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isMinimized;
        private Visibility _showRibbon = Visibility.Collapsed;

        public MainViewModel()
            : base("WhostPlusPOS")
        {
            MenuCommand = new RelayCommand<string>(MenuCommandExecute);
            LogoutCommand = new RelayCommand(LogoutCommandExecute);
            ApplicationCloseCommand = new RelayCommand(ApplicationCloseCommandExecute);
            IsMinimized = true;
            RegisterMessaging();

            CheckLicense();
        }

        private void RegisterMessaging()
        {
            Messenger.Default.Register(this,
              new Action<string>(messages =>
              {
                  if (messages.Equals("Login Success"))
                  {
                      ShowRibbon = Visibility.Visible;
                      MenuCommandExecute("Home");
                  }
                  else if (messages.Equals("Product Activated"))
                  {
                      MenuCommandExecute("User Login");
                  }
              }));

        }

        public ContentPresenter ContentPresenter { get; set; }
        public RelayCommand<string> MenuCommand { get; private set; }
        public RelayCommand LogoutCommand { get; private set; }
        public RelayCommand ApplicationCloseCommand { get; private set; }

        public bool IsMinimized
        {
            get { return _isMinimized; }
            set
            {
                _isMinimized = value;
                RaisePropertyChanged(() => IsMinimized);
            }
        }

        public Visibility ShowRibbon
        {
            get { return _showRibbon; }
            set
            {
                _showRibbon = value;
                RaisePropertyChanged(() => ShowRibbon);
            }
        }

        private async void MenuCommandExecute(string commandName)
        {
            IsBusy = true;
            IsMinimized = false;
            IsMinimized = true;
            await Task.Factory.StartNew(() =>
                                        {
                                            CommandParser.Parser.LoadFeture(commandName, ContentPresenter);
                                            IsBusy = false;
                                        });

        }

        private void LogoutCommandExecute()
        {
            if (!ShowMessage.ShowYesNoMessage("Are you Sure Are You Want To Logout ?")) return;
            UserAuthorize.Authorize.UserLogout();
            ShowRibbon = Visibility.Collapsed;
            MenuCommandExecute("User Login");
        }

        private void ApplicationCloseCommandExecute()
        {
            if (ShowMessage.ShowYesNoMessage("Are you Sure Are You Want To Exit ?"))
                Application.Current.Shutdown();
        }

        private async void CheckLicense()
        {
            IsBusy = true;
            BusyConyent = "Checking License";
            await Task.Factory.StartNew(() =>
                                        {
                                            using (var db = new Dbesm())
                                            {
                                                var company = db.CompanyInformations.Find(1);
                                            }
                                            MenuCommandExecute(new InitialLicense().Check() ? "User Login" : "Activation");
                                            // MenuCommandExecute(true ? "User Login" : "Activation");
                                            IsBusy = false;
                                            BusyConyent = "Loading...";
                                        });
        }
    }
}