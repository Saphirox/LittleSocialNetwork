using LittleSocialNetwork.Common.Definitions.Constants;
using Microsoft.Extensions.Configuration;

namespace LittleSocialNetwork.Common.Definitions.Settings
{
    public class EmailSettings
    {
        private const int _DEFAULT_MAIL_SMTP_PORT = 25;

        private const bool _DEFAULT_REQUIRE_SSL = false;

        private readonly IConfigurationSection _section;

        internal EmailSettings(IConfigurationSection section)
        {
            _section = section;
        }

        public string MAIL_SMTP_HOST => _section[ConfigurationFileKeys.MAIL_SMTP_HOST];
        public string MAIL_SMTP_PASSWORD => _section[ConfigurationFileKeys.MAIL_SMTP_PASSWORD];
        public string MAIL_SMTP_LOGIN => _section[ConfigurationFileKeys.MAIL_SMTP_LOGIN];
        public string SYSTEM_SENDER_TITLE => _section[ConfigurationFileKeys.SYSTEM_SENDER_TITLE];
        public string SYSTEM_SENDER_ADDRESS => _section[ConfigurationFileKeys.SYSTEM_SENDER_ADDRESS];

        public int MAIL_SMTP_PORT
        {
            get
            {
                var value = _section[ConfigurationFileKeys.MAIL_SMTP_PORT];

                if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int timeSpan))
                {
                    return timeSpan;
                }

                return _DEFAULT_MAIL_SMTP_PORT;
            }
        }

        public bool REQUIRE_SSL
        {
            get
            {
                var value = _section[ConfigurationFileKeys.REQUIRE_SSL];

                if (!string.IsNullOrWhiteSpace(value) && bool.TryParse(value, out var requireSSL))
                {
                    return requireSSL;
                }

                return _DEFAULT_REQUIRE_SSL;
            }
        }
    }
}