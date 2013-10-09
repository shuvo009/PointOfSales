using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Supplier
{
    public class SupplierReportViewModel : SearchEnableViewModel<Suppliers>
    {
        public SupplierReportViewModel() : base("Supplier Report")
        {
            ReportType = ReportType.SupplierReport;
            InitialAction();
        }

        private void InitialAction()
        {
            SearchAction = Search;
        }

        #region Private Methods

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult = new ObservableCollection<Suppliers>(WposContext.SupplierRepository.Search(x => x.EntityDate >= searchMetadata.FromDate && x.EntityDate <= searchMetadata.ToDate));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult = new ObservableCollection<Suppliers>(WposContext.SupplierRepository.Search(x => x.EntityDate == searchMetadata.FromDate));
                    break;
                default:
                    SearchResult = new ObservableCollection<Suppliers>(WposContext.SupplierRepository.GetAll());
                    break;
            }
        }

        #endregion
    }
}
