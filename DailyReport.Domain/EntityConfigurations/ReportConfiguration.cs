using DailyReport.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Domain.EntityConfigurations
{
    public class ReportConfiguration : BaseEntityConfiguration<Report>
    {
        public override void EntityConfiguration(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable($"tr_{nameof(Report)}");
            builder.HasKey( x => x.Id );
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
