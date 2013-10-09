using System;
using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.Transction
{
    public class SalesReturnViewModel : BasicTransctionViewModel<Customers>
    {
        private readonly ICustomerList _customerList;

        public SalesReturnViewModel(ICustomerList customerList)
            : base("Sales Return")
        {
            _customerList = customerList;
            ReportType = ReportType.SalesReturn;
            SelectedItem.IsSales =
                SelectedItem.IsReturn = true;
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = SalesReturn;
            CanExecuteFunc = x => x != null && x.TransactionLedgers.Any() && !String.IsNullOrEmpty(x.InvoiceNumber);
            AddToInvoiceCommandCanExecuteFunc = x => TransctionMetaData.SalesRate > 0
                                                     && TransctionMetaData.SalesQuantity > 0
                                                     && TransctionMetaData.Amount > 0;
        }

        #region Base Class Methods

        public override void RateCalculatorCommandExecute()
        {
            IsBusy = true;
            if (TransctionMetaData != null && TransctionMetaData.SalesQuantity > 0 && TransctionMetaData.Amount > 0)
            {
                TransctionMetaData.SalesRate = Convert.ToDouble((TransctionMetaData.Amount / TransctionMetaData.SalesQuantity).ToString("F2"));
            }
            else
            {
                if (TransctionMetaData != null) TransctionMetaData.SalesRate = 0;
            }
            IsBusy = false;
        }

        public override void AmountCalculatorCommandExecute()
        {
            IsBusy = true;
            if (TransctionMetaData != null)
            {
                TransctionMetaData.Amount = TransctionMetaData.SalesQuantity * TransctionMetaData.SalesRate;
            }
            IsBusy = false;
        }

        public override void DuePaymentCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void SelectManCommandExecute()
        {
            SelectedMan = _customerList.GetSelectedCustomer();
            SelectedItem.CustomerId = SelectedMan.Id;
        }
        #endregion

        #region Private Methods

        private void SalesReturn(Transaction entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Sales Return?")) return;
            WposContext.TransactionRepository.SalesReturn(entity);
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("{0} Is Compete.", Title), MessageBoxImage.Ok);
            PrintCommandExecute(SelectedItem);
            ResetCommandExecute(SelectedItem);
        }

        #endregion
    }
}
