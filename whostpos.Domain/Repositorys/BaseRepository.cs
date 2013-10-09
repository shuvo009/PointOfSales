using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using whostpos.Domain.Context;
using whostpos.Domain.Interface;
using whostpos.Entitys.Interface;

namespace whostpos.Domain.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable
        where T : class, IEntity
    {
        internal readonly Dbesm DbesmBasic;
        internal DbSet<T> DbSetBasic;

        public BaseRepository(Dbesm dbesmBasic)
        {
            DbesmBasic = dbesmBasic;
            DbSetBasic = DbesmBasic.Set<T>();
        }

        public virtual void Add(T entity)
        {
            DbSetBasic.Add(entity);
        }

        public virtual void Remove(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public virtual void Update(T entity)
        {
            DbSetBasic.Attach(entity);
            DbesmBasic.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> GetAll()
        {
            return DbSetBasic.Where(x => !x.IsDeleted);
        }

        public T FindById(long id)
        {
            return DbSetBasic.FirstOrDefault(x => x.Id == id);
        }

        public bool IsExists(Expression<Func<T, bool>> expression)
        {
            return DbSetBasic.Any(expression);
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> expression)
        {
            return DbSetBasic.Where(expression);
        }

        public void ClearChanges(T entity)
        {
            if (entity == null) return;
            if (DbesmBasic.Entry(entity).State == EntityState.Modified)
                DbesmBasic.Entry(entity).State = EntityState.Unchanged;
        }

        public void RemoveAddedEntity(T entity)
        {
            if (entity == null) return;
                DbSetBasic.Remove(entity);
        }

        public void Dispose()
        {
            if (DbesmBasic != null)
                DbesmBasic.Dispose();
            DbSetBasic = null;
        }
    }
}
