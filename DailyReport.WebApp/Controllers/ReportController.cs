using DailyReport.Application;
using DailyReport.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyReport.WebApp.Controllers
{
    public class ReportController : BaseController
    {
        private readonly ILogger<ReportController> _logger;

        public ReportController(ICurrentUserService currentUser, ILogger<ReportController> logger) : base(currentUser)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Detail(Guid Id)
        {
            var report = await Mediator.Send(new DetailReportQuery
            {
                Id = Id
            });

            return View(report);
        }
        public async Task<IActionResult> Edit(Guid Id)
        {
            var report = await Mediator.Send(new DetailReportQuery
            {
                Id = Id
            });

            return View(report);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReportCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateReportCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
        [HttpPost]
        public async Task<IActionResult> DatatableReport(DatatableReportQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
