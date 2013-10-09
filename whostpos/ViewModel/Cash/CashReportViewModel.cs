using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Cash
{
    public class CashReportViewModel : SearchEnableViewModel<OpeningClosingBalance>
    {
        public CashReportViewModel()
            : base("Cash Report")
        {
            InitalActions();
        }

        private void InitalActions()
        {
            SearchAction = Search;
        }

        #region Private Methods

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult = new ObservableCollection<OpeningClosingBalance>(WposContext.OpeningClosingBlanceRepository.GetCashs(x => x.EntityDate >= searchMetadata.FromDate && x.EntityDate <= searchMetadata.ToDate));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult = new ObservableCollection<OpeningClosingBalance>(WposContext.OpeningClosingBlanceRepository.GetCashs(x => x.EntityDate == searchMetadata.FromDate));
                    break;
                default:
                    SearchResult = new ObservableCollection<OpeningClosingBalance>(WposContext.OpeningClosingBlanceRepository.GetCashs(x => true));
                    break;
            }
        }

        #endregion
    }
}
