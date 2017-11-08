using LittleSocialNetwork.ApiModels.Models;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Settings;
using LittleSocialNetwork.Common.Extensions;
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

        [HttpPost]
        [Route("create-user")]
        public IActionResult RegisterAsUser([FromBody]RegisterApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return ReturnResult(_accountService.Create(model.To(UserRole.User))
                .ConvertToResult(user => JwtTokenApiModel.From(user, _settings)));
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult SignInUser([FromBody] SignInApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return ReturnResult(_accountService.Authenticate(model.To())
                .ConvertToResult(user => JwtTokenApiModel.From(user, _settings)));
        }

        [HttpPost]
        [Route("forgot-password-email")]
        public IActionResult ForgotPasswordViaEmail([FromBody] ForgotPasswordViaEmailApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return ReturnResult(_accountService.ForgotPassword(model.Email, NotificationSourceType.Email));
        }

        [HttpPost]
        [Route("restore-password-email")]
        public IActionResult RestorePassword([FromBody]RestorePasswordViaEmailApiModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return ReturnResult(_accountService.ChangePassword(model.Email, model.NewPassword,
                NotificationSourceType.Email));
        }
    }
}