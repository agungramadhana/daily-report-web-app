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
    public class UpdateRoleCommand : IRequest
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateRoleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Entity<Role>().AsQueryable();

            var isExists = await query.AnyAsync(x => x.Id != request.Id && x.Name.ToLower() == request.RoleName.ToLower(), cancellationToken);

            if (isExists)
                throw new BadRequestException("Role name already exists");

            var role = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (role is null)
                throw new NotFoundException("Role not found");

            role.Name = request.RoleName;
            role.IsActive = request.IsActive;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
