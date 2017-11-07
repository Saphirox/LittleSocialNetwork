using LittleSocialNetwork.Common.Definitions.DependencyResolver;
using LittleSocialNetwork.Common.Definitions.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LittleSocialNetwork.Common.IoC
{
    public static class CommonIoC
    {
        public static IServiceCollection RegisterCommonDependencies(this IServiceCollection serviceCollection)
        {
            return serviceCollection;
        }

        public static IServiceCollection RegisterConfiguration(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
        {
            serviceCollection.AddSingleton(typeof(IAppSettings), new AppSettings(configurationRoot));

            return serviceCollection;
        }

        public static IDependencyResolver RegisterDependencyResolver(this IServiceCollection serviceCollection)
        {
            var dependencyResolver =
                new DependencyResolver().RegisterServiceProvider(serviceCollection.BuildServiceProvider());

            serviceCollection.AddSingleton(typeof(IDependencyResolver), dependencyResolver);

            return dependencyResolver;
        }
    }
}