using System;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.Expenses
{
    public class ExpenceViewModel : BasicViewModel<Entitys.Entites.Expenses>
    {
        public ExpenceViewModel()
            : base("Expense")
        {
            SelectedItem.EntityDate = DateTime.Today;
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = Expence;
            RemoveAddedEntityAction = x => WposContext.ExpensesRepository.RemoveAddedEntity(x);
            CanExecuteFunc = x => x != null && !String.IsNullOrEmpty(x.Particulars) && x.Amount > 0;
        }

        #region Private Methods
        private void Expence(Entitys.Entites.Expenses entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Save?")) return;
            WposContext.ExpensesRepository.Add(entity);
            WposContext.Save();
            NewCommandExecute();
            ShowMessage.ShowMessage(String.Format("{0} Is Saved.", Title), MessageBoxImage.Ok);
        } 
        #endregion
    }

}
