using System;
using System.Threading.Tasks;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LittleSocialNetwork.Web.Controllers
{
    public class HubController<THub> : BaseController
        where THub : Hub 
    {
        private readonly Lazy<IHubContext<THub>> _hubContext;

        protected HubController(IHubContext<THub> hubContext)
        {
            _hubContext = new Lazy<IHubContext<THub>>(hubContext);
        }

        protected IHubContext<THub> GetHubContext()
        {
            return _hubContext.Value;
        }

        private async Task InvokeAsync(string userId, string methodName, params object[] model)
        {
            var chars = methodName.ToCharArray();
            chars[0] = char.ToLower(chars[0]);
            methodName = new string(chars);
            
            await GetHubContext().Clients.User(userId).InvokeAsync(methodName, model);
        }

        protected async Task<IActionResult> ReturnResult<TResult>(ServiceResult<TResult> serviceResult, string methodName, params string[] userIds) where TResult : class
        {
            if (!serviceResult.IsSuccessed)
            {
                return ReturnResult(serviceResult);
            }

            foreach (var userId in userIds)
            {
                await InvokeAsync(userId, methodName, serviceResult.Result);
            }

            return await Task.FromResult(Ok());
        }

        protected string CurrentUserIdAsString => HttpContext.GetUserIdAsString();
    }

    public class HubController<THub, TService> : HubController<THub>
        where THub : Hub<TService> where TService : class
    {
        protected HubController(IHubContext<THub> hubContext) : base(hubContext)
        { }
    }
}
