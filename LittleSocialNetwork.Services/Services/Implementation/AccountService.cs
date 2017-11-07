using System;
using System.Linq;
using LittleSocialNetwork.Common.Definitions.Constants;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.DataAccess.Extensions;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.Services.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IHashingService _hashingService;
        private readonly IUnitOfWork _uow;

        public AccountService(IHashingService hashingService, IUnitOfWork uow)
        {
            _hashingService = hashingService;
            _uow = uow;
        }

        public ServiceResult<User> Create(User user)
        {
            var serviceResult = new ServiceResult<User>();

            try
            {
                if (_uow.Repository<User>().GetUserByEmail(user.Email) != null)
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
    }
}