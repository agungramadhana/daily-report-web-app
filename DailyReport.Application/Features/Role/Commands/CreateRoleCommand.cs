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
    public class CreateRoleCommand : IRequest
    {
        public string RoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateRoleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<Role>().FirstOrDefaultAsync(x => x.Name.ToLower() == request.RoleName.ToLower(), cancellationToken);

            if (query is not null)
                throw new BadRequestException("Role name already exists");

            _dbContext.Entity<Role>().Add(new Role
            {
                Name = request.RoleName,
                IsActive = request.IsActive
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
