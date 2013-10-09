using System;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Customer
{
    public class CustomerViewModel : BasicViewModel<Customers>
    {
        private readonly IImageBrowser _imageBrowser;

        public CustomerViewModel(IImageBrowser imageBrowser)
            : base("Customer")
        {

            ReportType = ReportType.Customer;
            _imageBrowser = imageBrowser;
            InternalCollection = WposContext.CustomerRepository.GetAllCustomers();
            Initial();
            InitialAction();
        }

        private void InitialAction()
        {
            SaveAction = x =>
            {
                if (x.Id > 0)
                    UpdateCustomer(x);
                else
                    CreateCustomer(x);
            };

            DeleteAction = DeleteCustomer;
            SelectionChangeAction = x => SelectedItem = WposContext.CustomerRepository.FindById(x);
            ClearEntityAction = x => WposContext.CustomerRepository.ClearChanges(x);
            RemoveAddedEntityAction = x => WposContext.CustomerRepository.RemoveAddedEntity(x);
            CanExecuteFunc = x => x != null && string.IsNullOrEmpty(x.Error);
        }

        #region Command
        public RelayCommand BrowseCommand { get; private set; }
        public RelayCommand WebcamCommand { get; private set; }
        #endregion

        #region Private Methods

        private void BrowseCommandExecute()
        {
            IsBusy = true;
            try
            {
                SelectedItem.Photo = _imageBrowser.GetFromFile();
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

        private void WebcamCommandExecute()
        {
            IsBusy = true;
            try
            {
                var image = _imageBrowser.GetFromWebCam();
                if (image != null)
                    SelectedItem.Photo = image;
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
            WebcamCommand = new RelayCommand(WebcamCommandExecute);
        }

        private void CreateCustomer(Customers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Save?")) return;
            WposContext.CustomerRepository.Add(entity);
            WposContext.Save();
            InternalCollection.Add(new Customers
            {
                Id = entity.Id,
                Name = entity.Name,
                Mobile = entity.Mobile,
                Photo = entity.Photo
            });
            NewCommandExecute();
            ShowMessage.ShowMessage(String.Format("{0} Is Saved.", Title), MessageBoxImage.Ok);
        }
        private void UpdateCustomer(Customers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Update?")) return;
            WposContext.CustomerRepository.Update(entity);
            WposContext.Save();
            var updatedProduct = InternalCollection.Single(x => x.Id == entity.Id);
            updatedProduct.Name = entity.Name;
            updatedProduct.Mobile = entity.Mobile;
            updatedProduct.Photo = entity.Photo;
            ShowMessage.ShowMessage(String.Format("{0} Is Updated.", Title), MessageBoxImage.Ok);
        }
        private void DeleteCustomer(Customers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Delete?")) return;
            WposContext.CustomerRepository.Remove(entity);
            WposContext.Save();
            InternalCollection.Remove(InternalCollection.Single(x => x.Id == entity.Id));
            ShowMessage.ShowMessage(String.Format("{0} Is Deleted.", Title), MessageBoxImage.Ok);
        }
        #endregion
    }
}
