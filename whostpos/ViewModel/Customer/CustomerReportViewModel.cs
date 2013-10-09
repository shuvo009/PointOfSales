using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.Customer
{
    public class CustomerReportViewModel : SearchEnableViewModel<Customers>
    {
        public CustomerReportViewModel() : base("Customer Report")
        {
            ReportType = ReportType.CustomerReport;
            InitialActions();
        }

        private void InitialActions()
        {
            SearchAction = Search;
        }

        #region Private Method

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult =
                        new ObservableCollection<Customers>(
                            WposContext.CustomerRepository.Search(
                                x => x.EntityDate >= searchMetadata.FromDate && x.EntityDate <= searchMetadata.ToDate));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult =
                        new ObservableCollection<Customers>(
                            WposContext.CustomerRepository.Search(x => x.EntityDate == searchMetadata.FromDate));
                    break;
                default:
                    SearchResult = new ObservableCollection<Customers>(WposContext.CustomerRepository.GetAll());
                    break;
            }
        }

        #endregion
    }
}