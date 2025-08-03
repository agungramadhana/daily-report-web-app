using MediatR;
using DailyReport.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application
{
    public class DeleteRoleCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteRoleCommandHander : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        
        public DeleteRoleCommandHander(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _dbContext.Entity<Role>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            
            if (role is null)
                throw new NotFoundException("Role not found");

            role.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
