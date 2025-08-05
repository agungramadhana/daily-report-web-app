using DailyReport.Application;
using DailyReport.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace DailyReport.WebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> logger;
        public UserController(ICurrentUserService currentUser, ILogger<UserController> logger) : base(currentUser)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.ListRole = await ListRoles(null);
            ViewBag.ActiveStatus = ActiveStatus(null);

            return View();
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            var data = await Mediator.Send(new DetailUserQuery
            {
                Id = Id
            });

            ViewBag.ListRole = ListRoles(data.RoleId).Result;
            ViewBag.ActiveStatus = ActiveStatus(data.IsActive);

            return View(data);
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var data = await Mediator.Send(new DetailUserQuery
            {
                Id = Id
            });

            ViewBag.ListRole = ListRoles(data.RoleId).Result;
            ViewBag.ActiveStatus = ActiveStatus(data.IsActive);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
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

        [HttpPost]
        public async Task<IActionResult> Edit([FromBody] UpdateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand
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
        private async Task<List<SelectListItem>> ListRoles(Guid? roleId)
        {
            var listRole = new List<SelectListItem>();
            var roles = await Mediator.Send(new ListRoleQuery());

            listRole.Add(new SelectListItem
            {
                Text = "Select Role",
                Value = string.Empty
            });

            foreach (var item in roles)
            {
                listRole.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = roleId is not null && roleId == item.Id ? true : false
                });
            }
            return listRole;
        }
    }
}
