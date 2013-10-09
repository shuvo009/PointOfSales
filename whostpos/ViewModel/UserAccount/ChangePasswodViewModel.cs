using System;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.BaseClass;
using whostpos.Core.Classes;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.UserAccount
{
    public class ChangePasswodViewModel : BaseViewModel
    {
        private string _currentPassword;
        private string _newPassword;
        private string _conformPassword;

        public ChangePasswodViewModel()
            : base("Change Password")
        {
            ChangePasswodCommand = new RelayCommand(ChangePasswordExecute, ChangePasswordCanExecute);
        }

        public RelayCommand ChangePasswodCommand { get; private set; }


        #region Event
        public delegate void PasswordBoxClear();
        public event PasswordBoxClear OnPasswordBoxClear;

        protected virtual void OnOnPasswordBoxClear()
        {
            PasswordBoxClear handler = OnPasswordBoxClear;
            if (handler != null) handler();
        }

        #endregion

        #region Property

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                RaisePropertyChanged(() => CurrentPassword);
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                RaisePropertyChanged(() => NewPassword);
            }
        }

        public string ConformPassword
        {
            get { return _conformPassword; }
            set
            {
                _conformPassword = value;
                RaisePropertyChanged(() => ConformPassword);
            }
        }

        #endregion

        #region Command Execute

        private void ChangePasswordExecute()
        {
            IsBusy = false;
            try
            {
                var isValid = true;
                if (NewPassword.Equals(ConformPassword))
                {
                    var encryptedNewPassword = MiraculousMethods.Sha256Encrypt(NewPassword);
                    var encryptedCurrentPassword = MiraculousMethods.Sha256Encrypt(CurrentPassword);
                    if (WposContext.UserAccountRepository.ChangePassword(UserAuthorize.Authorize.Username, encryptedCurrentPassword,
                        encryptedNewPassword))
                    {
                        WposContext.Save();
                        OnOnPasswordBoxClear();
                        ShowMessage.ShowMessage("Password Change Successfully", MessageBoxImage.Ok);
                    }
                    else
                    {
                        isValid = false;
                    }
                }
                else
                {
                    isValid = false;
                }

                if (!isValid)
                    ShowMessage.ShowMessage("Wrong Password !", MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage("Wrong Password !", MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private bool ChangePasswordCanExecute()
        {
            return !String.IsNullOrEmpty(CurrentPassword)
                   && !String.IsNullOrEmpty(NewPassword)
                   && !String.IsNullOrEmpty(ConformPassword);
        }

        #endregion
    }
}
