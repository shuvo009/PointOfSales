using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface IOpeningClosingBlanceRepository
    {
        void CashPayment(double amount);
        void CashReceive(double amount);
        OpeningClosingBalance GetCurrentCash();
        ObservableCollection<OpeningClosingBalance> GetInitialBalances();
        ObservableCollection<OpeningClosingBalance> GetCashs(Expression<Func<OpeningClosingBalance, bool>> expression);
        void Add(OpeningClosingBalance openingClosingBalance);
        void RemoveAddedEntity(OpeningClosingBalance entity);
    }
}
