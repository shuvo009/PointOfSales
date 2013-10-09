using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Classes;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.ViewModel.Products;

namespace whostpos.ViewModel.Stock
{
    public class StockViewModel : SearchEnableViewModel<whostpos.Entitys.Entites.Products>
    {
        private ProductSetupViewModel _productSetupViewModel;

        public StockViewModel()
            : base("Stock")
        {
            ProductSetupViewModel = new ProductSetupViewModel(new ImageBrowser());
            ReportType = ReportType.Stock;
            InitialActions();
        }

        private void InitialActions()
        {
            SearchAction = Search;
            ClearEntityAction = x => WposContext.ProductRepository.ClearChanges(ProductSetupViewModel.SelectedItem);
            SelectionChangeAction = x =>
            {
                ProductSetupViewModel.SelectedItem = WposContext.ProductRepository.FindById(x);
                if (ProductSetupViewModel.SelectedItem == null) return;
                SearchMetadata.Name = ProductSetupViewModel.SelectedItem.ProductName;
            };
        }

        #region Propertys
        public ProductSetupViewModel ProductSetupViewModel
        {
            get { return _productSetupViewModel; }
            set
            {
                _productSetupViewModel = value;
                RaisePropertyChanged(() => ProductSetupViewModel);
            }
        }
        #endregion

        #region Private Methods

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.Name:
                    SearchResult = new ObservableCollection<Entitys.Entites.Products>(WposContext.ProductRepository.Search(x => x.ProductName.Equals(searchMetadata.Name)));
                    break;
                case SearchOptions.Amount:
                    SearchResult = new ObservableCollection<Entitys.Entites.Products>(WposContext.ProductRepository.Search(x => x.Stock.Quantity <= searchMetadata.Quantity));
                    break;
                default:
                    SearchResult = new ObservableCollection<Entitys.Entites.Products>(WposContext.ProductRepository.GetAll());
                    break;
            }
        }

        #endregion
    }
}
