using System;
using System.Linq;
using LittleSocialNetwork.Common.Definitions.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LittleSocialNetwork.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static long GetUserId(this HttpContext context)
        {
            return 
                long.TryParse(context.User.FindFirst(ClaimTypes.Sid)?.Value, 
                    out long userId) 
                    ? userId : 0;
        }

        public static string GetUserIdAsString(this HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Sid)?.Value;
        }

        public static UserRole GetUserRole(this HttpContext context)
        {
            return Enum.TryParse(
                ((ClaimsIdentity)context.User.Identity).Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.Role)
                ?.Value, out UserRole role) ?
                role : UserRole.User;
        }

        public static string GetHostBaseUrl(this HttpContext context, string relativeUrl)
        {
            var protocol = context.Request.IsHttps ? "https" : "http";
            var host = context.Request.Host.Host;
            var port = context.Request.Host.Port ?? (context.Request.IsHttps ? 443 : 80);

            return new UriBuilder(protocol, host, port, relativeUrl).ToString();
        }
    }
}