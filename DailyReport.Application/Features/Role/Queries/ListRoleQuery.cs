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
    public class ListRoleQuery : IRequest<List<ListRoleModel>>
    {
    }

    public class ListRoleQueryHandler : IRequestHandler<ListRoleQuery, List<ListRoleModel>>
    {
        private readonly IApplicationDbContext _dbContext;

        public ListRoleQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ListRoleModel>> Handle(ListRoleQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Entity<Role>()
                .Where(x => !x.IsDeleted && x.Code != RoleEnum.SuperAdministrator)
                .Select(x => new ListRoleModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                }).OrderBy(x => x.Code).ToListAsync(cancellationToken: cancellationToken);

            return result;
        }
    }
}
