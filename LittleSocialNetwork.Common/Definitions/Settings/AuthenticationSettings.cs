using LittleSocialNetwork.Common.Definitions.Constants;
using Microsoft.Extensions.Configuration;

namespace LittleSocialNetwork.Common.Definitions.Settings
{
    public class AuthenticationSettings
    {
        private const string _DEFAULT_ISSUER_NAME = @"localhost";
        private const string _DEFAULT_AUDIENCE = @"MyKey";
        private const string _DEFAULT_ENCRYPTION_KEY = "MyKey";
        private const ushort _DEFAULT_TOKEN_LIFETIME_DAYS = 30;

        private readonly IConfigurationSection _section;

        internal AuthenticationSettings(IConfigurationSection section)
        {
            _section = section;
        }

        public string ISSUER_NAME => _section[ConfigurationFileKeys.ISSUER_NAME] ?? _DEFAULT_ISSUER_NAME;
        public string ENCRYPTION_KEY => _section[ConfigurationFileKeys.ENCRYPTION_KEY] ?? _DEFAULT_ENCRYPTION_KEY;
        public string AUDIENCE => _section[ConfigurationFileKeys.AUDIENCE] ?? _DEFAULT_AUDIENCE;

        public ushort TOKEN_LIFETIME_DAYS
        {
            get
            {
                var value = _section[ConfigurationFileKeys.TOKEN_LIFETIME_DAYS];

                if (!string.IsNullOrWhiteSpace(value) && ushort.TryParse(value, out ushort minutes))
                {
                    return minutes;
                }

                return _DEFAULT_TOKEN_LIFETIME_DAYS;
            }
        }
    }
}