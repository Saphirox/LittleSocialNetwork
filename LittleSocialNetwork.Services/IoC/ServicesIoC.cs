using LittleSocialNetwork.Services.Factories;
using LittleSocialNetwork.Services.Factories.Implementations;
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
            collection.AddTransient<IUserProfileService, UserProfileService>();
            collection.AddTransient<IHashingService, HashingService>();
            collection.AddTransient<INotificationServiceFactory, NotificationServiceFactory>();
            return collection;
        }
    }
}