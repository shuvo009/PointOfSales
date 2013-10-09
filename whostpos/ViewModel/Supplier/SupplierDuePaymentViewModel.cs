using System;
using whostpos.Core.BaseClass;
using whostpos.Entitys.Entites;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.Supplier
{
    public class SupplierDuePaymentViewModel : BasicViewModel<Suppliers>
    {
        private SupplierAccount _supplierAccount;
        private double _paidAmount;

        public SupplierDuePaymentViewModel()
            : base("Supplier payment")
        {
            InternalCollection = WposContext.SupplierRepository.GetAllSuppliers();
            ReportType = ReportType.SupplierDuePayment;
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = SupplierPayment;
            ClearEntityAction = x => WposContext.SupplierRepository.ClearChanges(x);
            SelectionChangeAction = x =>
            {
                SelectedItem = WposContext.SupplierRepository.FindById(x);
                if (SelectedItem == null) return;
                SupplierAccount = SelectedItem.SupplierAccount;
                PaidAmount = 0;
            };

            CanExecuteFunc = x => PaidAmount > 0 && x != null && !String.IsNullOrEmpty(x.Name);
        }

        #region Property

        public SupplierAccount SupplierAccount
        {
            get { return _supplierAccount; }
            set
            {
                _supplierAccount = value;
                RaisePropertyChanged(() => SupplierAccount);
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

        private void SupplierPayment(Suppliers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Paid?")) return;
            WposContext.SupplierRepository.SupplierPayment(entity.Id, PaidAmount);
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("{0} Is Successfully Paid", entity.Name), MessageBoxImage.Ok);
        }

        #endregion
    }
}
