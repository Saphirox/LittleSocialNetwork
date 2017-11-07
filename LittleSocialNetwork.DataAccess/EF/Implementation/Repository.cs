using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleSocialNetwork.DataAccess.EF.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbContext DbContext;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            return DbContext.Add(entity).Entity;
        }

        public IEnumerable<TEntity> Add(IReadOnlyCollection<TEntity> entities)
        {
            return DbContext.Add(entities).Entity;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return DbContext.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbContext.Attach(entity);
            }

            DbContext.Remove(entity);
        }

        public void Delete(IReadOnlyCollection<TEntity> entities)
        {
            if (DbContext.Entry(entities).State == EntityState.Detached)
            {
                DbContext.Attach(entities);
            }

            DbContext.Remove(entities);
        }
    }
}