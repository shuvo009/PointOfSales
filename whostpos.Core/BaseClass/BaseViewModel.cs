using GalaSoft.MvvmLight;
using whostpos.Core.Classes;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;

namespace whostpos.Core.BaseClass
{
    public class BaseViewModel : ViewModelBase
    {
        public readonly ILogExceptions LogExceptions;
        public readonly IShowMessage ShowMessage;
        public readonly IWposContext WposContext;
        private string _applicationName;
        private string _busyConyent;
        private string _deleteContent;
        private bool _isBusy;
        private string _newContent;
        private string _printContent;

        private ReportType _reportType;
        private string _resetContent;
        private string _saveContent;
        private string _title;
        private string _updateContent;


        public BaseViewModel(string title)
        {
            InitialVariables();
            Title = title;
            ShowMessage = new DxShowMessage();
            WposContext = new WposContext();
            LogExceptions = new NlogException();
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

        public string ApplicationName
        {
            get { return _applicationName; }
            set
            {
                _applicationName = value;
                RaisePropertyChanged(() => ApplicationName);
            }
        }

        public string SaveContent
        {
            get { return _saveContent; }
            set
            {
                _saveContent = value;
                RaisePropertyChanged(() => SaveContent);
            }
        }

        public string UpdateContent
        {
            get { return _updateContent; }
            set
            {
                _updateContent = value;
                RaisePropertyChanged(() => UpdateContent);
            }
        }

        public string DeleteContent
        {
            get { return _deleteContent; }
            set
            {
                _deleteContent = value;
                RaisePropertyChanged(() => DeleteContent);
            }
        }

        public string NewContent
        {
            get { return _newContent; }
            set
            {
                _newContent = value;
                RaisePropertyChanged(() => NewContent);
            }
        }

        public string PrintContent
        {
            get { return _printContent; }
            set
            {
                _printContent = value;
                RaisePropertyChanged(() => PrintContent);
            }
        }

        public string ResetContent
        {
            get { return _resetContent; }
            set
            {
                _resetContent = value;
                RaisePropertyChanged(() => ResetContent);
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

        public ReportType ReportType
        {
            get { return _reportType; }
            set
            {
                _reportType = value;
                RaisePropertyChanged(() => ReportType);
            }
        }

        private void InitialVariables()
        {
            SaveContent = "Save";
            UpdateContent = "Update";
            DeleteContent = "Delete";
            PrintContent = "Print";
            ResetContent = "Reset";
            NewContent = "New";
            BusyConyent = "Please Wait...";
            IsBusy = false;

            ApplicationName = "Power Of Sales [www.whostpos.com]";
        }
    }
}