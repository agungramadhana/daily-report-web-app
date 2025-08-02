using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Interfaces;
using DailyReport.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application
{
    public class LoginQuery : IRequest<string>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IJwtProvider _jwtProvider;
        public LoginQueryHandler(IApplicationDbContext dbContext, IJwtProvider jwtProvider)
        {
            _dbContext = dbContext;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<User>()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (query == null)
            {
                throw new NotFoundException("Please check email or password");
            }

            if (!_jwtProvider.Verify(query.Password, request.Password))
            {
                throw new NotFoundException("Please check email or password");
            }

            var token = await _jwtProvider.GenerateToken(new UserModel
            {
                IdUser = query.Id,
                FullName = query.FullName,
                UserName = query.UserName,
                Role = query.Role.Name
            });

            return token;
        }
    }
}
