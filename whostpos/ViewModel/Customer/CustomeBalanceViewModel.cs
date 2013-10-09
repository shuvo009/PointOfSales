using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Customer
{
    public class CustomeBalanceViewModel : SearchEnableViewModel<Customers>
    {
        public CustomeBalanceViewModel()
            : base("Customer Balance")
        {
            InternalCollection = WposContext.CustomerRepository.GetAllCustomers();
            ReportType = ReportType.CustomerBalance;
            InitialActions();
        }

        private void InitialActions()
        {
            SearchAction = Search;
            SelectionChangeAction = x =>
            {
                SelectedItem = WposContext.CustomerRepository.FindById(x);
                if (SelectedItem == null) return;
                SearchMetadata.Name = SelectedItem.Name;
            };

            ClearEntityAction = x => WposContext.CustomerRepository.ClearChanges(SelectedItem);
        }

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.Name:
                    SearchResult = new ObservableCollection<Customers>(WposContext.CustomerRepository.Search(x => x.Name.Equals(searchMetadata.Name)));
                    break;
                case SearchOptions.Amount:
                    SearchResult = new ObservableCollection<Customers>(WposContext.CustomerRepository.Search(x => x.CustomerAccount.Credit >= searchMetadata.Amount || x.CustomerAccount.Debit >= searchMetadata.Amount));
                    break;
                default:
                    SearchResult = new ObservableCollection<Customers>(WposContext.CustomerRepository.GetAll());
                    break;
            }
        }
    }
}
