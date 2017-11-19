using System.Collections;
using System.Collections.Generic;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.Services.Services
{
    public interface ISingleChatMessageService
    {
        ServiceResult<SingleChatMessage> CreateMessage(SingleChatMessage message);
        ServiceResult<IEnumerable<SingleChatMessage>> GetMessagesByConversation(long firstUser, long secondUser, int? skip = null, int? take = null);
        ServiceResult<SingleChatMessage> UpdateMessage(SingleChatMessage message);
        ServiceResult DeleteMessage(SingleChatMessage message);
        ServiceResult<IEnumerable<UserProfile>> GetConversations(long userId);
        ServiceResult Connect(string email, string connId);
        ServiceResult Disconnect(string email);
    }
}