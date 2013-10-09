using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace whostpos.Windows.BaseClass
{
    public abstract class BaseViewModel<T> : ViewModelBase where T : class, new()
    {
        private string _title;
        private ObservableCollection<T> _internalCollection;
        private string _busyConyent;
        private bool _isBusy;
        public CollectionViewSource CollectionView { get; set; }
        private string _searchText;

        public delegate void WindowCloseing();
        public event WindowCloseing CloseWindow;

        protected BaseViewModel(string title)
        {
            BusyConyent = "Loading....";
            CollectionView = new CollectionViewSource();
            CollectionView.Filter += ApplyFilter;
            Title = title;
            DoubleClickCommand = new RelayCommand<T>(DoubleClickCommandExecute, DoubleClickCommandCanExecute);
        }

        #region Commands
        public RelayCommand<T> DoubleClickCommand { get; private set; }
        #endregion

        #region Execute

        private void DoubleClickCommandExecute(T entity)
        {
            try
            {
                Messenger.Default.Send(entity);
            }
            catch (Exception)
            {
                
            }
            if (CloseWindow != null)
            {
                CloseWindow();
            }
        }

        private bool DoubleClickCommandCanExecute(T entity)
        {
            return entity != null;
        }

        #endregion

        #region Propertys
        public ObservableCollection<T> InternalCollection
        {
            get { return _internalCollection; }
            set
            {
                _internalCollection = value;
                CollectionView.Source = InternalCollection;
            }
        }

        public ICollectionView ItemCollection
        {
            get { return CollectionView.View; }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string BusyConyent
        {
            get { return _busyConyent; }
            set
            {
                _busyConyent = value;
                RaisePropertyChanged(() => BusyConyent);
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnFilterChanged();
            }
        }
        #endregion

        #region Private Methods
        private void OnFilterChanged()
        {
            CollectionView.View.Refresh();
        }

        void ApplyFilter(object sender, FilterEventArgs e)
        {
            var propertyInfos = typeof(T).GetProperties();

            if (string.IsNullOrWhiteSpace(SearchText) || SearchText.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                var filterText = SearchText.ToLower();
                e.Accepted = propertyInfos.FirstOrDefault(x => x.GetSetMethod() != null
                                                 && x.GetSetMethod() != null
                                                 && !typeof(byte[]).IsAssignableFrom(x.PropertyType)
                                                 && x.GetValue(e.Item, null) != null
                                                 && x.GetValue(e.Item, null).ToString().ToLower().Contains(filterText)) != null;
            }
        }
        #endregion
    }
}
