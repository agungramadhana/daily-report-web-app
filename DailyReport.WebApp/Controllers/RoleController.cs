using DailyReport.Application;
using DailyReport.Application.Interfaces;
using DailyReport.Application.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DailyReport.WebApp.Controllers
{
    public class RoleController : BaseController
    {
        public RoleController(ICurrentUserService currentUser) : base(currentUser)
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
        public async Task<IActionResult> DatatableRole(DatatableRoleQuery request)
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
