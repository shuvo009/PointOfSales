using System;
using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Transction
{
    public class PurchaseViewModel : BasicTransctionViewModel<Suppliers>
    {
        private readonly ISupplierList _supplierList;

        public PurchaseViewModel(ISupplierList supplierList)
            : base("Purchase")
        {
            _supplierList = supplierList;
            ReportType = ReportType.Purchase;
            SelectedItem.IsReturn = false;
            SelectedItem.IsSales = false;
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = ProductPurchase;
            CanExecuteFunc = x => x != null && x.TransactionLedgers.Any() && !String.IsNullOrEmpty(x.InvoiceNumber);
        
            AddToInvoiceCommandCanExecuteFunc =x=> TransctionMetaData.Quantity > 0
                                                   && TransctionMetaData.Rate > 0
                                                   && TransctionMetaData.SalesRate > 0;
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
            IsBusy = true;
            SelectedItem.DueTotal = SelectedItem.SubTotal - SelectedItem.PaidTotal;
            IsBusy = false;
        }

        public override void SelectManCommandExecute()
        {
            SelectedMan = _supplierList.GetSelectedSuppliers();
            SelectedItem.SupplierId = SelectedMan.Id;
        }

        #region Private Methods

        private void ProductPurchase(Transaction entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Purchase?")) return;
            switch (PaymentMethod)
            {
                case PaymentMethods.Cash:
                    WposContext.TransactionRepository.PurchaseOnCash(entity);
                    break;
                case PaymentMethods.Due:
                    if (SelectedItem.SupplierId.HasValue)
                        WposContext.TransactionRepository.PurchaseOnDue(entity);
                    else
                        ShowMessage.ShowMessage("Please Select A Supplier.", MessageBoxImage.Error);
                    break;
                case PaymentMethods.Bank:
                    WposContext.TransactionRepository.PurchaseOnBank(entity, BankId, ChequeNumber);
                    break;
                default:
                    ShowMessage.ShowMessage("Please Select A Payment Method.", MessageBoxImage.Error);
                    break;
            }
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("{0} Is Compete.", Title), MessageBoxImage.Ok);
            PrintCommandExecute(SelectedItem);
            ResetCommandExecute(SelectedItem);
        }

        #endregion
    }
}
