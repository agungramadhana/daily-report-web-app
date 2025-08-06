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
    public class UpdateReportCommand : IRequest
    {
        public Guid Id { get; set; }
        public string AreaName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public string? ReportNote { get; set; }
    }

    public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public UpdateReportCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Report>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (query is null)
                throw new NotFoundException("Report not found");

            query.AreaName = request.AreaName;
            query.Latitude = request.Latitude;
            query.Longitude = request.Longitude;
            query.Date = request.Date;
            query.ReportNote = request.ReportNote; ;

            _dbContext.Entity<Report>().Update(query);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
