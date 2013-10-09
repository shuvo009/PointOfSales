using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Supplier
{
    public class SupplierLedgerViewModel : SearchEnableViewModel<SuppliersLedger>
    {
        private BasicViewModel<Suppliers> _supplierViewModel;

        public SupplierLedgerViewModel()
            : base("Supplier Ledger")
        {
            SupplierViewModel = new SupplierViewModel();
            SupplierViewModel.InternalCollection = SupplierViewModel.WposContext.SupplierRepository.GetAllSuppliers();
            ReportType = ReportType.SupplierLedger;
            InitialAction();
        }

        private void InitialAction()
        {
            SelectionChangeAction = x =>
            {
                SupplierViewModel.SelectedItem = WposContext.SupplierRepository.FindById(x);
                if (SupplierViewModel.SelectedItem == null) return;
                SearchMetadata.Name = SupplierViewModel.SelectedItem.Name;
            };
            SearchAction = Search;
            ClearEntityAction = x => WposContext.SupplierRepository.ClearChanges(SupplierViewModel.SelectedItem);
        }

        #region Propertys

        public BasicViewModel<Suppliers> SupplierViewModel
        {
            get { return _supplierViewModel; }
            set
            {
                _supplierViewModel = value;
                RaisePropertyChanged(() => SupplierViewModel);
            }
        }

        #endregion

        #region Private Methods

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult = new ObservableCollection<SuppliersLedger>(WposContext.SupplierRepository.SearchInLedger(x => x.EntityDate >= searchMetadata.FromDate && x.EntityDate <= searchMetadata.ToDate && (IsNameInclude ? x.Suppliers.Name.Equals(searchMetadata.Name) : true)));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult = new ObservableCollection<SuppliersLedger>(WposContext.SupplierRepository.SearchInLedger(x => x.EntityDate == searchMetadata.FromDate && (IsNameInclude ? x.Suppliers.Name.Equals(searchMetadata.Name) : true)));
                    break;
                default:
                    SearchResult = new ObservableCollection<SuppliersLedger>(WposContext.SupplierRepository.SearchInLedger(x => IsNameInclude ? x.Suppliers.Name.Equals(searchMetadata.Name) : true));
                    break;
            }
        } 
        #endregion
    }
}
