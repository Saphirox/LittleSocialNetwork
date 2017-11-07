using System;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.DataAccess.EF;
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
                user.Password = _hashingService.Hash(user.Password);
                _uow.Repository<User>().Add(user);
                _uow.SaveChanges();

                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }
    }
}