using System.Collections.Generic;
using System.IO;
using LittleSocialNetwork.Common.Definitions.Constants;
using LittleSocialNetwork.Common.Definitions.DependencyResolver;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Common.IoC;
using LittleSocialNetwork.DataAccess;
using LittleSocialNetwork.DataAccess.IoC;
using LittleSocialNetwork.Services.IoC;
using LittleSocialNetwork.Web.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LittleSocialNetwork.Web.Configurations;

namespace LittleSocialNetwork.Web
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private IDependencyResolver _dependencyResolver;
        private IAppSettings _settings;

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

            _settings = _dependencyResolver.GetService<IAppSettings>();

            services.RegisterDatabase(_settings);
            services.AddSignalR();
            services.AddMvc();
            services.AddCors();
            services.AddJwtAuthentication(_settings);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseAuthentication();
            app.UseChats(_settings);
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMvc();
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
