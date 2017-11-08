using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace LittleSocialNetwork.Web.IoC
{
    public static class WebIoC
    {
        public static IServiceCollection RegisterWebDependencies(this IServiceCollection collection)
        {
            collection.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            collection.AddSwagger();
            return collection;
        }

        private static void AddSwagger(this IServiceCollection collection)
        {
            collection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }
    }
}