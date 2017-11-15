using LittleSocialNetwork.Common.Definitions.Constants;
using Microsoft.Extensions.Configuration;

namespace LittleSocialNetwork.Common.Definitions.Settings
{
    public class ChatSettings
    {
        private readonly IConfigurationSection _section;

        private static string _DEFAULT_SINGLE_CHAT_URL = "/single-chat";

        public ChatSettings(IConfigurationSection section)
        {
            _section = section;
        }

        public string SINGLE_CHAT_URL => _section[ConfigurationFileKeys.SINGLE_CHAT_URL] ?? _DEFAULT_SINGLE_CHAT_URL;
    }
}