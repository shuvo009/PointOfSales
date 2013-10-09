using System;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Interface
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        IQueryable<Transaction> GetStockRegister(Expression<Func<Transaction, bool>> expression);
        Stock GetStockById(Int64 productId);
        IQueryable<Transaction> SearchInPurchase(Expression<Func<Transaction, bool>> expression);
        Int64 TotalStock();
    }
}
