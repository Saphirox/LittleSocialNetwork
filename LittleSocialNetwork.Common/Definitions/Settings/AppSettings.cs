using LittleSocialNetwork.Common.Definitions.Constants;
using Microsoft.Extensions.Configuration;

namespace LittleSocialNetwork.Common.Definitions.Settings
{
    public class AppSettings : IAppSettings
    {
        private readonly IConfigurationRoot _confRoot;

        public AppSettings(IConfigurationRoot confRoot)
        {
            _confRoot = confRoot;
            AuthenticationSettings = new AuthenticationSettings(confRoot.GetSection(ConfigurationFileKeys.AUTHENTICATION_SECTION));
            DatabaseSettings = new DatabaseSettings(confRoot.GetSection(ConfigurationFileKeys.DATABASE_SECTION));
            EmailSettings = new EmailSettings(confRoot.GetSection(ConfigurationFileKeys.EMAIL_SECTION));
            ChatSettings = new ChatSettings(confRoot.GetSection(ConfigurationFileKeys.CHAT_SECTION));
        }

        public ChatSettings ChatSettings { get; }
        public AuthenticationSettings AuthenticationSettings { get; }
        public DatabaseSettings DatabaseSettings { get; }
        public EmailSettings EmailSettings { get; }
        public string APPLICATION_ROOT => _confRoot[ConfigurationFileKeys.APPLICATION_ROOT];
    }
}