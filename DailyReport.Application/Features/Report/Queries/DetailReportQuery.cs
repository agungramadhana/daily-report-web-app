using DailyReport.Application;
using DailyReport.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application
{
    public class DetailReportQuery : IRequest<ReportModel>
    {
        public Guid Id { get; set; }
    }

    public class DetailReportQueryHandler : IRequestHandler<DetailReportQuery, ReportModel>
    {
        private readonly IApplicationDbContext _dbContext;
        public DetailReportQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ReportModel> Handle(DetailReportQuery request, CancellationToken cancellationToken)
        {
            var report = await _dbContext.Entity<Report>()
                               .Where(x => x.Id == request.Id)
                               .Select(x => new ReportModel
                               {
                                   Id = x.Id,
                                   AreaName = x.AreaName,
                                   Latitude = x.Latitude,
                                   Longitude = x.Longitude,
                                   Date = x.Date,
                                   ReportNote = x.ReportNote
                               }).FirstOrDefaultAsync(cancellationToken);

            if (report is null)
                throw new NotFoundException("Report not found");

            return report;
        }
    }
}
