using System;
using GalaSoft.MvvmLight.Messaging;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Core.Classes
{
    public class SupplierList : ISupplierList
    {
        private Suppliers _selectedSupplier = new Suppliers();

        public SupplierList()
        {
            Messenger.Default.Register(this,
                new Action<Suppliers>(supplierInfo => { _selectedSupplier = supplierInfo; }));
        }

        public Suppliers GetSelectedSuppliers()
        {
            new Windows.SupplierList().ShowDialog();
            return _selectedSupplier;
        }
    }
}