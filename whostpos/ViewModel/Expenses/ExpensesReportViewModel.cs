using System.Collections.ObjectModel;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;

namespace whostpos.ViewModel.Expenses
{
    public class ExpensesReportViewModel : SearchEnableViewModel<Entitys.Entites.Expenses>
    {
        public ExpensesReportViewModel()
            : base("Expense Report")
        {
            ReportType = ReportType.ExpensesReport;
            InitilaActions();
        }

        private void InitilaActions()
        {
            SearchAction = Search;
        }

        private void Search(SearchMetadata searchMetadata)
        {
            switch (SearchOptions)
            {
                case SearchOptions.DateRange:
                    SearchResult = new ObservableCollection<Entitys.Entites.Expenses>(WposContext.ExpensesRepository.Search(x => x.EntityDate >= searchMetadata.FromDate && x.EntityDate <= searchMetadata.ToDate));
                    break;
                case SearchOptions.SingleDate:
                    SearchResult = new ObservableCollection<Entitys.Entites.Expenses>(WposContext.ExpensesRepository.Search(x => x.EntityDate == searchMetadata.FromDate));
                    break;
                default:
                    SearchResult = new ObservableCollection<Entitys.Entites.Expenses>(WposContext.ExpensesRepository.Search(x => true));
                    break;
            }
        }
    }
}
