using System;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel
{
    public class CompanyViewModel : BasicViewModel<CompanyInformation>
    {
        private readonly IImageBrowser _imageBrowser;
        public CompanyViewModel(IImageBrowser imageBrowser)
            : base("Company Information")
        {
            _imageBrowser = imageBrowser;
            Initial();
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = UpdateCompany;
            CanExecuteFunc = x => x != null && String.IsNullOrEmpty(x.Error);
            ClearEntityAction = x => WposContext.ClearAllProperty();
        }

        #region Command
        public RelayCommand BrowseCommand { get; private set; }
        #endregion

        #region Private Methods

        private void BrowseCommandExecute()
        {
            IsBusy = true;
            try
            {
                SelectedItem.Logo = _imageBrowser.GetFromFile();
            }
            catch (Exception ex)
            {
                LogExceptions.LogException(ex);
                ShowMessage.ShowMessage("Image Is Not Supported.", MessageBoxImage.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Initial()
        {
            BrowseCommand = new RelayCommand(BrowseCommandExecute);
            SelectedItem = WposContext.CompanyInformation.GetCopmanyInformation();
        }

        private void UpdateCompany(CompanyInformation entity)
        {
            WposContext.CompanyInformation.Update(entity);
            WposContext.Save();
            ShowMessage.ShowMessage(String.Format("{0} Is Updated.", Title), MessageBoxImage.Ok);
        }

        #endregion
    }
}
