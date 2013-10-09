using System;
using System.Linq;
using whostpos.Core.BaseClass;
using whostpos.Core.Helpers;
using whostpos.Entitys.Entites;

namespace whostpos.ViewModel.Supplier
{
    public class SupplierViewModel : BasicViewModel<Suppliers>
    {
        public SupplierViewModel()
            : base("Supplier Setup")
        {
            SelectedItem.EntityDate = DateTime.Today;
            InternalCollection = WposContext.SupplierRepository.GetAllSuppliers();
            ReportType = ReportType.Supplier;
            InitialAction();
        }

        private void InitialAction()
        {
            SaveAction = x =>
            {
                if (x.Id > 0)
                    UpdateSupplier(x);
                else
                    CreateSupplier(x);
            };
            DeleteAction = DeleteSupplier;
            SelectionChangeAction = x => SelectedItem = WposContext.SupplierRepository.FindById(x);
            ClearEntityAction = x => WposContext.SupplierRepository.ClearChanges(x);
            RemoveAddedEntityAction = x => WposContext.SupplierRepository.ClearChanges(x);
            CanExecuteFunc = x => x != null && string.IsNullOrEmpty(x.Error);
        }

        #region Private Methods

        private void CreateSupplier(Suppliers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Save?")) return;
            WposContext.SupplierRepository.Add(entity);
            WposContext.Save();
            InternalCollection.Add(new Suppliers
            {
                Id = entity.Id,
                Name = entity.Name,
                Mobile = entity.Mobile
            });
            NewCommandExecute();
            ShowMessage.ShowMessage(String.Format("{0} Is Saved.", Title), MessageBoxImage.Ok);
        }

        private void UpdateSupplier(Suppliers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Update?")) return;
            WposContext.SupplierRepository.Update(entity);
            WposContext.Save();
            var updatedProduct = InternalCollection.Single(x => x.Id == entity.Id);
            updatedProduct.Name = entity.Name;
            updatedProduct.Mobile = entity.Mobile;
            ShowMessage.ShowMessage(String.Format("{0} Is Updated.", Title), MessageBoxImage.Ok);
        }

        private void DeleteSupplier(Suppliers entity)
        {
            if (!ShowMessage.ShowYesNoMessage("Are You Sure Are You Want To Delete?")) return;
            WposContext.SupplierRepository.Remove(entity);
            WposContext.Save();
            InternalCollection.Remove(InternalCollection.Single(x => x.Id == entity.Id));
            ShowMessage.ShowMessage(String.Format("{0} Is Deleted.", Title), MessageBoxImage.Ok);
        }
        #endregion
    }
}
