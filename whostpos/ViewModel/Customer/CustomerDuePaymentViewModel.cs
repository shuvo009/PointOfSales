using System;
using whostpos.Core.BaseClass;
using whostpos.Entitys.Entites;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.Customer
{
    public class CustomerDuePaymentViewModel : BasicViewModel<Customers>
    {
        private CustomerAccount _customerAccount;
        private double _paidAmount;

        public CustomerDuePaymentViewModel()
            : base("Customer Due Payment")
        {
            InternalCollection = WposContext.CustomerRepository.GetAllCustomers();
            ReportType = ReportType.CustomerDuePAyment;
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = CustomerDuePayment;
            ClearEntityAction = x => WposContext.CustomerRepository.ClearChanges(x);
            SelectionChangeAction = x =>
            {
                SelectedItem = WposContext.CustomerRepository.FindById(x);
                if (SelectedItem == null) return;
                CustomerAccount = SelectedItem.CustomerAccount;
                PaidAmount = 0;
            };
            CanExecuteFunc = x => PaidAmount > 0 && x != null && !String.IsNullOrEmpty(x.Name);
        }

        #region Property

        public CustomerAccount CustomerAccount
        {
            get { return _customerAccount; }
            set
            {
                _customerAccount = value;
                RaisePropertyChanged(() => CustomerAccount);
            }
        }

        public double PaidAmount
        {
            get { return _paidAmount; }
            set
            {
                _paidAmount = value;
                RaisePropertyChanged(() => PaidAmount);
            }
        }

        #endregion

        #region Private Methods

        private void CustomerDuePayment(Customers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Paid?")) return;
            WposContext.CustomerRepository.CustomerPayment(entity.Id, PaidAmount);
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("{0} Is Successfully Paid", entity.Name), MessageBoxImage.Ok);
        }

        #endregion
    }
}