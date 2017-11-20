using System;
using System.Collections.Generic;
using System.Linq;
using LittleSocialNetwork.Common.Definitions.Constants;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.DataAccess.EF;
using LittleSocialNetwork.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleSocialNetwork.Services.Services.Implementation
{
    public class FrienshipService : IFrienshipService
    {
        private readonly IUnitOfWork _uow;

        public FrienshipService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ServiceResult<Friendship> Accept(Friendship friendship)
        {
            var serviceResult = new ServiceResult<Friendship>();

            try
            {
                var friends = GetFriends(friendship.FromId);

                if (friends.Any())
                {
                    if (friends.Any(f => f.Id == friendship.ToId))
                    {
                        serviceResult.ErrorMessage = EMessages.UserHaveFriendship;
                        serviceResult.Status = ResultStatus.Error;
                        return serviceResult;
                    }
                }

                var requests = _uow.Repository<Friendship>()
                    .GetQueryable()
                    .Where(f => f.FromId == friendship.FromId && 
                                f.ToId == friendship.ToId 
                                && f.Status != FriendshipStatus.Accepted);

                if (requests.Any())
                {
                    serviceResult.ErrorMessage = EMessages.UserHavePendingRequest;
                    serviceResult.Status = ResultStatus.Error;
                    return serviceResult;
                }

                friendship.Status = FriendshipStatus.Pending;
                _uow.Repository<Friendship>().Add(friendship);

                _uow.SaveChanges();

                serviceResult.Result = friendship;
                serviceResult.Status = ResultStatus.Success;
            }
            catch (Exception e)
            {
                serviceResult.Status = ResultStatus.ServerError;
            }

            return serviceResult;
        }

        public ServiceResult Decline(Friendship friendship)
        {
            return null;
        }

        public ServiceResult Invite(Friendship friendship)
        {
            return null;
        }

        private IQueryable<UserProfile> GetFriends(long userId)
        {
            return _uow.Repository<Friendship>().GetQueryable()
                .Where(f => (f.FromId == userId || f.ToId == userId) && f.Status == FriendshipStatus.Accepted)
                .Include(f => f.To)
                .Include(f => f.From)
                .Select(s => s.FromId == userId ? s.To : s.From );
        }
    }
}