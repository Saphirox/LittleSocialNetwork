using System.Collections.Generic;
using System.IO;
using LittleSocialNetwork.Common.Definitions.Constants;
using LittleSocialNetwork.Common.Definitions.DependencyResolver;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Common.IoC;
using LittleSocialNetwork.Configurations;
using LittleSocialNetwork.DataAccess;
using LittleSocialNetwork.DataAccess.IoC;
using LittleSocialNetwork.Services.IoC;
using LittleSocialNetwork.Web.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace LittleSocialNetwork.Web
{
    public class Startup
    {
        private IDependencyResolver _dependencyResolver;
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = 
                new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(ConfigurationFileNames.DATABASE_CONFIGURATION_FILENAME, optional: false, reloadOnChange: false)
                .AddJsonFile(ConfigurationFileNames.COMMON_CONFIGURATION_FILENAME, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    { ConfigurationFileKeys.APPLICATION_ROOT, Directory.GetCurrentDirectory() }
                });

            _configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _dependencyResolver = services
                .RegisterDataAccessDependencies()
                .RegisterServiceDependencies()
                .RegisterCommonDependencies()
                .RegisterConfiguration(_configuration)
                .RegisterWebDependencies()
                .RegisterDependencyResolver();

            services.AddMvc();
            services.AddCors();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_dependencyResolver.GetService<IAppSettings>().DatabaseSettings.CONNECTION_STRING));
            services.AddJwtAuthentication(_dependencyResolver.GetService<IAppSettings>());
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().Build());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeshMe API");
            });

            app.UseMvc();
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
