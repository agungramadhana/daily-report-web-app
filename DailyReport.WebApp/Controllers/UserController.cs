using DailyReport.Application;
using DailyReport.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyReport.WebApp.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ICurrentUserService currentUser) : base(currentUser)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DatatableUser(DatatableUserQuery request)
        {
            try
            {
                var result = await Mediator.Send(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
