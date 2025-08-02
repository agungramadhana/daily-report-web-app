using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyReport.Domain.EntityConfigurations
{
    public class RoleConfiguration : BaseEntityConfiguration<Role>
    {
        public override void EntityConfiguration(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable($"ms_{nameof(Role)}");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.HasIndex(x => x.Id);
        }
    }
}
