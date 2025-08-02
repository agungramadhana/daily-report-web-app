using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Interfaces;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using MediatR;

namespace DailyReport.Application
{
    public class AddUserCommand : IRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? RoleId { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;
        public AddUserCommandHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Entity<User>().Add(new User
                {
                    Id = Guid.NewGuid(),
                    UserName = request.UserName,
                    Email = request.Email,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Password = _jwtProvider.Hash(request.Password)
                });

                await _dbContext.SaveChangesAsync();

                return Unit.Value;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
