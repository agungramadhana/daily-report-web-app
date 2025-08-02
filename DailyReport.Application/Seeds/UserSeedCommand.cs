using DailyReport.Application;
using DailyReport.Application.Interfaces;
using DailyReport.Application.Seeds;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application.Seeds
{
    public class UserSeedCommand : IRequest
    {

    }

    public class UserSeedCommandHandler : IRequestHandler<UserSeedCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvide;
        private const string passwordAdmin = "PasswordAdmin";
        public UserSeedCommandHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _jwtProvide = jwtProvider;
        }

        public async Task<Unit> Handle(UserSeedCommand request, CancellationToken cancellationToken)
        {
            var userSeed = UserSeed.GetUserSeed();

            var userQuery = await _dbContext.Entity<User>().Where(x => !x.IsDeleted).ToListAsync(cancellationToken);

            foreach (var user in userSeed)
            {
                if (!userQuery.Any(x => x.Id == user.Id))
                {
                    _dbContext.Entity<User>().Add(new User
                    { 
                        Id = user.Id,
                        EmployeeNumber = user.EmployeeNumber,
                        FullName = user.FullName,
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        RoleId = user.RoleId,
                        Password = _jwtProvide.Hash(passwordAdmin)
                    });
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
