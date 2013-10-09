using System.Collections.ObjectModel;
using whostpos.Domain.Context;
using whostpos.Entitys.Entites;
using whostpos.Windows.BaseClass;

namespace whostpos.Windows.ViewModel
{
    public class SupplierListViewModel : BaseViewModel<Suppliers>
    {
        public SupplierListViewModel() : base("Supplier List")
        {
            IsBusy = true;
            InternalCollection = new ObservableCollection<Suppliers>(new WposContext().SupplierRepository.GetAllSuppliers());
            IsBusy = false;
        }
    }
}
