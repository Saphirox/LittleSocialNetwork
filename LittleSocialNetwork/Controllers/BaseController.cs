using System.Net;
using LittleSocialNetwork.Common.Definitions.Enums;
using LittleSocialNetwork.Common.Definitions.Results;
using LittleSocialNetwork.Common.Extensions;
using LittleSocialNetwork.DataAccess.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LittleSocialNetwork.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _uow;

        public BaseController()
        {}
        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected UserRole CurrentUserRole => HttpContext.GetUserRole();
        protected long CurrentUserId => HttpContext.GetUserId();
                
        protected IActionResult ReturnResult<TEntity>(ServiceResult<TEntity> serviceResult) where TEntity : class
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (serviceResult.IsSuccessed)
            {
                _uow?.CommitTransaction();
                return Ok(serviceResult.Result);
            }

            return 
                serviceResult.Status == ResultStatus.Error
                ? BadRequest(serviceResult.ErrorMessage)
                : serviceResult.Status == ResultStatus.Forbidden
                    ? Forbid()
                    : StatusCode((int)HttpStatusCode.InternalServerError) as IActionResult;
        }

        protected IActionResult ReturnResult(ServiceResult serviceResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (serviceResult.IsSuccessed)
            {
                _uow?.CommitTransaction();
                return StatusCode((int)HttpStatusCode.NoContent);
            }

            return 
                serviceResult.Status == ResultStatus.Error
                ? BadRequest(serviceResult.ErrorMessage)
                : serviceResult.Status == ResultStatus.Forbidden
                    ? Forbid()
                    : StatusCode((int)HttpStatusCode.InternalServerError) as IActionResult;
        }
    }
}