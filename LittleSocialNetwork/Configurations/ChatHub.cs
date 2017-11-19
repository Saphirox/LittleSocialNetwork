using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.Entities;
using LittleSocialNetwork.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace LittleSocialNetwork.Web.Configurations
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ISingleChatMessageService _messageService;
        private readonly IHttpContextAccessor _accessor;

        public ChatHub(ISingleChatMessageService messageService, IHttpContextAccessor accessor)
        {
            _messageService = messageService;
            _accessor = accessor;
        }

        public override Task OnConnectedAsync()
        {
            _messageService.Connect(Context.User.GetUserEmail(), Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _messageService.Disconnect(Context.User.GetUserEmail());
            return base.OnDisconnectedAsync(exception);
        }
    }
}
