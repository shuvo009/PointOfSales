using System;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Bank
{
    public class BankDepositViewModel : BasicViewModel<BankTransactions>
    {
        private BankAcountViewModel _bankAcountViewModel;

        public BankDepositViewModel()
            : base("Bank Deposit")
        {
            BankAcountViewModel = new BankAcountViewModel();

            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = DepositToBank;
            ClearEntityAction = x => WposContext.BankRepository.ClearChanges(BankAcountViewModel.SelectedItem);
            SelectionChangeAction = x =>
            {
                SelectedItem = new BankTransactions();
                BankAcountViewModel.SelectedItem = WposContext.BankRepository.FindById(x);
                if (BankAcountViewModel.SelectedItem == null) return;
                if (SelectedItem.BankAccount == null) SelectedItem.BankAccount = new BankAccounts();
                SelectedItem.BankAccountId = BankAcountViewModel.SelectedItem.Id;
                SelectedItem.BankAccount = BankAcountViewModel.SelectedItem;
            };

            CanExecuteFunc = x => x != null && x.Deposit > 0 && !string.IsNullOrEmpty(x.ChequeNumber);
        }

        #region Property

        public BankAcountViewModel BankAcountViewModel
        {
            get { return _bankAcountViewModel; }
            set
            {
                _bankAcountViewModel = value;
                RaisePropertyChanged(() => BankAcountViewModel);
            }
        }

        #endregion

        #region Private Methods

        private void DepositToBank(BankTransactions entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Deposit?")) return;
            WposContext.BankRepository.Deposit(entity.BankAccountId, entity);
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("Deposit in {0} Successfully.", entity.BankAccount.BankName),
                MessageBoxImage.Ok);
        }

        #endregion
    }
}