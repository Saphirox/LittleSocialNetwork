using LittleSocialNetwork.ApiModels.Models;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace LittleSocialNetwork.Web.Controllers
{
    [Route("api/profile")]
    public class UserProfileController : BaseController
    {
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }
        
        [HttpGet]
        public IActionResult Get([FromQuery] long id)
        {
            return ReturnResult(_userProfileService.Get(id).ConvertToResult(UserProfileApiModel.From));
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserProfileApiModel model)
        {
            return ReturnResult(_userProfileService.Update(model.To()).ConvertToResult(UserProfileApiModel.From));
        }
    }
}