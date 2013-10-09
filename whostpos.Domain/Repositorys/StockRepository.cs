using System;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Entites;

namespace whostpos.Domain.Repositorys
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        private readonly Dbesm _dbesmBasic;

        public StockRepository(Dbesm dbesmBasic) : base(dbesmBasic)
        {
            _dbesmBasic = dbesmBasic;
        }

        public IQueryable<Transaction> GetStockRegister(Expression<Func<Transaction, bool>> expression)
        {
            return DbesmBasic.Transactions.Where(expression);
        }

        public Stock GetStockById(long productId)
        {
            var stockInfo = DbesmBasic.Productses.FirstOrDefault(x => x.Id == productId);
            return stockInfo != null ? stockInfo.Stock : new Stock();
        }

        public IQueryable<Transaction> SearchInPurchase(Expression<Func<Transaction, bool>> expression)
        {
            return DbesmBasic.Transactions.Where(expression);
        }

        public Int64 TotalStock()
        {
            return _dbesmBasic.Productses.Where(x => !x.IsDeleted).ToList().Sum(x => x.Stock.Quantity);
        }
    }
}
