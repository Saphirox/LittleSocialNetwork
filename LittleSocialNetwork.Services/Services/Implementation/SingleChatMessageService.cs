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

        public ServiceResult<SingleChatMessage> CreateMessage(SingleChatMessage message)
        {
            var serviceResult = new ServiceResult<SingleChatMessage>();

            try
            {
                _uow.Repository<SingleChatMessage>().Add(message);
                _uow.SaveChanges();

                var result = _uow.Repository<SingleChatMessage>()
                    .GetQueryable()
                    .Include(s => s.To)
                    .Include(s => s.From)
                    .FirstOrDefault(m => message.Id == m.Id);

                serviceResult.Result = result;
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

                var messages = _uow.Repository<SingleChatMessage>()
                    .GetQueryable()
                    .Where(m => m.FromId == userId || m.ToId == userId)
                    .Include(s => s.From)
                    .Include(s => s.To)
                    .ToList();

                var coll1 = messages.Select(s => s.From);
                
                var coll2 = messages.Select(s => s.To);

                serviceResult.Result = coll1.Concat(coll2).Distinct(new TestClass()).Where(s => s.Id != userId);
                    

                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult Connect(string email, string connId)
        {
            var serviceResult = new ServiceResult<string>();

            try
            {
                var user = _uow.Repository<User>().FindByEmailQueryable(email).Include(u => u.UserProfile).FirstOrDefault();

                user.UserProfile.ChatConnectionId = connId;

                _uow.SaveChanges();
                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }


        public ServiceResult Disconnect(string email)
        {
            var serviceResult = new ServiceResult();

            try
            {
                var user = _uow.Repository<User>().FindByEmailQueryable(email).Include(u => u.UserProfile).FirstOrDefault();

                user.UserProfile.ChatConnectionId = null;

                _uow.SaveChanges();
                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }


        private class TestClass : IEqualityComparer<UserProfile>
        {
            public bool Equals(UserProfile x, UserProfile y)
            {
                return x == y;
            }

            public int GetHashCode(UserProfile obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}