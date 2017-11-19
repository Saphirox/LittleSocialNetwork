using LittleSocialNetwork.Common.Definitions.Enums;

namespace LittleSocialNetwork.Entities
{
    public class Friendship : IEntity
    {
        public long Id { get; set; }
        public long FromId { get; set; }
        public UserProfile From { get; set; }
        public long ToId { get; set; }
        public UserProfile To { get; set; }
        public FriendshipStatus Status { get; set; }
    }
}