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
    public class DetailRoleQuery : IRequest<RoleModel>
    {
        public Guid Id { get; set; }
    }
    public class DetailRoleQueryHandler : IRequestHandler<DetailRoleQuery, RoleModel>
    {
        private readonly IApplicationDbContext _dbContext;

        public DetailRoleQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RoleModel> Handle(DetailRoleQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Role>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (query is null)
                throw new NotFoundException("Role not found");

            RoleModel role = new RoleModel
            {
                Id = query.Id,
                Name = query.Name,
                IsActive = query.IsActive
            };

            return role;
        }
    }
}
