using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Supplier
{
    public class SupplierBalanceViewModel : SearchEnableViewModel<Suppliers>
    {
        public SupplierBalanceViewModel()
            : base("Supplier Balance")
        {
            InternalCollection = WposContext.SupplierRepository.GetAllSuppliers();
            ReportType = ReportType.SupplierBalance;
            InitialActions();
        }

        private void InitialActions()
        {
            SearchAction = Search;
            ClearEntityAction = x => WposContext.SupplierRepository.ClearChanges(SelectedItem);
            SelectionChangeAction = x =>
            {
                SelectedItem = WposContext.SupplierRepository.FindById(x);
                if (SelectedItem == null) return;
                SearchMetadata.Name = SelectedItem.Name;
            };
        }

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.Name:
                    SearchResult = new ObservableCollection<Suppliers>(WposContext.SupplierRepository.Search(x => x.Name.Equals(searchMetadata.Name)));
                    break;
                case SearchOptions.Amount:
                    SearchResult = new ObservableCollection<Suppliers>(WposContext.SupplierRepository.Search(x => x.SupplierAccount.Credit >= searchMetadata.Amount || x.SupplierAccount.Debit >= searchMetadata.Amount));
                    break;
                default:
                    SearchResult = new ObservableCollection<Suppliers>(WposContext.SupplierRepository.GetAll());
                    break;
            }
        }
    }
}
