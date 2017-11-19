
using System.Threading.Tasks;
using LittleSocialNetwork.ApiModels.Models;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.Services.Services;
using LittleSocialNetwork.Web.Configurations;
using LittleSocialNetwork.Web.Configurations.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LittleSocialNetwork.Web.Controllers
{
    [Route("api/messages")]
    public class SingleChatController : HubController<ChatHub>
    {
        private readonly ISingleChatMessageService _messageService;

        public SingleChatController(IHubContext<ChatHub> hubContext, ISingleChatMessageService messageService) : base(hubContext)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Route("get-conversations")]
        public IActionResult GetConversations()
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
            var service = _messageService.CreateMessage(model.To(CurrentUserId));

            return await ReturnResult(service.ConvertToResult(SingleChatMessageApiModel.From)
                , nameof(CreateMessage), service.Result.To.ChatConnectionId, service.Result.From.ChatConnectionId);
        }

        [HttpGet]
        [Route("get-messages")]
        public IActionResult GetMessages(
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
