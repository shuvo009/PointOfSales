using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;

namespace whostpos.ViewModel.Database
{
    public class DatabaseRestoreBackupViewModel : BaseViewModel
    {
        private const string CatalogName = "WhostPointOfSalesDatabase";
        private string _backupLocation;
        private bool _isEnable = true;
        private bool _isIndeterminate;
        private string _restoreLocation;

        public DatabaseRestoreBackupViewModel()
            : base("Database Backup and Restore")
        {
            BackupButtonContext = "Backup";
            RestoreButtonContext = "Restore";
            BrowseButtonContext = "Browse";
            CancelButtonContext = "Cancel";
            BackupCommand = new RelayCommand<string>(BackupCommnadExecute, BackupCommandCanExecute);
            RestoreCommand = new RelayCommand<string>(RestoreCommnadExecute, RestoreCommandCanExecute);
            BackupBrowserCommand = new RelayCommand<string>(BackupBrowserExecute);
            RestoreBrowseCommand = new RelayCommand<string>(RestoreBrowseExecute);
        }

        public string BackupLocation
        {
            get { return _backupLocation; }
            set
            {
                _backupLocation = value;
                RaisePropertyChanged(() => BackupLocation);
            }
        }

        public string RestoreLocation
        {
            get { return _restoreLocation; }
            set
            {
                _restoreLocation = value;
                RaisePropertyChanged(() => RestoreLocation);
            }
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                _isEnable = value;
                RaisePropertyChanged(() => IsEnable);
            }
        }

        public bool IsIndeterminate
        {
            get { return _isIndeterminate; }
            set
            {
                _isIndeterminate = value;
                RaisePropertyChanged(() => IsIndeterminate);
            }
        }


        public string BackupButtonContext { get; private set; }
        public string RestoreButtonContext { get; private set; }
        public string BrowseButtonContext { get; private set; }
        public string CancelButtonContext { get; private set; }

        public RelayCommand<string> BackupCommand { get; private set; }
        public RelayCommand<string> RestoreCommand { get; private set; }
        public RelayCommand<string> BackupBrowserCommand { get; private set; }
        public RelayCommand<string> RestoreBrowseCommand { get; private set; }


        private void BackupBrowserExecute(string path)
        {
            IsBusy = true;
            try
            {
                var backupPathDialog = new FolderBrowserDialog
                {
                    Description = @"Please Select a folder for backup file",
                    ShowNewFolderButton = true
                };
                if (backupPathDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    BackupLocation = backupPathDialog.SelectedPath;
                }
            }
            catch (Exception exception)
            {
                LogExceptions.LogException(exception);
                ShowMessage.ShowMessage("Unable to get location", MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void BackupCommnadExecute(string path)
        {
            IsBusy = true;

            IsIndeterminate = true;
            IsEnable = false;
            DatabaseBackup(path);
            IsEnable = true;
            IsIndeterminate = false;
            ShowMessage.ShowMessage("Database Backup Successfully.", MessageBoxImage.Ok);
            IsBusy = false;
        }

        private bool BackupCommandCanExecute(string path)
        {
            try
            {
                return !String.IsNullOrEmpty(path);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void RestoreBrowseExecute(string path)
        {
            IsBusy = true;
            try
            {
                var restorePathDialog = new OpenFileDialog
                {
                    InitialDirectory =
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Filter = @"SQL Backup file (*.bak)|*.bak"
                };
                if (restorePathDialog.ShowDialog().Equals(DialogResult.OK))
                {
                    RestoreLocation = restorePathDialog.FileName;
                }
            }
            catch (Exception exception)
            {
                LogExceptions.LogException(exception);
                ShowMessage.ShowMessage("Unable to get location", MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void RestoreCommnadExecute(string path)
        {
            IsBusy = true;
            IsIndeterminate = true;
            IsEnable = false;
            DatabaseRestore(path);
            IsEnable = true;
            IsIndeterminate = false;
            ShowMessage.ShowMessage("Database Restore Successfully", MessageBoxImage.Ok);
            IsBusy = false;
        }

        private bool RestoreCommandCanExecute(string path)
        {
            try
            {
                return !String.IsNullOrEmpty(path);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper

        private string DatabaseBackup(string path = null)
        {
            try
            {
                if (String.IsNullOrEmpty(path))
                {
                    path = GetTempFolder();
                }

                using (
                    var backupConnection =
                        new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True"))
                {
                    backupConnection.Open();
                    var useMasterCommand = new SqlCommand("USE master", backupConnection);
                    useMasterCommand.ExecuteNonQuery();
                    string backupPath = Path.Combine(path, GetBackupFileName());
                    string alter1 = string.Format(@"BACKUP DATABASE {0} TO DISK = '{1}'",
                        CatalogName, backupPath);
                    var alter1Cmd = new SqlCommand(alter1, backupConnection);
                    alter1Cmd.ExecuteNonQuery();
                    alter1Cmd.Dispose();
                    return backupPath;
                }
            }
            catch (Exception exception)
            {
                LogExceptions.LogException(exception);
                ShowMessage.ShowMessage("Unable to database backup.", MessageBoxImage.Error);
                return null;
            }
        }

        private void DatabaseRestore(string path)
        {
            try
            {
                using (
                    var restoreConnection =
                        new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True"))
                {
                    restoreConnection.Open();
                    var useMasterCommand = new SqlCommand("USE master", restoreConnection);
                    useMasterCommand.ExecuteNonQuery();
                    string alter1 = string.Format(@"ALTER DATABASE [{0}] SET Single_User WITH Rollback Immediate",
                        CatalogName);
                    var alter1Cmd = new SqlCommand(alter1, restoreConnection);
                    alter1Cmd.ExecuteNonQuery();
                    alter1Cmd.Dispose();
                    string restore = string.Format(@"RESTORE DATABASE [{0}] FROM DISK = N'{1}' WITH REPLACE",
                        CatalogName, path);
                    var restoreCmd = new SqlCommand(restore, restoreConnection);
                    restoreCmd.ExecuteNonQuery();
                    restoreCmd.Dispose();
                    string alter2 = string.Format(@"ALTER DATABASE [{0}] SET Multi_User", CatalogName);
                    var alter2Cmd = new SqlCommand(alter2, restoreConnection);
                    alter2Cmd.ExecuteNonQuery();
                    alter2Cmd.Dispose();
                }
            }
            catch (Exception exception)
            {
                LogExceptions.LogException(exception);
                ShowMessage.ShowMessage("Unable to restore Database", MessageBoxImage.Error);
                try
                {
                    using (
                        var restoreConnection =
                            new SqlConnection(
                                @"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True"))
                    {
                        restoreConnection.Open();
                        string alter2 = string.Format(@"ALTER DATABASE [{0}] SET Multi_User",
                            CatalogName);
                        var alter2Cmd = new SqlCommand(alter2, restoreConnection);
                        alter2Cmd.ExecuteNonQuery();
                        alter2Cmd.Dispose();
                    }
                }
                catch (Exception exc)
                {
                    LogExceptions.LogException(exc);
                    ShowMessage.ShowMessage("Serious error ! Contract admin.\n" + exc.Message, MessageBoxImage.Error);
                }
            }
        }

        private string GetBackupFileName()
        {
            return string.Format("{0}.bak", DateTime.Now.ToString("ddMMyyyyhhsstt"));
        }

        private string GetTempFolder()
        {
            string tempFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            tempFolder = Path.Combine(tempFolder, "RhGDatabaseBackupRestore");
            if (!Directory.Exists(tempFolder))
                Directory.CreateDirectory(tempFolder);
            return tempFolder;
        }

        #endregion
    }
}