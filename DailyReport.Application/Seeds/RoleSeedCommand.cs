using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application.Seeds
{
    public class RoleSeedCommand : IRequest
    {
    }

    public class RoleSeedCommandHandler : IRequestHandler<RoleSeedCommand>
    {
        private IApplicationDbContext _dbContext;

        public RoleSeedCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(RoleSeedCommand request, CancellationToken cancellationToken)
        {
            var roleSeed = RoleSeed.GetRoleSeed();

            var roleQuery = await _dbContext.Entity<Role>().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);

            foreach (var role in roleSeed)
            {
                if (!roleQuery.Any(x => x.Id == role.Id))
                {
                    _dbContext.Entity<Role>().Add(new Role
                    {
                        Id = role.Id,
                        Name = role.Name!,
                    });
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
