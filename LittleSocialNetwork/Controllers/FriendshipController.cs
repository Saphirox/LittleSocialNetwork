using LittleSocialNetwork.ApiModels.Models;
using LittleSocialNetwork.Services.Services;
using LittleSocialNetwork.Web.Configurations.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace LittleSocialNetwork.Web.Controllers
{
    [Route("api/friendship")]
    public class FriendshipController : BaseController
    {
        private IFrienshipService _frienshipService;

        public FriendshipController(IFrienshipService frienshipService)
        {
            _frienshipService = frienshipService;
        }

        [HttpPost]
        public IActionResult Invite([RequiredFromQuery] FriendshipApiModel model)
        {
            return ReturnResult(_frienshipService.Accept(model.To()));
        }
    }
}
