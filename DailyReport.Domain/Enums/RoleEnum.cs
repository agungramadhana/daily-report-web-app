using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Domain.Enums
{
    public enum RoleEnum
    {
        [Description("Super Administrator")]
        SuperAdministrator,
        [Description("Administrator")]
        Administrator,
        [Description("User")]
        User
    }
}
