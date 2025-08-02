using DailyReport.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DailyReport.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>() ?? throw new Exception("Mediator is required");

        private readonly ICurrentUserService _currentUser;

        public BaseController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            TempData["IdUser"] = _currentUser.IdUser;
            TempData["FullName"] = _currentUser.FullName;
            TempData["UserName"] = _currentUser.UserName;
            TempData["Role"] = _currentUser.Role;

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
