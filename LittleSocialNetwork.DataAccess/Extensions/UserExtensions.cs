using System.Linq;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.DataAccess.Extensions
{
    public static class UserExtensions
    {
        public static User FindUserByEmail(this IRepository<User> repository, string email)
        {
            return repository.GetQueryable().SingleOrDefault(u => u.Email == email);
        }
    }
}