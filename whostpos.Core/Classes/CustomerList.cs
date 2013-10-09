using System;
using GalaSoft.MvvmLight.Messaging;
using whostpos.Core.Interface;
using whostpos.Entitys.Entites;
using whostpos.Windows;

namespace whostpos.Core.Classes
{
    public class CustomerList : ICustomerList
    {
        private Customers _selectedCustomer = new Customers();

        public CustomerList()
        {
            Messenger.Default.Register(this,
                new Action<Customers>(customerInfo => { _selectedCustomer = customerInfo; }));
        }

        public Customers GetSelectedCustomer()
        {
            new CoustomerList().ShowDialog();
            return _selectedCustomer;
        }
    }
}