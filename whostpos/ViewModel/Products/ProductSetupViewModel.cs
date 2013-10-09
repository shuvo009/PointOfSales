using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DevExpress.Data.Utils;
using DevExpress.Xpf.Bars;
using GalaSoft.MvvmLight.Command;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Core.Interface;

namespace whostpos.ViewModel.Products
{
    public class ProductSetupViewModel : BasicViewModel<whostpos.Entitys.Entites.Products>
    {
        private readonly IImageBrowser _imageBrowser;
        private ObservableCollection<string> _models;
        private ObservableCollection<string> _colors;

        public ProductSetupViewModel(IImageBrowser imageBrowser)
            : base("Product")
        {
            _imageBrowser = imageBrowser;
            Initial();
            ReportType = ReportType.Product;
            LoadData();
            InternalCollection = WposContext.ProductRepository.GetAllProduucts();
            InitialActions();
        }

        private void InitialActions()
        {
            SaveAction = x =>
            {
                if (x.Id > 0)
                    UpdateProduct(x);
                else
                    SaveProduct(x);
                LoadData();
            };

            DeleteAction = DelectProduct;

            RemoveAddedEntityAction = x => WposContext.ProductRepository.RemoveAddedEntity(x);
            ClearEntityAction = x => WposContext.ProductRepository.ClearChanges(x);
            CanExecuteFunc = x => x != null && string.IsNullOrEmpty(x.Error);
            SelectionChangeAction = x => SelectedItem = WposContext.ProductRepository.FindById(x);
        }

        private void LoadData()
        {
            Models = WposContext.ProductRepository.GetModel();
            Colors = WposContext.ProductRepository.GetColor();
            GenerateProductCode();
        }

        #region Property

        public ObservableCollection<string> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                RaisePropertyChanged(() => Models);
            }
        }

        public ObservableCollection<string> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                RaisePropertyChanged(() => Colors);
            }
        }

        #endregion

        #region Command
        public RelayCommand BrowseCommand { get; private set; }
        public RelayCommand WebcamCommand { get; private set; }
        public RelayCommand<string> BarcodePrintCommand { get; private set; }
        #endregion

        #region Base Command Override

        public override void NewCommandExecute()
        {
            base.NewCommandExecute();
            GenerateProductCode();
        }

        public override void ResetCommandExecute(whostpos.Entitys.Entites.Products entity)
        {
            var productCode = entity.Code;
            var barcode = entity.BardCode;
            base.ResetCommandExecute(entity);
            entity.Code = productCode;
            entity.BardCode = barcode;
        }

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
            BarcodePrintCommand = new RelayCommand<string>(BarcodePrintCommandExecute);
        }

        private void SaveProduct(whostpos.Entitys.Entites.Products entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Save?")) return;
            WposContext.ProductRepository.Add(entity);
            WposContext.Save();
            InternalCollection.Add(new whostpos.Entitys.Entites.Products
            {
                Id = entity.Id,
                ProductName = entity.ProductName,
                Photo = entity.Photo,
                Code = entity.Code
            });
            NewCommandExecute();
            ShowMessage.ShowMessage(String.Format("{0} Is Saved.", Title), MessageBoxImage.Ok);
        }

        private void UpdateProduct(whostpos.Entitys.Entites.Products entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Update?")) return;
            WposContext.ProductRepository.Update(entity);
            WposContext.Save();
            var updatedProduct = InternalCollection.Single(x => x.Id == entity.Id);
            updatedProduct.ProductName = entity.ProductName;
            updatedProduct.Photo = entity.Photo;
            ShowMessage.ShowMessage(String.Format("{0} Is Updated.", Title), MessageBoxImage.Ok);
        }

        private void DelectProduct(whostpos.Entitys.Entites.Products entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Delete?")) return;
            WposContext.ProductRepository.Remove(entity);
            WposContext.Save();
            InternalCollection.Remove(InternalCollection.Single(x => x.Id == entity.Id));
            LoadData();
            ShowMessage.ShowMessage(String.Format("{0} Is Deleted.", Title), MessageBoxImage.Ok);
        }

        private void BarcodePrintCommandExecute(string barcode)
        {
            Printer.Printer(ReportType.PrintBarcode, barcode);
        }

        private void GenerateProductCode()
        {
            if (SelectedItem != null)
            {
                SelectedItem.Code = WposContext.ProductRepository.GenerateProductCode();
                SelectedItem.BardCode = MiraculousMethods.ToBarcodeEN78(SelectedItem.Code);
            }
        }

        #endregion
    }
}
