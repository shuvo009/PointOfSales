using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.Helpers;
using whostpos.Core.Metedata;

namespace whostpos.Core.BaseClass
{
    public abstract class SearchEnableViewModel<T> : BasicViewModel<T> where T : class, new()
    {
        private string _singleDateContent;
        private string _dateRangeContent;
        private string _allContent;
        private string _nameContent;
        private string _amountContent;
        private string _quantityContent;
        private SearchMetadata _searchMetadata;
        private SearchOptions _searchOptions;
        private ObservableCollection<T> _searchResult;
        private bool _isNameInclude;

        private string _searchContent;

        protected SearchEnableViewModel(string title)
            : base(title)
        {
            SearchCommand = new RelayCommand<SearchMetadata>(SearchCommandExecute);
            CollectionPrintCommand = new RelayCommand<object>(CollectioPrintCommandExecute, CollectionPrintCanExecute);
            SearchMetadata = new SearchMetadata();
            InitialPropertys();
        }

        private void InitialPropertys()
        {
            SingleDateContent = "Single Date";
            DateRangeContent = "Date Range";
            AllContent = "All";
            NameContent = "Name";
            AmountContent = "Amount";
            SearchContent = "Search";
            QuantityContent = "Quantity";
            SearchOptions = SearchOptions.All;
            SearchMetadata = new SearchMetadata
                {
                    FromDate =  DateTime.Today,
                    ToDate = DateTime.Today.AddMonths(1)
                };
        }

        #region Actions

        public Action<SearchMetadata> SearchAction;
        #endregion

        #region Command
        public RelayCommand<SearchMetadata> SearchCommand { get; private set; }
        public RelayCommand<object> CollectionPrintCommand { get; set; }

        #endregion

        #region Command Execute

        public virtual void SearchCommandExecute(SearchMetadata searchMetadata)
        {
            IsBusy = true;
            try
            {
                SearchAction(searchMetadata);
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

        public virtual void CollectioPrintCommandExecute(object collection)
        {
            IsBusy = true;
            try
            {
                Printer.Printer(ReportType, (((Telerik.Windows.Data.DataItemCollection)collection).Cast<T>()));
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage("Unable To Print.", MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public virtual bool CollectionPrintCanExecute(object collection)
        {
            return collection != null && (((Telerik.Windows.Data.DataItemCollection)collection).Cast<T>()).Any();
        }

        #endregion

        #region Propertys
        public string SingleDateContent
        {
            get { return _singleDateContent; }
            set
            {
                _singleDateContent = value;
                RaisePropertyChanged(() => SingleDateContent);
            }
        }

        public string DateRangeContent
        {
            get { return _dateRangeContent; }
            set
            {
                _dateRangeContent = value;
                RaisePropertyChanged(() => DateRangeContent);
            }
        }

        public string AllContent
        {
            get { return _allContent; }
            set
            {
                _allContent = value;
                RaisePropertyChanged(() => AllContent);
            }
        }

        public string NameContent
        {
            get { return _nameContent; }
            set
            {
                _nameContent = value;
                RaisePropertyChanged(() => NameContent);
            }
        }

        public string AmountContent
        {
            get { return _amountContent; }
            set
            {
                _amountContent = value;
                RaisePropertyChanged(() => AmountContent);
            }
        }

        public string QuantityContent
        {
            get { return _quantityContent; }
            set
            {
                _quantityContent = value;
                RaisePropertyChanged(() => QuantityContent);
            }
        }

        public string SearchContent
        {
            get { return _searchContent; }
            set
            {
                _searchContent = value;
                RaisePropertyChanged(() => SearchContent);
            }
        }

        public SearchMetadata SearchMetadata
        {
            get { return _searchMetadata; }
            set
            {
                _searchMetadata = value;
                RaisePropertyChanged(() => SearchMetadata);
            }
        }

        public SearchOptions SearchOptions
        {
            get { return _searchOptions; }
            set
            {
                _searchOptions = value;
                RaisePropertyChanged(() => SearchOptions);
            }
        }

        public ObservableCollection<T> SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                RaisePropertyChanged(() => SearchResult);
            }
        }

        public bool IsNameInclude
        {
            get { return _isNameInclude; }
            set
            {
                _isNameInclude = value;
                RaisePropertyChanged(() => IsNameInclude);
            }
        }

        #endregion
    }
}
