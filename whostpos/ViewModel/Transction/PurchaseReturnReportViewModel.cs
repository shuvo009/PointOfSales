using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;

namespace whostpos.ViewModel.Transction
{
    public class PurchaseReturnReportViewModel : BaseSearchTransctionViewModel
    {
        public PurchaseReturnReportViewModel()
            : base("Purchase Return Report")
        {
            ReportType = ReportType.PurchaseReturnReport;
            InitialActions();
        }

        private void InitialActions()
        {
            SearchAction = Search;
        }

        #region Private Methods

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult = WposContext.TransactionRepository.GetTransctions(x => !x.IsSales
                                                                                         && x.IsReturn
                                                                                         && x.EntityDate >= searchMetadata.FromDate
                                                                                         && x.EntityDate <= searchMetadata.ToDate
                                                                                         && (!IsNameInclude || x.TransactionLedgers.Any(y => y.Product.ProductName.Equals(searchMetadata.Name))));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult = WposContext.TransactionRepository.GetTransctions(x => !x.IsSales
                                                                                         && x.IsReturn
                                                                                         && x.EntityDate == searchMetadata.FromDate
                                                                                         && (!IsNameInclude || x.TransactionLedgers.Any(y => y.Product.ProductName.Equals(searchMetadata.Name))));
                    break;
                default:
                    SearchResult = WposContext.TransactionRepository.GetTransctions(x => !x.IsSales
                                                                                          && x.IsReturn
                                                                                          && (!IsNameInclude || x.TransactionLedgers.Any(y => y.Product.ProductName.Equals(searchMetadata.Name))));
                    break;
            }
        }

        #endregion
    }
}
