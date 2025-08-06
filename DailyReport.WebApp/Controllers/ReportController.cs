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
        public IActionResult Detail(Guid Id)
        {
            return View();
        }
        public IActionResult Edit(Guid Id)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReportCommand request)
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
