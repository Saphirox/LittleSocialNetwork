using LittleSocialNetwork.Services.Services;
using LittleSocialNetwork.Services.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace LittleSocialNetwork.Services.IoC
{
    public static class ServicesIoC
    {
        public static IServiceCollection RegisterServiceDependencies(this IServiceCollection collection)
        {
            collection.AddTransient<IAccountService, AccountService>();
            return collection;
        }
    }
}