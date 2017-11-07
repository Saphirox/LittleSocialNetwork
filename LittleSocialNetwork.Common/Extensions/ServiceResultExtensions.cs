using System;
using System.Collections.Generic;
using System.Linq;
using LittleSocialNetwork.Common.Definitions.Results;

namespace LittleSocialNetwork.Common.Extensions
{
    public static class ServiceResultExtensions
    {
        public static ServiceResult<TModel> ConvertToResult<TEntity, TModel>(
            this ServiceResult<TEntity> source,
            Func<TEntity, TModel> transformator) 
            where TEntity : class where TModel : class
        {
            var serviceResult = new ServiceResult<TModel>();

            serviceResult.UpdateFrom(source, transformator);

            return serviceResult;
        }

        public static ServiceResult<IEnumerable<TModel>> ConvertToResult<TEntity, TModel>(
            this ServiceResult<IEnumerable<TEntity>> source,
            Func<TEntity, TModel> transformator)
            where TEntity : class where TModel : class
        {
            var serviceResult = new ServiceResult<IEnumerable<TModel>>();

            serviceResult.UpdateFrom(source, transformator);

            return serviceResult;
        }
    }
}