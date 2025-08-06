using DailyReport.Application.Features.Report.Models;
using DailyReport.Application.Models;
using DailyReport.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Application
{
    public class DatatableReportQuery : BaseDatatableRequest, IRequest<BaseDatatableResponse>
    {
    }
    public class DatatableReportQueryHandler : IRequestHandler<DatatableReportQuery, BaseDatatableResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        public DatatableReportQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseDatatableResponse> Handle(DatatableReportQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Entity<Report>()
                        .Where(x => x.CreatedBy != null)
                        .Select(x => new ReportModel
                        {
                            Id = x.Id,
                            FullName = _dbContext.Entity<User>()
                                .Where(u => u.Id.ToString() == x.CreatedBy)
                                .Select(u => u.FullName)
                                .FirstOrDefault(),
                            AreaName = x.AreaName,
                            Latitude = x.Latitude,
                            Longitude = x.Longitude,
                            Date = x.Date
                        })
                        .AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                var keyword = request.Keyword.ToLower();
                query = query.Where(x => x.AreaName.ToLower().Contains(keyword) || x.FullName.ToLower().Contains(keyword));
            }

            switch (request?.OrderCol?.ToLower())
            {
                case "fullname":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.FullName) : query.OrderByDescending(x => x.FullName);
                    break;
                case "areaname":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.AreaName) : query.OrderByDescending(x => x.AreaName);
                    break;
                case "latitude":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.Latitude) : query.OrderByDescending(x => x.Latitude);
                    break;
                case "longitude":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.Longitude) : query.OrderByDescending(x => x.Longitude);
                    break;
                case "date":
                    query = request.OrderType == "asc" ? query.OrderBy(x => x.Date) : query.OrderByDescending(x => x.Date);
                    break;
            }

            var recordsFiltered = query.Count();

            var data = await query.Take(request.Length).Skip(request.Start).ToListAsync(cancellationToken);

            return new BaseDatatableResponse
            {
                Data = data,
                RecordsFiltered = recordsFiltered,
                RecordsTotal = query.Count(),
            };
        }
    }
}
