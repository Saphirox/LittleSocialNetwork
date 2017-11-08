using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Common.Messages;

namespace LittleSocialNetwork.Services.Services
{
    public interface INotificationService
    {
        ServiceResult Send(NotificationMessage message);

        bool CanUse { get; }
    }
}