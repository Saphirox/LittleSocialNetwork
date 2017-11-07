﻿using LittleSocialNetwork.Common.Definitions.Constants;
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
        }

        public AuthenticationSettings AuthenticationSettings { get; }
        public DatabaseSettings DatabaseSettings { get; }
        public string APPLICATION_ROOT => _confRoot[ConfigurationFileKeys.APPLICATION_ROOT];
    }
}