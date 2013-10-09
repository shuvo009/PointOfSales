using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface IBankRepository : IBaseRepository<BankAccounts>
    {
        void Deposit(Int64 bankId, BankTransactions bankTransactions);
        void Withdrawal(Int64 bankId, BankTransactions bankTransactions);
        ObservableCollection<BankAccounts> GetBankAccounts();
        IQueryable<BankTransactions> GetBankTransctions(Expression<Func<BankTransactions, bool>> expression);
        int TotalBankAcount();
    }
}
