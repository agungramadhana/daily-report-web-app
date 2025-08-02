using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Abstractions;
using DailyReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DailyReport.Application
{
    public interface IApplicationDbContext
    {
        DbSet<T> Entity<T>() where T : class, IEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
