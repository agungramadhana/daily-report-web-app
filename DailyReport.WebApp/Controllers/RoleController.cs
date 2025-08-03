using DailyReport.Application;
using DailyReport.Application.Interfaces;
using DailyReport.Application.Models;
using DailyReport.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace DailyReport.WebApp.Controllers
{
    public class RoleController : BaseController
    {
        private readonly ILogger<RoleController> _logger;
        public RoleController(ICurrentUserService currentUser, ILogger<RoleController> logger) : base(currentUser)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.ActiveStatus = ActiveStatus(null);

            return View();
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            var role = await Mediator.Send(new DetailRoleQuery
            {
                Id = Id
            });

            ViewBag.ActiveStatus = ActiveStatus(role.IsActive);

            return View(role);
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var role = await Mediator.Send(new DetailRoleQuery
            {
                Id = Id
            });

            ViewBag.ActiveStatus = ActiveStatus(role.IsActive);

            return View(role);
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

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateRoleCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return Ok(await Mediator.Send(new DeleteRoleCommand
            {
                Id = Id
            }));
        }

        private List<SelectListItem> ActiveStatus(bool? activeStatus)
        {
            List<SelectListItem> listActive = new List<SelectListItem>();
            listActive.Add(new SelectListItem { Text = "Select Status", Value = "" });
            listActive.Add(new SelectListItem { Text = "Active", Value = "true", Selected = (activeStatus is not null ? activeStatus == true ? true : false : false) });
            listActive.Add(new SelectListItem { Text = "Inactive", Value = "false", Selected = (activeStatus is not null ? activeStatus == false ? true : false : false) });

            return listActive;
        }
    }
}
