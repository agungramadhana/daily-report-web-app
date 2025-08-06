using DailyReport.Application;
using DailyReport.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyReport.WebApp.Controllers
{
    public class ReportController : BaseController
    {
        public ReportController(ICurrentUserService currentUser) : base(currentUser)
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
        public async Task<IActionResult> Create([FromBody] CreateReportCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
