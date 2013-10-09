using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class BankRepository : BaseRepository<BankAccounts>, IBankRepository
    {
        public BankRepository(Dbesm dbesmBasic)
            : base(dbesmBasic)
        {
        }

        public void Deposit(long bankId, BankTransactions bankTransactions)
        {
            var bankInfo = FindById(bankId);
            bankInfo.AccountBalance += bankTransactions.Deposit;
            bankTransactions.DateOfTan = DateTime.Now;
            bankInfo.BankTransaction.Add(bankTransactions);
        }

        public void Withdrawal(long bankId, BankTransactions bankTransactions)
        {
            var bankInfo = FindById(bankId);
            bankInfo.AccountBalance -= bankTransactions.Withdrawal;
            bankTransactions.DateOfTan = DateTime.Now;            
            bankInfo.BankTransaction.Add(bankTransactions);
        }

        public IQueryable<BankTransactions> GetBankTransctions(Expression<Func<BankTransactions, bool>> expression)
        {
            return DbesmBasic.BankTransactionses.Where(expression);
        }

        public ObservableCollection<BankAccounts> GetBankAccounts()
        {
            return new ObservableCollection<BankAccounts>(GetAll().ToList()
                                                             .Select(x => new BankAccounts
                                                             {
                                                                 Id = x.Id,
                                                                 BankName = x.BankName,
                                                                 AccountName = x.AccountName,
                                                             }));
        }

        public int TotalBankAcount()
        {
            return DbSetBasic.Count(x => !x.IsDeleted);
        }
    }
}
