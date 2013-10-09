using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface ICustomerRepository : IBaseRepository<Customers>
    {
        ObservableCollection<Customers> GetAllCustomers();
        void AddToLedger(CustomerLedger customerLedger);
        IQueryable<CustomerLedger> SearchInLedger(Expression<Func<CustomerLedger, bool>> expression);
        void CustomerPayment(Int64 customerId, double amount);
        void AddToCustomerAccount(long customerId, double amount);
        int TotalCustomer();
    }
}
