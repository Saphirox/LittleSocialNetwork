using LittleSocialNetwork.ApiModels.Models;
using LittleSocialNetwork.Services.Services;
using LittleSocialNetwork.Web.Configurations.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace LittleSocialNetwork.Web.Controllers
{
    [Route("api/friendship")]
    public class FriendshipController : BaseController
    {
        private readonly IFrienshipService _frienshipService;

        public FriendshipController(IFrienshipService frienshipService)
        {
            _frienshipService = frienshipService;
        }

        [HttpPost]
        public IActionResult Invite([FromBody] FriendshipApiModel model)
        {
            return ReturnResult(_frienshipService.Accept(model.To(CurrentUserId)));
        }
    }
}
