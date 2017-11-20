using System.ComponentModel.DataAnnotations;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class FriendshipApiModel
    {
        [Required]
        public long ToId { get; set; }
        public UserProfileApiModel To { get; set; }
        public long? FromId { get; set; }
        public FriendshipMode Mode { get; set; }
        public static FriendshipApiModel From(Friendship friendship)
        {
            return new FriendshipApiModel
            {
                FromId = friendship.FromId,
                ToId = friendship.ToId,
                To = UserProfileApiModel.From(friendship.To),
            };
        }
    }

    public static class FriendshipApiModelExtension
    {
        public static Friendship To(this FriendshipApiModel model, long? userId = null)
        {
            return new Friendship()
            {
                FromId = userId ?? model.FromId ?? 0,
                ToId = model.ToId,
            };
        } 
    }
}