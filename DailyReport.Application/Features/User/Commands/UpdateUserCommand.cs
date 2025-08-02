using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Interfaces;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;
        public UpdateUserCommandHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var query = await _dbContext.Entity<User>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (query == null)
                {
                    throw new NotFoundException();
                }

                query.UserName = request.UserName;
                query.Email = request.Email;
                query.PhoneNumber = request.PhoneNumber;
                query.Address = request.Address;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
