using System;
using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Data;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.UserAccount
{
    public class UserAccountViewModel : BasicViewModel<Entitys.Entites.UserAccount>
    {
        private bool _enableUserNameTextBox;

        public UserAccountViewModel()
            : base("User Account")
        {
            InternalCollection = WposContext.UserAccountRepository.GetUserAccounts();
            NewItem();
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = x =>
            {
                if (x.Id > 0)
                    UpdateUser(x);
                else
                    CreateUser(x);
            };

            DeleteAction = DeleteUser;

            RemoveAddedEntityAction = x => WposContext.UserAccountRepository.RemoveAddedEntity(x);
            ClearEntityAction = x => WposContext.UserAccountRepository.ClearChanges(x);

            SelectionChangeAction = x =>
            {
                SelectedItem = WposContext.UserAccountRepository.FindById(x);
                EnableUserNameTextBox = false;
                OnOnPasswordBoxClear();
            };

            CanExecuteFunc =
                x => x != null && !String.IsNullOrEmpty(x.Username) && x.Permissions.Any();
        }

        public bool EnableUserNameTextBox
        {
            get { return _enableUserNameTextBox; }
            set
            {
                _enableUserNameTextBox = value;
                RaisePropertyChanged(() => EnableUserNameTextBox);
            }
        }

        #region Event

        public delegate void PasswordBoxClear();

        public event PasswordBoxClear OnPasswordBoxClear;

        protected virtual void OnOnPasswordBoxClear()
        {
            PasswordBoxClear handler = OnPasswordBoxClear;
            if (handler != null) handler();
        }

        #endregion

        public override void ResetCommandExecute(Entitys.Entites.UserAccount entity)
        {
            NewItem();
        }

        public override void NewCommandExecute()
        {
            NewItem();
        }

        #region Private Methods

        private void CreateUser(Entitys.Entites.UserAccount entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Save?")) return;
            if (String.IsNullOrEmpty(entity.Password))
            {
                ShowMessage.ShowMessage("Please Enter Password", MessageBoxImage.Error);
                return;
            }
            if (!WposContext.UserAccountRepository.IsNew(entity.Username))
            {
                ShowMessage.ShowMessage(
                    string.Format("{0} is already exist.\nPlease Choose Another Username.", entity.Username),
                    MessageBoxImage.Error);
                return;
            }
            entity.Password = MiraculousMethods.Sha256Encrypt(entity.Password);
            WposContext.UserAccountRepository.Add(entity);
            WposContext.Save();
            InternalCollection.Add(new Entitys.Entites.UserAccount
            {
                Id = entity.Id,
                Username = entity.Username
            });
            NewCommandExecute();
            ShowMessage.ShowMessage(String.Format("{0} Is Saved.", Title), MessageBoxImage.Ok);
        }

        private void UpdateUser(Entitys.Entites.UserAccount entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Update?")) return;
            entity.Password = MiraculousMethods.Sha256Encrypt(entity.Password);
            WposContext.UserAccountRepository.Update(entity);
            WposContext.Save();
            OnOnPasswordBoxClear();
            ShowMessage.ShowMessage(String.Format("{0} Is Updated.", entity.Username), MessageBoxImage.Ok);
        }

        private void DeleteUser(Entitys.Entites.UserAccount entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Delete?")) return;
            WposContext.UserAccountRepository.Remove(entity);
            WposContext.Save();
            InternalCollection.Remove(InternalCollection.Single(x => x.Id == entity.Id));
            OnOnPasswordBoxClear();
            ShowMessage.ShowMessage(String.Format("{0} Is Deleted.", entity.Username), MessageBoxImage.Ok);
        }

        private void NewItem()
        {
            EnableUserNameTextBox = true;
            SelectedItem = new Entitys.Entites.UserAccount
            {
                Permissions = new UserPermissions().GetUserPermissions()
            };
            OnOnPasswordBoxClear();
        }
        #endregion
    }
}
