using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    class OpeningClosingBlanceRepository : BaseRepository<OpeningClosingBalance>, IOpeningClosingBlanceRepository
    {
        public OpeningClosingBlanceRepository(Dbesm dbesmBasic)
            : base(dbesmBasic)
        {
        }

        public void CashPayment(double amount)
        {
            var getCurrentCash = GetCurrentCash();
            getCurrentCash.Credit += amount;
        }

        public void CashReceive(double amount)
        {
            var getCurrentCash = GetCurrentCash();
            getCurrentCash.Debit += amount;
        }

        public ObservableCollection<OpeningClosingBalance> GetInitialBalances()
        {
            return new ObservableCollection<OpeningClosingBalance>(DbSetBasic.Where(x => x.IsInitial));
        }

        public OpeningClosingBalance GetCurrentCash()
        {
            var currentCash = DbSetBasic.FirstOrDefault(x => x.EntityDate == DateTime.Today);
            if (currentCash != null) return currentCash;
            currentCash = new OpeningClosingBalance();
            Add(currentCash);
            return currentCash;
        }

        public ObservableCollection<OpeningClosingBalance> GetCashs(Expression<Func<OpeningClosingBalance, bool>> expression)
        {
            return new ObservableCollection<OpeningClosingBalance>(DbSetBasic.Where(expression));
        }
    }
}