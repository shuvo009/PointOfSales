using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using whostpos.Entitys.Entites;

namespace whostpos.Core.BaseClass
{
    public abstract class BaseSearchTransctionViewModel : SearchEnableViewModel<Transaction>
    {
        private ObservableCollection<Products> _productses;

        protected BaseSearchTransctionViewModel(string title)
            : base(title)
        {
            Productses = WposContext.ProductRepository.GetAllProduucts();
            ProductSelectionChange= new RelayCommand<Products>(ProductSelectionChageCommandExecute);
        }

        #region Command

        public RelayCommand<Products> ProductSelectionChange{ get; private set; }
        #endregion

        #region Command Execute

        private void ProductSelectionChageCommandExecute(Products products)
        {
            if (products != null)
                SearchMetadata.Name = products.ProductName;
        }

        #endregion

        #region Propertys

        public ObservableCollection<Products> Productses
        {
            get { return _productses; }
            set
            {
                _productses = value;
                RaisePropertyChanged(() => Productses);
            }
        }

        #endregion


    }
}
