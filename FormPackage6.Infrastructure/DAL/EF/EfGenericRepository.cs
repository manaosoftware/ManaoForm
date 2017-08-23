using FormPackage6.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FormPackage6.Infrastructure.DAL.EF
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbSet<T> dbSet;

        public EfGenericRepository(DbSet<T> dbSet)
        {
            this.dbSet = dbSet;
        }

        #region IGenericRepository<T> implementation

        public virtual IQueryable<T> AsQueryable()
        {
            return dbSet.AsQueryable();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public virtual T Single(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Single(predicate);
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return dbSet.SingleOrDefault(predicate);
        }

        public virtual T First(Expression<Func<T, bool>> predicate)
        {
            return dbSet.First(predicate);
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Attach(T entity)
        {
            dbSet.Attach(entity);
        }
        #endregion
    }
}
