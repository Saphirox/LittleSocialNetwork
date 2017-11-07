using LittleSocialNetwork.Common.Definitions.Constants;
using Microsoft.Extensions.Configuration;

namespace LittleSocialNetwork.Common.Definitions.Settings
{
    public class DatabaseSettings
    {
        private readonly IConfigurationSection _section;

        public DatabaseSettings(IConfigurationSection section)
        {
            _section = section;
        }

        public string CONNECTION_STRING => _section[ConfigurationFileKeys.CONNECTION_STRING];
    }
}