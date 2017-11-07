using LittleSocialNetwork.ApiModels.Models;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.Controllers;
using LittleSocialNetwork.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace LittleSocialNetwork.Web.Controllers
{
    [Route("api/account")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IAppSettings _settings;

        public AccountController(IAccountService accountService, IAppSettings settings)
        {
            _accountService = accountService;
            _settings = settings;
        }

        [Route("create-user")]
        public IActionResult RegisterAsUser([FromBody]RegisterApiModel model)
        {
            return ReturnResult(_accountService.Create(model.To(UserRole.User))
                .ConvertToResult(user => JwtTokenApiModel.From(user, _settings)));
        }
    }
}