using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Features.Role.Models;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application
{
    public class ListRoleQuery : IRequest<List<RoleModel>>
    {
    }

    public class ListRoleQueryHandler : IRequestHandler<ListRoleQuery, List<RoleModel>>
    {
        private readonly IApplicationDbContext _dbContext;

        public ListRoleQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RoleModel>> Handle(ListRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Entity<Role>()
                .Where(x => !x.IsDeleted && x.Id != Guid.Empty)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new RoleModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsActive = x.IsActive
                }).ToListAsync(cancellationToken: cancellationToken);

            return result;
        }
    }
}
