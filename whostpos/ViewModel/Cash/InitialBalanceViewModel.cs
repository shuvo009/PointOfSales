using System;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Cash
{
    public class InitialBalanceViewModel : BasicViewModel<OpeningClosingBalance>
    {
        public InitialBalanceViewModel() : base("Initial Balance")
        {
            InternalCollection = WposContext.OpeningClosingBlanceRepository.GetInitialBalances();
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = CreateInitialBalance;
            CanExecuteFunc = x => x != null && x.Debit > 0;
        }

        #region Private Methods

        private void CreateInitialBalance(OpeningClosingBalance entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Save?")) return;
            entity.IsInitial = true;
            entity.EntityDate = DateTime.Now;
            WposContext.OpeningClosingBlanceRepository.Add(entity);
            WposContext.Save();
            InternalCollection.Add(new OpeningClosingBalance
            {
                Id = entity.Id,
                Debit = entity.Debit,
                EntityDate = entity.EntityDate
            });
            NewCommandExecute();
            ShowMessage.ShowMessage(String.Format("{0} Is Saved.", Title), MessageBoxImage.Ok);
        }

        #endregion

    }
}