using System.Security.Claims;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Extensions
{
    public static class IdentityHelperExtensions
    {
        public static ClaimsIdentity GenerateIdentity(this User user)
        {
            return new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()), 
            });
        }
    }
}