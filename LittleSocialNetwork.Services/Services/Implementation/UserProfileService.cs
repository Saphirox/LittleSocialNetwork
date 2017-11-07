using System;
using System.Collections.Generic;
using System.Linq;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.DataAccess.Extensions;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.Services.Services.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _uow;

        public UserProfileService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ServiceResult<UserProfile> Get(long id)
        {
            var serviceResult = new ServiceResult<UserProfile>();

            try
            {
                var entity = _uow.Repository<UserProfile>().FindById(id);

                serviceResult.Status = ResultStatus.Success;

                serviceResult.Result = entity;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult<UserProfile> Update(UserProfile model)
        {
            var serviceResult = new ServiceResult<UserProfile>();

            try
            {
                var entity = _uow.Repository<UserProfile>().FindById(model.Id);

                entity.UpdateFrom(model);

                _uow.SaveChanges();

                serviceResult.Result = entity;

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