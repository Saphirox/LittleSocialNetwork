using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.DataAccess.EF
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void SaveChanges();
    }
}