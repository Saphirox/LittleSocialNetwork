using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LittleSocialNetwork.ApiModels.Extensions;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Entities;
using Microsoft.IdentityModel.Tokens;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class JwtTokenApiModel
    {
        public string Token { get; set; }

        public static JwtTokenApiModel From(User user, IAppSettings settings)
        {
            var identity = user.GenerateIdentity();

            return new JwtTokenApiModel()
            {
                Token = GenerateToken(identity, settings)
            };
        }

        private static string GenerateToken(ClaimsIdentity identity, IAppSettings settings)
        {
            DateTime now = DateTime.UtcNow;

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: settings.AuthenticationSettings.ISSUER_NAME,
                audience: settings.AuthenticationSettings.AUDIENCE,
                claims: identity.Claims,
                notBefore: now,
                expires: now.AddDays(settings.AuthenticationSettings.TOKEN_LIFETIME_DAYS),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AuthenticationSettings.ENCRYPTION_KEY)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}