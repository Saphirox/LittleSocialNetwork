using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.DataAccess.EF.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace LittleSocialNetwork.DataAccess.IoC
{
    public static class DataAccessIoC
    {
        public static IServiceCollection RegisterDataAccessDependencies(this IServiceCollection collection)
        {
            collection.AddScoped<IUnitOfWork, UnitOfWork>();
            return collection;
        }
    }
}