using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Enums;

namespace DailyReport.Application
{
    public class DatatableUserModel
    {
        public Guid? Id { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
