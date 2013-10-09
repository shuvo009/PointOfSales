using System;
using GalaSoft.MvvmLight.Messaging;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Core.Classes
{
    public class ProductList : IProductList
    {
        private Products _selectedProduct;

        public ProductList()
        {
            Messenger.Default.Register(this, new Action<Products>(productInfo =>
            {
                _selectedProduct = new Products();
                _selectedProduct = productInfo;
            }));
        }

        public Products GetSelectedProduct()
        {
            new Windows.ProductList().ShowDialog();
            return _selectedProduct;
        }
    }
}