using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application.Models;
using DailyReport.Domain.Entities;
using DailyReport.Infrastructure.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application
{
    public class DatatableUserQuery : BaseDatatableRequest, IRequest<BaseDatatableResponse>
    {
    }


    public class DatatableUserQueryHandler : IRequestHandler<DatatableUserQuery, BaseDatatableResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        public DatatableUserQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseDatatableResponse> Handle(DatatableUserQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Entity<User>()
                                .Where(x => !x.IsDeleted);

            var recordsTotal = query.Count();

            if (request.Keyword?.ToLower() != null)
            {
                query = query.Where(x => x.UserName.ToLower().Contains(request.Keyword.ToLower()));
            }

            switch(request.OrderCol?.ToLower())
            {
                case "username":
                    if (request.OrderType == "asc")
                    {
                        query = query.OrderBy(x => x.UserName);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.UserName);
                    }
                    break;
                case "createdat":
                    if (request.OrderType == "asc")
                    {
                        query = query.OrderBy(x => x.CreatedAt);
                    }
                    else
                    {
                        query = query.OrderByDescending(x => x.CreatedAt);
                    }
                    break;
            }

            var recordsFiltered = query.Count();

            List<DatatableUserModel> data = await query
                                            .Skip(request.Start)
                                            .Take(request.Length)
                                            .Select(x => new DatatableUserModel
                                            {
                                                Id = x.Id,
                                                UserName = x.UserName,
                                                CreatedAt = x.CreatedAt
                                            })
                                            .ToListAsync(cancellationToken);
            
            return new BaseDatatableResponse
            {
                Data = data,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal,
            };
        }
    }
}
