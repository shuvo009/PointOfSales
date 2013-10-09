using System;
using System.Linq;
using System.Linq.Expressions;

namespace whostpos.Domain.Interface
{
    public interface IBaseRepository<T> : ICommonRepository<T>
    {
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        IQueryable<T> GetAll();
        T FindById(Int64 id);
        bool IsExists(Expression<Func<T, bool>> expression);
        IQueryable<T> Search(Expression<Func<T, bool>> expression);
        void RemoveAddedEntity(T entity);
    }
}
