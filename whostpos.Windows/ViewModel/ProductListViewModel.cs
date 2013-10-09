using whostpos.Domain.Context;
using whostpos.Entitys.Entites;
using whostpos.Windows.BaseClass;

namespace whostpos.Windows.ViewModel
{
    public class ProductListViewModel : BaseViewModel<Products>
    {
        public ProductListViewModel() : base("Product List")
        {
            IsBusy = true;
            InternalCollection = new WposContext().ProductRepository.GetAllProduucts();
            IsBusy = false;
        }
    }
}