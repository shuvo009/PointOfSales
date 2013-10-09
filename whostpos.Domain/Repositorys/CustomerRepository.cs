using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class CustomerRepository : BaseRepository<Customers>, ICustomerRepository
    {
        private readonly Dbesm _dbesmBasic;

        public CustomerRepository(Dbesm dbesmBasic)
            : base(dbesmBasic)
        {
            _dbesmBasic = dbesmBasic;
        }

        public override void Add(Customers entity)
        {
            entity.EntityDate = DateTime.Today;
            entity.CustomerAccount = new CustomerAccount();
            base.Add(entity);
        }

        public ObservableCollection<Customers> GetAllCustomers()
        {
            return new ObservableCollection<Customers>(DbSetBasic.Where(x=>!x.IsDeleted).ToList().Select(x => new Customers
                {
                    Id = x.Id,
                    Name = x.Name,
                    Mobile = x.Mobile,
                    Photo = x.Photo,
                    CustomerAccount =  x.CustomerAccount
                }));
        }

        public void AddToLedger(CustomerLedger customerLedger)
        {
            customerLedger.EntityDate = DateTime.Now;
            DbesmBasic.CustomerLedgers.Add(customerLedger);
        }

        public IQueryable<CustomerLedger> SearchInLedger(Expression<Func<CustomerLedger, bool>> expression)
        {
            return DbesmBasic.CustomerLedgers.Where(expression);
        }

        public void CustomerPayment(Int64 customerId, double amount)
        {
            var cutomerInfo = FindById(customerId);
            cutomerInfo.CustomerAccount.Credit += amount;
            if (cutomerInfo.CustomerLedgers == null)
                cutomerInfo.CustomerLedgers = new Collection<CustomerLedger>();
            cutomerInfo.CustomerLedgers.Add(new CustomerLedger
            {
                Debit = amount,
                EntityDate = DateTime.Today,
                Particulars = "Payment",
                CustomersId = customerId
            });
            IOpeningClosingBlanceRepository openingAndClosingCash = new OpeningClosingBlanceRepository(_dbesmBasic);
            openingAndClosingCash.CashReceive(amount);
        }

        public void AddToCustomerAccount(long customerId, double amount)
        {
            var cutomerInfo = FindById(customerId);
            cutomerInfo.CustomerAccount.Debit += amount;
            if (cutomerInfo.CustomerLedgers == null)
                cutomerInfo.CustomerLedgers = new Collection<CustomerLedger>();
            cutomerInfo.CustomerLedgers.Add(new CustomerLedger
            { 
                Debit = amount,
                EntityDate = DateTime.Today,
                Particulars = "Sales Due",
                CustomersId = customerId
            });
        }

        public int TotalCustomer()
        {
            return DbSetBasic.Count(x => !x.IsDeleted);
        }
    }
}
