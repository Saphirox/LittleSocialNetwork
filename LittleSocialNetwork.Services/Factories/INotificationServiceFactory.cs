using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Services.Services;

namespace LittleSocialNetwork.Services.Factories
{
    public interface INotificationServiceFactory
    {
        INotificationService GetService(NotificationSourceType type);
    }
}