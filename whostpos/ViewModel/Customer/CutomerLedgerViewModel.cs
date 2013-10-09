using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Classes;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Customer
{
    public class CutomerLedgerViewModel : SearchEnableViewModel<CustomerLedger>
    {
        private BasicViewModel<Customers> _customerViewModel;

        public CutomerLedgerViewModel()
            : base("Customer Ledger")
        {
            CustomerViewModel = new CustomerViewModel(new ImageBrowser());
            ReportType = ReportType.CustomerLedger;
            InitialActions();
        }

        private void InitialActions()
        {
            SearchAction = Search;
            SelectionChangeAction = x =>
            {
                CustomerViewModel.SelectedItem = WposContext.CustomerRepository.FindById(x);
                if (CustomerViewModel.SelectedItem == null) return;
                SearchMetadata.Name = CustomerViewModel.SelectedItem.Name;
            };

            ClearEntityAction = x => WposContext.CustomerRepository.ClearChanges(CustomerViewModel.SelectedItem);
        }

        #region Propertys

        public BasicViewModel<Customers> CustomerViewModel
        {
            get { return _customerViewModel; }
            set
            {
                _customerViewModel = value;
                RaisePropertyChanged(() => CustomerViewModel);
            }
        }

        #endregion

        #region Private Methods

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult =
                        new ObservableCollection<CustomerLedger>(
                            WposContext.CustomerRepository.SearchInLedger(
                                x =>
                                    x.EntityDate >= searchMetadata.FromDate && x.EntityDate <= searchMetadata.ToDate &&
                                    (IsNameInclude ? x.Customers.Name.Equals(searchMetadata.Name) : true)));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult =
                        new ObservableCollection<CustomerLedger>(
                            WposContext.CustomerRepository.SearchInLedger(
                                x =>
                                    x.EntityDate == searchMetadata.FromDate &&
                                    (IsNameInclude ? x.Customers.Name.Equals(searchMetadata.Name) : true)));
                    break;
                default:
                    SearchResult =
                        new ObservableCollection<CustomerLedger>(
                            WposContext.CustomerRepository.SearchInLedger(
                                x => IsNameInclude ? x.Customers.Name.Equals(searchMetadata.Name) : true));
                    break;
            }
        }

        #endregion
    }
}