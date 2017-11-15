using System.IO;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
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

        public static IServiceCollection RegisterDatabase(this IServiceCollection collection, IAppSettings settings)
        {
            collection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(settings.DatabaseSettings.CONNECTION_STRING));
            return collection;
        }

        private static void AddSwagger(this IServiceCollection collection)
        {
            collection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "LSN Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    In = "header",
                    Description = "Please insert JWT with Bearer",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.DescribeAllEnumsAsStrings();
                c.DescribeAllParametersInCamelCase();
            });
        }
    }
}
