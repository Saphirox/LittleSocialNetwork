using System.Collections.Generic;
using System.Linq;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.DataAccess.Extensions
{
    public static class UserProfileRepositoryExtensions
    {
        public static IQueryable<UserProfile> GetUsersByIds(this IRepository<UserProfile> profile, params long[] ids)
        {
            return profile.GetQueryable().Where(up => ids.Contains(up.Id));
        }
    }
}