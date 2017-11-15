using System.Collections.Generic;
using System.Linq;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.ApiModels.Models
{
    public class GetConversationApiModel
    {
        public long? ReceiverId { get; set; }
        public IEnumerable<SingleChatMessageApiModel> Messages { get; set; }
        public IEnumerable<UserProfileApiModel> Users { get; set; }

        public static GetConversationApiModel From(IEnumerable<SingleChatMessage> messageEntities, IEnumerable<UserProfile> userEntities)
        {
            return new GetConversationApiModel
            {
                ReceiverId = messageEntities.Any() ? messageEntities.ToArray()[0].ToId : (long?)null,
                Messages = messageEntities.Select(SingleChatMessageApiModel.From),
                Users = userEntities.Select(UserProfileApiModel.From)
            };
        }
    }
}