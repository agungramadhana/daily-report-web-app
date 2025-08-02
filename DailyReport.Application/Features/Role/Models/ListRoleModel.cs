using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Domain.Enums;

namespace DailyReport.Application.Features.Role.Models
{
    public class ListRoleModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public RoleEnum Code { get; set; }
    }
}
