using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

namespace DailyReport.Domain.EntityConfigurations
{
    public abstract class BaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TBaseEntity> builder)
        {
            builder.Property(x => x.Id).IsRequired();

            builder.Property(p => p.CreatedBy).HasMaxLength(maxLength: 100);

            builder.Property(p => p.UpdatedBy).HasMaxLength(maxLength: 100);
            EntityConfiguration(builder);
        }

        public abstract void EntityConfiguration(EntityTypeBuilder<TBaseEntity> builder);
    }
}
