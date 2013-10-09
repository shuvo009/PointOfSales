using System;
using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Bank
{
    public class BankAcountViewModel : BasicViewModel<BankAccounts>
    {
        public BankAcountViewModel()
            : base("Bank Account")
        {
            InternalCollection = WposContext.BankRepository.GetBankAccounts();
            ReportType = ReportType.BankAccount;
            IntialAction();
        }

        private void IntialAction()
        {
            SaveAction = x =>
            {
                if (x.Id > 0)
                    UpdateBankAccount(x);
                else
                    CreateBankAccount(x);
            };

            DeleteAction = DeleteBankAccount;
            ClearEntityAction = x => WposContext.BankRepository.ClearChanges(x);
            RemoveAddedEntityAction = x => WposContext.BankRepository.RemoveAddedEntity(x);
            CanExecuteFunc = x => x != null && string.IsNullOrEmpty(x.Error);
            SelectionChangeAction = x => SelectedItem = WposContext.BankRepository.FindById(x);
        }

        #region Private Methods

        private void CreateBankAccount(BankAccounts entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Save?")) return;
            WposContext.BankRepository.Add(entity);
            WposContext.Save();
            InternalCollection.Add(new BankAccounts
            {
                Id = entity.Id,
                BankName = entity.BankName,
                AccountName = entity.AccountName
            });
            NewCommandExecute();
            ShowMessage.ShowMessage(String.Format("{0} Is Saved.", Title), MessageBoxImage.Ok);
        }

        private void UpdateBankAccount(BankAccounts entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Update?")) return;
            WposContext.BankRepository.Update(entity);
            WposContext.Save();
            var updatedProduct = InternalCollection.Single(x => x.Id == entity.Id);
            updatedProduct.BankName = entity.BankName;
            updatedProduct.AccountName = entity.AccountName;
            ShowMessage.ShowMessage(String.Format("{0} Is Updated.", Title), MessageBoxImage.Ok);
        }

        private void DeleteBankAccount(BankAccounts entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Delete?")) return;
            WposContext.BankRepository.Remove(entity);
            WposContext.Save();
            InternalCollection.Remove(InternalCollection.Single(x => x.Id == entity.Id));
            ShowMessage.ShowMessage(String.Format("{0} Is Deleted.", Title), MessageBoxImage.Ok);
        }

        #endregion
    }
}
