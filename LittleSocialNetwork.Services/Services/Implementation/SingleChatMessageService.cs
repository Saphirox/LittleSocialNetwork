using System;
using System.Collections.Generic;
using System.Linq;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.DataAccess.Extensions;
using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleSocialNetwork.Services.Services.Implementation
{
    public class SingleChatMessageService : ISingleChatMessageService
    {
        private readonly IUnitOfWork _uow;

        public SingleChatMessageService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ServiceResult Connect(long hubId, long currentUserId)
        {
            throw new NotImplementedException();

            var serviceResult = new ServiceResult();

            try
            {
                var user =  _uow.Repository<UserProfile>()
                    .FindById(currentUserId);

                if (user == null)
                {
                    serviceResult.Status = ResultStatus.Error;
                    serviceResult.ErrorMessage = "User does not exist";
                    return serviceResult;
                }

                _uow.SaveChanges();

                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult Disconnect(long currentUserId)
        {
            throw new NotImplementedException();

            var serviceResult = new ServiceResult();

            try
            {
                var user = _uow.Repository<UserProfile>()
                    .FindById(currentUserId);

                if (user == null)
                {
                    serviceResult.Status = ResultStatus.Error;
                    serviceResult.ErrorMessage = "User does not exist";
                    return serviceResult;
                }

                //user.SingleChatHubId = 0;

                _uow.SaveChanges();

                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult<SingleChatMessage> CreateMessage(SingleChatMessage message)
        {
            var serviceResult = new ServiceResult<SingleChatMessage>();

            try
            {
                _uow.Repository<SingleChatMessage>().Add(message);
                _uow.SaveChanges();
                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult<IEnumerable<SingleChatMessage>> GetMessagesByConversation(long firstUser, long secondUser, int? skip, int? take)
        {
            var serviceResult = new ServiceResult<IEnumerable<SingleChatMessage>>();

            try
            {
                var messages = _uow.Repository<UserProfile>()
                    .GetUsersByIds(firstUser, secondUser)
                    .Include(up => up.MessagesFromMe)
                    .ToList()
                    .SelectMany(up => up.MessagesFromMe)
                    .OrderByDescending(up => up.PostTime);

                serviceResult.Result = messages;

                if (skip != null)
                {
                    serviceResult.Result = serviceResult.Result.Skip(skip.Value);
                }

                if (take != null)
                {
                    serviceResult.Result = serviceResult.Result.Take(take.Value);
                }

                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult<SingleChatMessage> UpdateMessage(SingleChatMessage model)
        {
            var serviceResult = new ServiceResult<SingleChatMessage>();

            try
            {
                var entity = _uow.Repository<SingleChatMessage>()
                    .GetQueryable()
                    .FirstOrDefault(m => m.Id == model.Id && m.FromId == model.FromId);

                if (entity == null)
                {
                    serviceResult.Status = ResultStatus.Forbidden;
                    return serviceResult;
                }

                entity.Update(model);

                _uow.SaveChanges();

                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult DeleteMessage(SingleChatMessage model)
        {
            var serviceResult = new ServiceResult();

            try
            {
                var entity = _uow.Repository<SingleChatMessage>()
                    .GetQueryable()
                    .FirstOrDefault(m => m.FromId == model.FromId && m.Id == model.Id);

                entity.IsDeleted = DeletedMessage.ForSender;

                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult<IEnumerable<UserProfile>> GetConversations(long userId)
        {
            var serviceResult = new ServiceResult<IEnumerable<UserProfile>>();

            try
            {
                serviceResult.Result = _uow.Repository<UserProfile>()
                    .FindByIdQueryable(userId)
                    .Include(m => m.MessagesFromMe)
                    .ThenInclude(m => m.To)
                    .Include(m => m.MessagesFromMe)
                    .ThenInclude(m => m.From)
                    .ToList();

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