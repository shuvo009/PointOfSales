using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;

namespace whostpos.ViewModel.Products
{
    public class ProductReportViewModel : SearchEnableViewModel<Entitys.Entites.Products>
    {
        public ProductReportViewModel() : base("Product Report")
        {
            ReportType = ReportType.ProductPreport;
            InitialAction();
        }

        private void InitialAction()
        {
            SearchAction = Search;
        }

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult = new ObservableCollection<Entitys.Entites.Products>(WposContext.ProductRepository.Search(x => x.EntityDate >= searchMetadata.FromDate && x.EntityDate <= searchMetadata.ToDate));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult = new ObservableCollection<Entitys.Entites.Products>(WposContext.ProductRepository.Search(x => x.EntityDate == searchMetadata.FromDate));
                    break;
                default:
                    SearchResult = new ObservableCollection<Entitys.Entites.Products>(WposContext.ProductRepository.GetAll());
                    break;
            }
        }
    }
}
