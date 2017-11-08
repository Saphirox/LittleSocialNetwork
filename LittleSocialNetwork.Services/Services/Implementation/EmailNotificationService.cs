using System;
using System.Collections.Generic;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Common.Messages;
using LittleSocialNetwork.Common.Validations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace LittleSocialNetwork.Services.Services.Implementation
{
    public class EmailNotificationService : INotificationService
    {
        private readonly IList<string> _errors;
        private readonly IAppSettings _settings;

        public bool CanUse => true;

        public EmailNotificationService(IAppSettings settings)
        {
            _settings = settings;
            _errors = new List<string>();
        }

        public ServiceResult Send(NotificationMessage msg)
        {
            var message = BuildMimeMessage(msg);

            if (!message.IsSuccessed)
            {
                return message;
            }

            ServiceResult serviceSettingResult = new EmailSettingResult(_errors);

            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(_settings.EmailSettings.MAIL_SMTP_HOST, _settings.EmailSettings.MAIL_SMTP_PORT, _settings.EmailSettings.REQUIRE_SSL);
                    client.Authenticate(_settings.EmailSettings.MAIL_SMTP_LOGIN, _settings.EmailSettings.MAIL_SMTP_PASSWORD);
                    client.Send(message.Result);

                    client.Disconnect(true);
                }

                serviceSettingResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceSettingResult.Status = ResultStatus.ServerError;
            }

            return serviceSettingResult;
        }

        private ServiceResult<MimeMessage> BuildMimeMessage(NotificationMessage message)
        {
            ServiceResult<MimeMessage> result = new ServiceResult<MimeMessage>();

            if (!message.From.IsValidEmail())
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessage = "Invalid sender address";
                return result;
            }

            if (!message.To.IsValidEmail())
            {
                result.Status = ResultStatus.Error;
                result.ErrorMessage = "Invalid recipient address";
                return result;
            }

            MimeMessage mimeMessage = new MimeMessage();

            mimeMessage.From.Add(new MailboxAddress(message.Title, message.From));
            mimeMessage.Sender = new MailboxAddress(message.Title, message.From);
            mimeMessage.Subject = message.Title;
            mimeMessage.Body = BuildBody(message);
            mimeMessage.To.Add(new MailboxAddress(message.Title, message.To));

            result.Result = mimeMessage;
            result.Status = ResultStatus.Success;

            return result;
        }

        private MimeEntity BuildBody(NotificationMessage message)
        {
            BodyBuilder resultBuilder = new BodyBuilder();

            resultBuilder.HtmlBody = message.Body;

            return resultBuilder.ToMessageBody();
        }

        public bool IsValidForUse => ValidateServiceInitailParameters();

        private bool ValidateServiceInitailParameters()
        {
            bool result = true;

            if (string.IsNullOrWhiteSpace(_settings.EmailSettings.MAIL_SMTP_HOST))
            {
                _errors.Add("Smtp host not set");
                result = false;
            }

            if (_settings.EmailSettings.MAIL_SMTP_PORT < 1)
            {
                _errors.Add("Smtp port is not valid");
                result = false;
            }

            if (string.IsNullOrWhiteSpace(_settings.EmailSettings.MAIL_SMTP_LOGIN))
            {
                _errors.Add("Smtp login not set");
                result = false;
            }

            if (string.IsNullOrWhiteSpace(_settings.EmailSettings.MAIL_SMTP_PASSWORD))
            {
                _errors.Add("Smtp password not set");
                result = false;
            }

            return result;
        }
    }
}