using System.Collections.Generic;
using System.Linq;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.DataAccess.EF
{
    public interface IRepository<TEntity> where TEntity: class, IEntity
    {
        TEntity Add(TEntity entity);
        IEnumerable<TEntity> Add(IReadOnlyCollection<TEntity> entities);
        IQueryable<TEntity> GetQueryable();
        void Delete(TEntity entity);
        void Delete(IReadOnlyCollection<TEntity> entities);
    }
}