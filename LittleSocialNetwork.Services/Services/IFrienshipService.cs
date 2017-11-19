using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.Services.Services
{
    public interface IFrienshipService
    {
        ServiceResult<Friendship> Accept(Friendship friendship);
        ServiceResult Decline(Friendship friendship);
        ServiceResult Invite(Friendship friendship);
    }
}