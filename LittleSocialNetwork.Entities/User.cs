using LittleSocialNetwork.Common.Definitions.Enums;

namespace LittleSocialNetwork.Entities
{
    public class User : IEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public long Id { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}