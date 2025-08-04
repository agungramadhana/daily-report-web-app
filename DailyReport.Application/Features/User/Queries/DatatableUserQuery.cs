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
                        .Include(x => x.Role)
                        .Where(x => !x.IsDeleted);

            var recordsTotal = query.Count();

            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.FullName.ToLower().Contains(request.Keyword.ToLower()) || 
                                         x.UserName.ToLower().Contains(request.Keyword.ToLower()) ||
                                         x.EmployeeNumber.ToLower().Contains(request.Keyword.ToLower()) ||
                                         x.Role.Name.ToLower().Contains(request.Keyword.ToLower())
                                    );

            switch (request.OrderCol?.ToLower())
            {
                case "fullname":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.FullName) : query.OrderByDescending(x => x.FullName);
                    break;
                case "username":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.UserName) : query.OrderByDescending(x => x.UserName);
                    break;
                case "employeename":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.EmployeeNumber) : query.OrderByDescending(x => x.EmployeeNumber);
                    break;
                case "rolename":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.Role.Name) : query.OrderByDescending(x => x.Role.Name);
                    break;
                
            }

            var recordsFiltered = query.Count();

            List<DatatableUserModel> data = await query
                                            .Skip(request.Start)
                                            .Take(request.Length)
                                            .Select(x => new DatatableUserModel
                                            {
                                                Id = x.Id,
                                                EmployeeNumber = x.EmployeeNumber,
                                                FullName = x.FullName,
                                                UserName = x.UserName,
                                                RoleName = x.Role.Name,
                                                IsActive = x.IsActive
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
