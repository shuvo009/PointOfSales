using System;
using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;

namespace whostpos.ViewModel.Transction
{
    public class SalesReturnReportViewModel : BaseSearchTransctionViewModel
    {
        public SalesReturnReportViewModel()
            : base("Sales Return Report")
        {
            ReportType = ReportType.SalesReturnReport;
        }

        public override void SearchCommandExecute(SearchMetadata searchMetadata)
        {
            IsBusy = true;
            try
            {
                switch (SearchOptions)
                {
                    case SearchOptions.DateRange:
                        SearchResult = WposContext.TransactionRepository.GetTransctions(x => x.IsSales
                                                                                             && x.IsReturn
                                                                                             && x.EntityDate >= searchMetadata.FromDate
                                                                                             && x.EntityDate <= searchMetadata.ToDate
                                                                                             && (!IsNameInclude || x.TransactionLedgers.Any(y => y.Product.ProductName.Equals(searchMetadata.Name))));
                        break;
                    case SearchOptions.SingleDate:
                        SearchResult = WposContext.TransactionRepository.GetTransctions(x => x.IsSales
                                                                                             && x.IsReturn
                                                                                             && x.EntityDate == searchMetadata.FromDate
                                                                                             && (!IsNameInclude || x.TransactionLedgers.Any(y => y.Product.ProductName.Equals(searchMetadata.Name))));
                        break;
                    default:
                        SearchResult = WposContext.TransactionRepository.GetTransctions(x => x.IsSales
                                                                                              && x.IsReturn
                                                                                              && (!IsNameInclude || x.TransactionLedgers.Any(y => y.Product.ProductName.Equals(searchMetadata.Name))));
                        break;
                }
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage(String.Format("{0} Is unable To Search.", Title), MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
