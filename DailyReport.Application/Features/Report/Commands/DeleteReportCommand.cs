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
    public class DeleteReportCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteReportCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Report>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (query is null)
                throw new NotFoundException("Report not found");

            query.IsDeleted = true;

            _dbContext.Entity<Report>().Update(query);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
