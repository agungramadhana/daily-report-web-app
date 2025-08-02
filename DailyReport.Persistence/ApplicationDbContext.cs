using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Application;
using DailyReport.Application.Interfaces;
using DailyReport.Domain.Abstractions;
using DailyReport.Domain.Entities;
using DailyReport.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService? _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
        }

        public DbSet<T> Entity<T>() where T : class, IEntity
        {
            return Set<T>();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _currentUserService.IdUser == null ? "SYSTEM" : _currentUserService.IdUser;
                    entry.Entity.CreatedAt = DateTime.UtcNow.Date;
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedBy = _currentUserService.IdUser;
                        entry.Entity.DeletedAt = DateTime.UtcNow.Date;
                    }
                    else
                    {
                        entry.Entity.UpdatedBy = _currentUserService.IdUser;
                        entry.Entity.UpdatedAt = DateTime.UtcNow.Date;
                    }
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

    }
}
