using System.Linq;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.DataAccess.Extensions
{
    public static class CommonExtensions
    {
        public static TEntity FindById<TEntity>(this IRepository<TEntity> entity, long id) where TEntity : class, IEntity
        {
            return entity.GetQueryable().SingleOrDefault(e => e.Id == id);
        }

        public static IQueryable<TEntity> FindByIdQueryable<TEntity>(this IRepository<TEntity> entity, long id) where TEntity : class, IEntity
        {
            return entity.GetQueryable().Where(e => e.Id == id);
        }
    }
}