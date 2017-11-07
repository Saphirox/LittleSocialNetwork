using System.Collections;
using System.Collections.Generic;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Entities;

namespace LittleSocialNetwork.Services.Services
{
    public interface IUserProfileService
    {
        ServiceResult<UserProfile> Get(long id);
        ServiceResult<UserProfile> Update(UserProfile model);
    }
}