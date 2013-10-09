using System.Collections.ObjectModel;
using whostpos.Domain.Context;
using whostpos.Entitys.Entites;
using whostpos.Windows.BaseClass;

namespace whostpos.Windows.ViewModel
{
    public class CustomerListViewModel : BaseViewModel<Customers>
    {
        public CustomerListViewModel() : base("Customer List")
        {
            IsBusy = true;
            InternalCollection = new ObservableCollection<Customers>(new WposContext().CustomerRepository.GetAllCustomers());
            IsBusy = false;
        }
    }
}
