using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.Services.Services
{
    public interface IAccountService
    {
        ServiceResult<User> Create(User model);
    }
}