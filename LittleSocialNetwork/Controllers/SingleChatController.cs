
using System.Threading.Tasks;
using LittleSocialNetwork.ApiModels.Models;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.Services.Services;
using LittleSocialNetwork.Web.Configurations;
using LittleSocialNetwork.Web.Configurations.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LittleSocialNetwork.Web.Controllers
{
    [Route("api/messages")]
    public class SingleChatController : HubController<ChatHub, ISingleChatMessageService>
    {
        private readonly ISingleChatMessageService _messageService;

        public SingleChatController(IHubContext<ChatHub> hubContext, ISingleChatMessageService messageService) : base(hubContext)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [Route("get-conversations")]
        public async Task<IActionResult> GetConversations()
        {
            return ReturnResult(_messageService.GetConversations(CurrentUserId).ConvertToResult(UserProfileApiModel.From));
        }

        [HttpPost]
        [Route("create-message")]
        public async Task<IActionResult> CreateMessage([FromBody] SingleChatMessageApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            return await ReturnResult(
                _messageService.CreateMessage(model.To(CurrentUserId)), nameof(CreateMessage), CurrentUserIdAsString, model.ToId.ToString());
        }

        [HttpGet]
        [Route("get-messages")]
        public async Task<IActionResult> GetMessages(
            [RequiredFromQuery] long otherId,
            [RequiredFromQuery] int skip,
            [RequiredFromQuery] int take)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(otherId);
            }

            return ReturnResult(
                _messageService.GetMessagesByConversation(CurrentUserId, otherId, skip, take)
                    .ConvertToResult(SingleChatMessageApiModel.From));
        }
    }
}
