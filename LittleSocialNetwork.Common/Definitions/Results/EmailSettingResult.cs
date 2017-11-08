using System;
using System.Collections;
using System.Collections.Generic;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Messages;

namespace LittleSocialNetwork.Common.Definitions.Results
{
    public class EmailSettingResult : ServiceResult
    {
        public EmailSettingResult(ICollection<string> errors)
        {
            SettingErrors = errors;
            Status = ResultStatus.Error;
        }

        private ICollection<string> _settingsErrors;

        public ICollection<string> SettingErrors
        {
            get => _settingsErrors;
            set
            {
                _settingsErrors = value ?? throw new ArgumentNullException(nameof(value));
                ErrorMessage = string.Join(". ", SettingErrors);
            }
        }
    }
}