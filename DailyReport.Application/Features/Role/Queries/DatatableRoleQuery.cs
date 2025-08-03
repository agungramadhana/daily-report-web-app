using DailyReport.Application.Features.Role.Models;
using DailyReport.Application.Models;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DailyReport.Application
{
    public class DatatableRoleQuery : BaseDatatableRequest, IRequest<BaseDatatableResponse>
    {
    }

    public class DatatableRoleQueryHandler : IRequestHandler<DatatableRoleQuery, BaseDatatableResponse>
    {
        private readonly IApplicationDbContext _dbContext;

        public DatatableRoleQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseDatatableResponse> Handle(DatatableRoleQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Entity<Role>()
                .Where(x => !x.IsDeleted && x.Id != Guid.Empty)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            var recordsFiltered = query.Count();

            List<RoleModel> data = await query
                                        .Skip(request.Start)
                                        .Take(request.Length)
                                        .Select(x => new RoleModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            IsActive = x.IsActive
                                        }).ToListAsync(cancellationToken);

            return new BaseDatatableResponse
            {
                Data = data,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = query.Count(),
            };
        }
    }
}
