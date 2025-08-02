using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteUserCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (query == null)
            {
                throw new NotFoundException("User not found !");
            }

            query.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
