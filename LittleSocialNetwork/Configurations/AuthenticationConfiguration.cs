using System.Text;
using LittleSocialNetwork.Common.Definitions.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LittleSocialNetwork.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IAppSettings settings)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = settings.AuthenticationSettings.ISSUER_NAME,

                        ValidateAudience = true,
                        ValidAudience = settings.AuthenticationSettings.AUDIENCE,
                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(settings.AuthenticationSettings.ENCRYPTION_KEY)),

                        ValidateIssuerSigningKey = true
                    };
                });
        }
    }
}
