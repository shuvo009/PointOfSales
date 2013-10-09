using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Bank
{
    public class BankDepositReportViewModel : SearchEnableViewModel<BankTransactions>
    {
        public BankDepositReportViewModel()
            : base("Bank Deposit Report")
        {
            ReportType = ReportType.BankDepositReport;
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
                    SearchResult =
                        new ObservableCollection<BankTransactions>(
                            WposContext.BankRepository.GetBankTransctions(
                                x =>
                                    x.DateOfTan >= searchMetadata.FromDate && x.DateOfTan <= searchMetadata.ToDate &&
                                    x.Deposit > 0));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult =
                        new ObservableCollection<BankTransactions>(
                            WposContext.BankRepository.GetBankTransctions(
                                x => x.DateOfTan == searchMetadata.FromDate && x.Deposit > 0));
                    break;
                default:
                    SearchResult =
                        new ObservableCollection<BankTransactions>(
                            WposContext.BankRepository.GetBankTransctions(x => x.Deposit > 0));
                    break;
            }
        }

        #endregion
    }
}