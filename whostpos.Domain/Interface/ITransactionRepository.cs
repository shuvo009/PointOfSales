using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface ITransactionRepository : ICommonRepository<Transaction>
    {
        ObservableCollection<Transaction> GetTransctions(Expression<Func<Transaction, bool>> expression);
        void PurchaseOnCash(Transaction transaction);
        void PurchaseOnBank(Transaction transaction, Int64 bankId, string chequeNumber);
        void PurchaseOnDue(Transaction transaction);
        bool SalesOnCash(Transaction transaction);
        bool SalesOnBank(Transaction transaction, Int64 bankId, string chequeNumber);
        bool SalesOnDue(Transaction transaction);
        string GenerateInvoiceNumber();
        void SalesReturn(Transaction transaction);
        void PurchaseReturn(Transaction transaction);
        double TodaysSales();
        double WeeklySales();
        double MonthlySales();
        int TodayTotalSalesItem();
    }
}
