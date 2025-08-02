using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application
{
    public class DetailUserQuery : IRequest<DetailUserModel>
    {
        public Guid Id { get; set; }
    }

    public class DetailUserQueryHandler : IRequestHandler<DetailUserQuery, DetailUserModel>
    {
        private readonly IApplicationDbContext _dbContext;
        public DetailUserQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DetailUserModel> Handle(DetailUserQuery request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.Entity<User>()
                                .Where(x => !x.IsDeleted && x.Id == request.Id)
                                .Select(x => new DetailUserModel
                                {
                                    Id = x.Id,
                                    UserName = x.UserName,
                                    Email = x.Email,
                                    PhoneNumber = x.PhoneNumber,
                                    Address = x.Address
                                })
                                .FirstOrDefaultAsync(cancellationToken);

            if (query == null)
            {
                throw new NotFoundException("User Not Found !");
            }

            return query!;
        }
    }
}
