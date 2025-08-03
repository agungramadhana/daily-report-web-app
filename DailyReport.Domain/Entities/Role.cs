using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Abstractions;
using DailyReport.Domain.Enums;

namespace DailyReport.Domain.Entities
{
    public class Role : BaseEntity, IEntity
    {
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
