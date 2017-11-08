using System;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Services.Services;
using LittleSocialNetwork.Services.Services.Implementation;

namespace LittleSocialNetwork.Services.Factories.Implementations
{
    public class NotificationServiceFactory : INotificationServiceFactory
    {
        private readonly IAppSettings _appSettings;

        public NotificationServiceFactory(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public INotificationService GetService(NotificationSourceType type)
        {
            switch (type)
            {
                case NotificationSourceType.Email:
                    return new EmailNotificationService(_appSettings);
                    break;
            }

            throw new NotImplementedException("Notification service does not exist");
        }
    }
}