using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
   public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbset;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbset = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(int id)
        {
            return dbset.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T,bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbset;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach(var incluesProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperties);
                }
            }

            if(orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperites = null)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // include properties will be comma separated
            if (includeProperites != null)
            {
                foreach (var includeProperty in includeProperites.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            // don't know why it is not working
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T enitityToRemove = dbset.Find(id);
            Remove(enitityToRemove);
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }
        public void RemoveListOfObjects(IEnumerable<T> entity)
        {
            dbset.RemoveRange(entity);
        }

    }
}
