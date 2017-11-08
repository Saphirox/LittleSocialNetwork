using System;
using System.Linq;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.Common.Messages;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.DataAccess.Extensions;
using LittleSocialNetwork.Entities;
using LittleSocialNetwork.Services.Factories;
using Microsoft.AspNetCore.Http;

namespace LittleSocialNetwork.Services.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IHashingService _hashingService;
        private readonly IUnitOfWork _uow;
        private readonly INotificationServiceFactory _notificationServiceFactory;
        private readonly IAppSettings _settings;
        private readonly IHttpContextAccessor _httpContext;

        public AccountService(IHashingService hashingService, IUnitOfWork uow, INotificationServiceFactory notificationServiceFactory, IAppSettings settings, IHttpContextAccessor httpContext)
        {
            _hashingService = hashingService;
            _uow = uow;
            _notificationServiceFactory = notificationServiceFactory;
            _settings = settings;
            _httpContext = httpContext;
        }

        public ServiceResult<User> Create(User user)
        {
            var serviceResult = new ServiceResult<User>();

            try
            {
                if (_uow.Repository<User>().FindUserByEmail(user.Email) != null)
                {
                    serviceResult.ErrorMessage = "Email have already exist";
                    serviceResult.Status = ResultStatus.Error;
                    return serviceResult;
                }
                
                user.Password = _hashingService.Hash(user.Password);
                _uow.Repository<User>().Add(user);
                _uow.SaveChanges();

                serviceResult.Status = ResultStatus.Success;
                serviceResult.Result = user;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult<User> Authenticate(User model)
        {
            var result = new ServiceResult<User>();

            try
            {
                var user = _uow.Repository<User>().GetQueryable()
                    .FirstOrDefault(u => _hashingService.VerifyHashed(u.Password, model.Password)
                                && model.Email == u.Email);

                if (user == null)
                {
                    result.Status = ResultStatus.Forbidden;
                    return result;
                }

                result.Result = user;
                result.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.ServerError;
            }

            return result;
        }

        public ServiceResult ForgotPassword(string model, NotificationSourceType type)
        {
            var result = new ServiceResult();
            NotificationMessage message = null;
            User user = null;

            try
            {
                if (type == NotificationSourceType.Inner)
                {
                    throw new ArgumentException("Inner notification cannot be used for restoring password");
                }

                var notification = _notificationServiceFactory.GetService(type);

                if (!notification.CanUse)
                {
                    throw new ArgumentException($"Service {nameof(notification)} cannot be use");
                }

                switch (type)
                {
                    case NotificationSourceType.Email:

                        user = _uow.Repository<User>().FindUserByEmail(model);

                        if (user == null)
                        {
                            result.Status = ResultStatus.Error;
                            result.ErrorMessage = "User does not exist";
                            return result;
                        }

                        var relativeUrl = $"/restore-passoword/{_hashingService.Hash("SomeText")}";

                        message = new NotificationMessage
                        {
                            Title = "Restoring password",
                            To = model,
                            Body = $"Click a link below {_httpContext.HttpContext.GetHostBaseUrl(relativeUrl)}",
                            From = _settings.EmailSettings.MAIL_SMTP_LOGIN
                        };
                        break;
                    default: 
                        throw new ArgumentException();
                }

                result = notification.Send(message);

                if (!result.IsSuccessed)
                {
                    return result;
                }

                user.RestorePassowordType = type;
                user.PasswordReseted = true;
                _uow.SaveChanges();

                result.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.ServerError;
            }

            return result;
        }

        public ServiceResult ChangePassword(string model, string password, NotificationSourceType type)
        {
            var result = new ServiceResult();

            try
            {
                User user = null;

                switch (type)
                {
                    case NotificationSourceType.Email:
                        user = _uow.Repository<User>().FindUserByEmail(model);
                        break;
                        default:
                            throw new ArgumentException();
                }

                if (user == null)
                {
                    result.Status = ResultStatus.Error;
                    result.ErrorMessage = "User does not exist";
                    return result;
                }

                user.Password = _hashingService.Hash(password);

                _uow.SaveChanges();

                result.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                result.Status = ResultStatus.ServerError;
            }

            return result;
        }
    }
}