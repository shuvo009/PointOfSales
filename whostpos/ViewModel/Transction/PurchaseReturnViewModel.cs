using System;
using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Transction
{
    public class PurchaseReturnViewModel : BasicTransctionViewModel<Suppliers>
    {
        private readonly ISupplierList _supplierList;

        public PurchaseReturnViewModel(ISupplierList supplierList) : base("Purchase Return")
        {
            _supplierList = supplierList;
            ReportType = ReportType.PurchaseReturn;
            SelectedItem.IsSales = false;
            SelectedItem.IsReturn = true;
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = PurchaseReturn;
            CanExecuteFunc = x => x != null && x.TransactionLedgers.Any() && !String.IsNullOrEmpty(x.InvoiceNumber);
            AddToInvoiceCommandCanExecuteFunc = x => TransctionMetaData.Rate > 0
                                                     && TransctionMetaData.Quantity > 0
                                                     && TransctionMetaData.Amount > 0;
        }

        public override void RateCalculatorCommandExecute()
        {
            IsBusy = true;
            if (TransctionMetaData != null && TransctionMetaData.Quantity > 0 && TransctionMetaData.Amount > 0)
            {
                TransctionMetaData.Rate = Convert.ToDouble((TransctionMetaData.Amount / TransctionMetaData.Quantity).ToString("F2"));
            }
            else
            {
                if (TransctionMetaData != null) TransctionMetaData.Rate = 0;
            }
            IsBusy = false;
        }

        public override void AmountCalculatorCommandExecute()
        {
            IsBusy = true;
            if (TransctionMetaData != null)
            {
                TransctionMetaData.Amount = TransctionMetaData.Quantity * TransctionMetaData.Rate;
            }
            IsBusy = false;
        }

        public override void DuePaymentCommandExecute()
        {
            throw new NotImplementedException();
        }

        public override void SelectManCommandExecute()
        {
            SelectedMan = _supplierList.GetSelectedSuppliers();
            SelectedItem.SupplierId = SelectedMan.Id;
        }

        #region Private Methods

        private void PurchaseReturn(Transaction entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Purchase Return?")) return;
            WposContext.TransactionRepository.PurchaseReturn(entity);
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("{0} Is Compete.", Title), MessageBoxImage.Ok);
            PrintCommandExecute(SelectedItem);
            ResetCommandExecute(SelectedItem);
        }

        #endregion
    }
}
