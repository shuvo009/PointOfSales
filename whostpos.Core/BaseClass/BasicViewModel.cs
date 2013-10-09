using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Windows.Data;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.Classes;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;

namespace whostpos.Core.BaseClass
{
    public abstract class BasicViewModel<T> : BaseViewModel where T : class, new()
    {
        public readonly IPrinter Printer;
        private ObservableCollection<T> _internalCollection;
        private string _searchText;
        private T _selectedItem;


        #region Actions
        public Action<T> SaveAction;
        public Action<T> RemoveAddedEntityAction;
        public Action<T> DeleteAction;
        public Action<T> ClearEntityAction;
        public Func<T, bool> CanExecuteFunc;
        public Action<Int64> SelectionChangeAction;
        #endregion

        protected BasicViewModel(string title)
            : base(title)
        {
            Printer = new Print();
            CreateNew();
            InitialCommands();
            CollectionView = new CollectionViewSource();
            CollectionView.Filter += ApplyFilter;
            SelectedItem = new T();
        }

        public CollectionViewSource CollectionView { get; set; }

        private void InitialCommands()
        {
            SaveCommand = new RelayCommand<T>(SaveCommandExecute, CommandCanExecute);
            UpdateCommand = new RelayCommand<T>(UpdateCommandExecute, CommandCanExecute);
            DeleteCommand = new RelayCommand<T>(DeleteCommandExecute, CommandCanExecute);
            ResetCommand = new RelayCommand<T>(ResetCommandExecute);
            PrintCommand = new RelayCommand<T>(PrintCommandExecute, PrintCommandCanExecute);
            NewCommand = new RelayCommand(NewCommandExecute);
            SelectionChangedCommand = new RelayCommand<Int64>(SelectionChangedExecute);
        }

        public void EntityValidation(DbEntityValidationException dbEx)
        {
            var errorMessBuilder = new StringBuilder();
            foreach (
                var validationError in
                    dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
            {
                errorMessBuilder.AppendLine(validationError.ErrorMessage);
            }
            LogExceptions.LogException(new Exception(errorMessBuilder.ToString()));
            ShowMessage.ShowMessage(errorMessBuilder.ToString(), MessageBoxImage.Error);
        }

        #region Private Methods

        private void CreateNew()
        {
            SelectedItem = new T();
            typeof(T).GetProperties()
                      .Where(x => typeof(DateTime).IsAssignableFrom(x.PropertyType))
                      .ToList()
                      .ForEach(pro => pro.SetValue(SelectedItem, DateTime.Today, null));
        }

        private void OnFilterChanged()
        {
            CollectionView.View.Refresh();
        }

        private void ApplyFilter(object sender, FilterEventArgs e)
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
                                                               && x.GetValue(e.Item, null)
                                                                .ToString()
                                                                .ToLower()
                                                                .Contains(filterText)) != null;
            }
        }

        #endregion

        #region Property

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnFilterChanged();
            }
        }

        public ObservableCollection<T> InternalCollection
        {
            get { return _internalCollection; }
            set
            {
                _internalCollection = value;
                CollectionView.Source = InternalCollection;
            }
        }

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public ICollectionView ItemCollection
        {
            get { return CollectionView.View; }
        }

        #endregion

        #region Command

        public RelayCommand<T> SaveCommand { get; private set; }
        public RelayCommand<T> DeleteCommand { get; private set; }
        public RelayCommand<T> UpdateCommand { get; private set; }
        public RelayCommand<T> ResetCommand { get; private set; }
        public RelayCommand<T> PrintCommand { get; private set; }
        public RelayCommand<Int64> SelectionChangedCommand { get; private set; }
        public RelayCommand NewCommand { get; private set; }

        #endregion

        #region Command Execute

        public virtual void NewCommandExecute()
        {
            CreateNew();
        }

        public virtual void ResetCommandExecute(T entity)
        {
            entity.ClearAllProperty();
        }

        public virtual void SaveCommandExecute(T entity)
        {
            IsBusy = true;
            try
            {
                SaveAction(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                if (RemoveAddedEntityAction != null)
                    RemoveAddedEntityAction(entity);
                EntityValidation(dbEx);
            }
            catch (Exception ex)
            {
                if (RemoveAddedEntityAction != null)
                    RemoveAddedEntityAction(entity);
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage(String.Format("{0} Is Not Saved.", Title), MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public virtual void UpdateCommandExecute(T entity)
        {
            // Remove Soon
        }

        public virtual void DeleteCommandExecute(T entity)
        {
            IsBusy = true;
            try
            {
                DeleteAction(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                if (ClearEntityAction != null)
                    ClearEntityAction(entity);
                EntityValidation(dbEx);
            }
            catch (Exception ex)
            {
                if (ClearEntityAction != null)
                    ClearEntityAction(entity);
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage(String.Format("{0} Is Not Deleted.", Title), MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public virtual bool CommandCanExecute(T entity)
        {
            return CanExecuteFunc(entity);
        }

        public virtual void PrintCommandExecute(T entity)
        {
            IsBusy = true;
            try
            {
                Printer.Printer(ReportType, entity);
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

        public virtual bool PrintCommandCanExecute(T entity)
        {
            return SelectedItem != null;
        }

        public virtual void SelectionChangedExecute(Int64 id)
        {
            IsBusy = true;
            try
            {
                if (ClearEntityAction != null)
                    ClearEntityAction(SelectedItem);
                SelectionChangeAction(id);
            }
            catch (DbEntityValidationException dbEx)
            {
                if (ClearEntityAction != null)
                    ClearEntityAction(SelectedItem);
                EntityValidation(dbEx);
            }
            catch (Exception ex)
            {
                if (ClearEntityAction != null)
                    ClearEntityAction(SelectedItem);
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage(String.Format("{0} Is unable To Load.", Title), MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}