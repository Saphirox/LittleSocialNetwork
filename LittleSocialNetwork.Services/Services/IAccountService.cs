using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.Services.Services
{
    public interface IAccountService
    {
        ServiceResult<User> Create(User model);
        ServiceResult<User> Authenticate(User model);
        ServiceResult ForgotPassword(string model, NotificationSourceType email);

        ServiceResult ChangePassword(string model, string password, NotificationSourceType type);
    }
}