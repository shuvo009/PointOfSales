using System;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.BaseClass;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.Transction
{
    public class SalesViewModel : BasicTransctionViewModel<Customers>
    {
        private readonly ICustomerList _customerList;

        public SalesViewModel(ICustomerList customerList)
            : base("Sales")
        {
            _customerList = customerList;
            ReportType = ReportType.Sales;
            SelectedItem.IsReturn = false;
            SelectedItem.IsSales = true;
            DiscontCalculatorCommand = new RelayCommand(DiscountCalculatorCommandExecute);
            SelectedItem.InvoiceNumber = WposContext.TransactionRepository.GenerateInvoiceNumber();
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = ProductSales;
            CanExecuteFunc = x => x != null && x.TransactionLedgers.Any();
            AddToInvoiceCommandCanExecuteFunc = x => TransctionMetaData.Quantity > 0
                                                     && TransctionMetaData.SalesQuantity > 0
                                                     && TransctionMetaData.Rate > 0
                                                     && TransctionMetaData.SalesRate > 0;
        }

        #region Command
        public RelayCommand DiscontCalculatorCommand { get; private set; }
        #endregion

        #region Command Execute

        private void DiscountCalculatorCommandExecute()
        {
            IsBusy = true;
            SelectedItem.NetTotal = SelectedItem.SubTotal - SelectedItem.Discount;
            DuePaymentCommandExecute();
            IsBusy = false;
        }

        #endregion

        #region Base Calss Methods

        public override void RateCalculatorCommandExecute()
        {
            //IsBusy = true;
            //if (TransctionMetaData != null && TransctionMetaData.SalesQuantity > 0 && TransctionMetaData.Amount > 0)
            //{
            //    TransctionMetaData.SalesRate = Convert.ToDouble((TransctionMetaData.Amount / TransctionMetaData.SalesQuantity).ToString("F2"));
            //}
            //else
            //{
            //    if (TransctionMetaData != null) TransctionMetaData.SalesRate = 0;
            //}
            //IsBusy = false;
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

        public override void SelectManCommandExecute()
        {
            SelectedMan = _customerList.GetSelectedCustomer();
            SelectedItem.CustomerId = SelectedMan.Id;
        }

        public override void AddToInvoiceCommandExecute()
        {
            base.AddToInvoiceCommandExecute();
            DiscountCalculatorCommandExecute();
        }

        public override void RemoveProductFromChatCommandExecute(TransactionLedger transactionLedger)
        {
            base.RemoveProductFromChatCommandExecute(transactionLedger);
            DiscountCalculatorCommandExecute();
        }

        public override void DuePaymentCommandExecute()
        {
            IsBusy = true;
            SelectedItem.DueTotal = SelectedItem.NetTotal - SelectedItem.PaidTotal;
            IsBusy = false;
        }

        #endregion

        #region Private Methods

        private void ProductSales(Transaction entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Sales?")) return;
            var isSuccess = false;
            switch (PaymentMethod)
            {
                case PaymentMethods.Cash:
                    isSuccess = WposContext.TransactionRepository.SalesOnCash(entity);
                    break;
                case PaymentMethods.Due:
                    if (SelectedItem.CustomerId.HasValue)
                        isSuccess = WposContext.TransactionRepository.SalesOnDue(entity);
                    else
                        ShowMessage.ShowMessage("Please Select A Customer.", MessageBoxImage.Error);
                    break;
                case PaymentMethods.Bank:
                    isSuccess = WposContext.TransactionRepository.SalesOnBank(entity, BankId, ChequeNumber);
                    break;
                default:
                    ShowMessage.ShowMessage("Please Select A Payment Method.", MessageBoxImage.Error);
                    break;
            }
            if (!isSuccess)
            {
                ShowMessage.ShowMessage("Products Are Out Of Stock.", MessageBoxImage.Error);
                return;
            }
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("{0} Is Compete.", Title), MessageBoxImage.Ok);
            PrintCommandExecute(SelectedItem);
            ResetCommandExecute(SelectedItem);
        }

        #endregion
    }
}
