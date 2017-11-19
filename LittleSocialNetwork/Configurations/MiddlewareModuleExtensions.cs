using LittleSocialNetwork.Common.Definitions.DependencyResolver;
using LittleSocialNetwork.Common.Definitions.Settings;
using Microsoft.AspNetCore.Builder;

namespace LittleSocialNetwork.Web.Configurations
{
    public static class MiddlewareModuleExtensions
    {
        public static void UseCors(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .Build());
        }

        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SeshMe API");
            });
        }

        public static void UseChats(this IApplicationBuilder app, IAppSettings settings)
        {
            app.UseSignalR(o => o.MapHub<ChatHub>(settings.ChatSettings.SINGLE_CHAT_URL));
        }
    }
}
