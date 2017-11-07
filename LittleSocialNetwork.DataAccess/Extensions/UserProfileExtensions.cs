using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.DataAccess.Extensions
{
    public static class UserProfileExtensions
    {
        public static void UpdateFrom(this UserProfile dest, UserProfile source)
        {
            dest.Sex = source.Sex;
            dest.BirthDate = source.BirthDate;
            dest.FirstName = source.FirstName;
            dest.LastName = source.LastName;
        }
    }
}