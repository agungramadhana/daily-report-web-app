using DailyReport.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application
{
    public class CreateReportCommand : IRequest
    {
        public string AreaName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public string? ReportNote { get; set; }
    }

    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public CreateReportCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var data = new Report
            {
                AreaName = request.AreaName,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Date = request.Date,
                ReportNote = request.ReportNote,
            };

            await _dbContext.Entity<Report>().AddAsync(data, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
