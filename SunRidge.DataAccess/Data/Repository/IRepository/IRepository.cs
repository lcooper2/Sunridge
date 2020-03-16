using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
   public interface IRepository<T> where T : class
    {
        // get by id
        T Get(int id);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null);

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);

        public void RemoveListOfObjects(IEnumerable<T> entity);

    }
}
