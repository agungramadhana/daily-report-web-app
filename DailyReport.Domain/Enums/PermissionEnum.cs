using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyReport.Domain.Enums
{
    public enum PermissionEnum
    {
        [Description("Create")]
        Create,
        [Description("Read")]
        Read,
        [Description("Update")]
        Update,
        [Description("Delete")]
        Delete
    }
}
