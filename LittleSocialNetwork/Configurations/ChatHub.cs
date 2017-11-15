using System.Threading.Tasks;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace LittleSocialNetwork.Web.Configurations
{

    public class ChatHub : Hub<ISingleChatMessageService>
    {
        private readonly IHttpContextAccessor _context;

        public ChatHub(IHttpContextAccessor context)
        {
            _context = context;
        }
    }
}
